using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject bullet;
    public float speed;
    [Range(2.0f, 5.0f)]
    public float m_minDistanceAway = 2.0f;
    [Range(5.0f, 10.0f)]
    public float m_maxDistanceAway = 5.0f;
    public bool m_seek = true;

    private GameObject m_player;
    private Rigidbody2D m_rigidbody;
    private ParticleSystem m_particles;
    private bool m_dead = false;
    private Vector3 m_destination;
    private Vector3 m_distanceAway;
    private float m_maxDistanceAwayFromCamera = 20.0f;
    private float m_seekRadiusSqr = 8.0f;
    private float m_maxChaseDistanceSqr = 64.0f;

    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_particles = GetComponent<ParticleSystem>();

        if (Random.value >= 0.5f)
        {
            m_seek = true;
        }
        else
        {
            m_seek = false;
        }

        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_seek)
        {
            if ((m_player.transform.position - transform.position).sqrMagnitude < m_seekRadiusSqr)
            {
                m_seek = true;
            }
        }
        else
        {
            if ((m_player.transform.position - transform.position).sqrMagnitude > m_maxChaseDistanceSqr)
            {
                m_seek = false;
            }
        }

        if (Mathf.Abs((Camera.main.transform.position - transform.position).magnitude) > m_maxDistanceAwayFromCamera)
        {
            //Destroy(gameObject);
            m_dead = true;
        }

        if (m_dead)
        {
            if (!m_particles.isPlaying)
            {
                //Destroy(gameObject);

                float camHeight = Camera.main.orthographicSize;
                float camWidth = camHeight / 5.4f * 9.6f;
                float rightBorder = Camera.main.transform.position.x + camWidth;
                float leftBorder = Camera.main.transform.position.x - camWidth;
                float topBorder = Camera.main.transform.position.y + camHeight;
                float bottomBorder = Camera.main.transform.position.y - camHeight;
                Vector3 position;

                if (Random.value > 0.5f)
                {
                    position.x = Random.Range(leftBorder - 3.0f, leftBorder);
                }
                else
                {
                    position.x = Random.Range(rightBorder, rightBorder + 3.0f);
                }

                if (Random.value > 0.5f)
                {
                    position.y = Random.Range(bottomBorder - 3.0f, bottomBorder);
                }
                else
                {
                    position.y = Random.Range(topBorder, topBorder + 3.0f);
                }

                position.z = 0;
                transform.position = position;
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<PolygonCollider2D>().enabled = true;
                SetNewDestination();

                if (Random.value >= 0.5f)
                {
                    m_seek = true;
                }
                else
                {
                    m_seek = false;
                }

                m_dead = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (m_seek)
        {
            Vector3 forward = Vector3.Normalize(m_player.transform.position - transform.position);
            forward.z = 0;
            transform.up = forward;

            m_rigidbody.AddForce(forward * Time.deltaTime * speed);
        }
        else
        {
            Vector3 destinationVec = m_destination - transform.position;
            Vector3 forward = Vector3.Normalize(destinationVec);
            forward.z = 0;
            transform.up = forward;

            m_rigidbody.AddForce(forward * Time.deltaTime * speed);

            if (destinationVec.sqrMagnitude < m_seekRadiusSqr)
            {
                destinationVec = m_destination;
                destinationVec.x += Random.Range(-5.0f, 5.0f);
                destinationVec.y += Random.Range(-5.0f, 5.0f);
                m_destination = destinationVec;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_dead = true;
            m_particles.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    Vector3 SetNewDestination()
    {
        m_distanceAway.x = Random.Range(m_minDistanceAway, m_maxDistanceAway);
        m_distanceAway.y = Random.Range(m_minDistanceAway, m_maxDistanceAway);
        m_destination.x = transform.position.x + (Random.Range(-1.0f, 1.0f) * m_distanceAway.x);
        m_destination.y = transform.position.y + (Random.Range(-1.0f, 1.0f) * m_distanceAway.y);
        m_destination.z = 0.0f;

        return m_destination;
    }
}
