using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    [Range(1.0f, 20.0f)]
    public float speed = 1.0f;
    
    private Camera m_mainCamera;
    private float m_cameraWidth;
    private float m_cameraHeight;

	// Use this for initialization
	void Start () {
        m_mainCamera = Camera.main;
        m_cameraHeight = m_mainCamera.orthographicSize * 2;
        m_cameraWidth = m_cameraHeight / 5.4f * 9.6f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        CheckBounds();
	}

    void CheckBounds()
    {
        bool outOfBoundsRight = transform.position.x > m_mainCamera.transform.position.x + (m_cameraWidth / 2);
        bool outOfBoundsLeft = transform.position.x < m_mainCamera.transform.position.x - (m_cameraWidth / 2);
        bool outOfBoundsTop = transform.position.y > m_mainCamera.transform.position.y + (m_cameraHeight / 2);
        bool outOfBoundsBottom = transform.position.y < m_mainCamera.transform.position.y - (m_cameraHeight / 2);

        if (outOfBoundsBottom || outOfBoundsTop || outOfBoundsLeft || outOfBoundsRight)
        {
            Destroy(gameObject);
        }
    }

    public void SetUp(Vector3 _up)
    {
        transform.up = _up;
    }

    public void SetSpeed(float _multiplier)
    {
        speed = _multiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
