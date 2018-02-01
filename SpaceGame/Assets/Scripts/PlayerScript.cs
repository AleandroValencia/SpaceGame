using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    enum MOUSEBUTTON
    {
        LEFT = 0,
        RIGHT,
        MIDDLE
    }

    [Range(1.0f, 1000.0f)]
    public float speed;
    public float boostMultiplier = 5.0f;
    public float maxVelocity;
    public float maxFuel;
    public float consumptionRate;
    public float recoverFuelRate;

    public GameObject bullet;
    public List<GameObject> backgrounds;

    public AudioSource m_shootSound;
    public AudioSource m_boostSound;

    private Rigidbody2D m_RigidBody;
    private bool m_boost = false;

    private float m_cameraHalfHeight;
    private float m_cameraHalfWidth;
    private float m_fuel;

    // Use this for initialization
    void Start()
    {
        m_cameraHalfHeight = Camera.main.orthographicSize;
        m_cameraHalfWidth = m_cameraHalfHeight / 5.4f * 9.6f;
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_fuel = maxFuel;
        GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        forward.z = 0.0f;
        transform.up = forward;

        ScrollBackground();

        Shoot(forward);
        Boost();
    }

    private void FixedUpdate()
    {
        Vector3 forward = Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

        // Cap values to 0.3f
        if (forward.x > 0.3f)
        {
            forward.x = 0.3f;
        }
        else if (forward.x < -0.3f)
        {
            forward.x = -0.3f;
        }
        if (forward.y > 0.3f)
        {
            forward.y = 0.3f;
        }
        else if (forward.y < -0.3f)
        {
            forward.y = -0.3f;
        }

        forward.z = 0.0f;
        transform.up = forward;

        Move(forward);
    }

    void Move(Vector3 _forward)
    {
        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetMouseButton((int)MOUSEBUTTON.RIGHT))
        {
            //transform.position += forward * speed * Time.deltaTime;
            if (m_RigidBody.velocity.magnitude < maxVelocity)
            {
                if (m_boost && m_fuel > 0.0f)
                {
                    m_RigidBody.AddForce(_forward * speed * boostMultiplier * Time.deltaTime);
                    m_fuel -= consumptionRate;
                    if (!m_boostSound.isPlaying)
                    {
                        m_boostSound.Play();
                    }
                    
                    if (!GetComponent<ParticleSystem>().isPlaying)
                    {
                        GetComponent<ParticleSystem>().Play();
                    }
                }
                else
                {
                    m_RigidBody.AddForce(_forward * speed * Time.deltaTime);

                    if (m_boostSound.isPlaying)
                    {
                        m_boostSound.Stop();
                    }

                    if (GetComponent<ParticleSystem>().isPlaying)
                    {
                        GetComponent<ParticleSystem>().Stop();
                    }
                }
            }
        }
    }

    void Shoot(Vector3 _forward)
    {
        if (Input.GetMouseButtonDown((int)MOUSEBUTTON.LEFT))
        {
            bullet.GetComponent<BulletScript>().SetUp(_forward);
            bullet.transform.position = transform.position;
            Instantiate(bullet);
            m_shootSound.Play();
        }
    }

    void Boost()
    {
        if (Input.GetKey("left shift"))
        {
            m_boost = true;
        }
        else
        {
            m_boost = false;
            if (m_fuel < maxFuel)
            {
                m_fuel += recoverFuelRate;
            }

            if (m_boostSound.isPlaying)
            {
                m_boostSound.Stop();
            }
        }
    }

    void ScrollBackground()
    {
        foreach (GameObject background in backgrounds)
        {
            if (background.GetComponent<ScrollingBackground>().index == 4)
            {
                if (transform.position.x > background.transform.position.x + m_cameraHalfWidth)
                {
                    foreach (GameObject bg in backgrounds)
                    {
                        bg.GetComponent<ScrollingBackground>().MoveRight();
                    }
                }
                else if (transform.position.x < background.transform.position.x - m_cameraHalfWidth)
                {
                    foreach (GameObject bg in backgrounds)
                    {
                        bg.GetComponent<ScrollingBackground>().MoveLeft();
                    }
                }

                if (transform.position.y > background.transform.position.y + m_cameraHalfHeight)
                {
                    foreach (GameObject bg in backgrounds)
                    {
                        bg.GetComponent<ScrollingBackground>().MoveUp();
                    }
                }
                else if (transform.position.y < background.transform.position.y - m_cameraHalfHeight)
                {
                    foreach (GameObject bg in backgrounds)
                    {
                        bg.GetComponent<ScrollingBackground>().MoveDown();
                    }
                }
            }
        }
    }

    public bool GetBoost()
    {
        return m_boost;
    }
}
