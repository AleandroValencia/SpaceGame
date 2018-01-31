using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    public float springConstant = 0.1f;
    public float horizontalBorder;
    public float verticalBorder;

    private Rigidbody2D m_rigidBody;

    // Use this for initialization
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        Vector3 playerPos = player.transform.position;
        float xDistanceBetween = playerPos.x - pos.x;
        float yDistanceBetween = playerPos.y - pos.y;

        bool outLeftBounds = xDistanceBetween < -horizontalBorder;
        bool outRightBounds = xDistanceBetween > horizontalBorder;
        bool outTopBounds = yDistanceBetween > verticalBorder;
        bool outBottomBounds = yDistanceBetween < -verticalBorder;

        if (outLeftBounds || outRightBounds || outTopBounds || outBottomBounds)
        {
            if (GetComponent<Rigidbody2D>().velocity.magnitude < player.GetComponent<PlayerScript>().maxVelocity)
            {
                if (player.GetComponent<PlayerScript>().GetBoost())
                {
                    // GetComponent<Rigidbody2D>().AddForce(player.GetComponent<Rigidbody2D>().velocity * player.GetComponent<PlayerScript>().boostMultiplier / 2);
                    GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(playerPos - pos) * player.GetComponent<PlayerScript>().speed * player.GetComponent<PlayerScript>().boostMultiplier / 2 * springConstant);
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(playerPos - pos) * player.GetComponent<PlayerScript>().speed * springConstant);
                }
            }
            else
            {
                // add force in opposite direction
                if (player.GetComponent<PlayerScript>().GetBoost())
                {
                    // GetComponent<Rigidbody2D>().AddForce(player.GetComponent<Rigidbody2D>().velocity * player.GetComponent<PlayerScript>().boostMultiplier / 2);
                    GetComponent<Rigidbody2D>().AddForce(-Vector3.Normalize(playerPos - pos) * player.GetComponent<PlayerScript>().speed * player.GetComponent<PlayerScript>().boostMultiplier / 2 * springConstant);
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(-Vector3.Normalize(playerPos - pos) * player.GetComponent<PlayerScript>().speed * springConstant);
                }
            }

            if (outLeftBounds)
            {
                if (m_rigidBody.velocity.x > 0)
                {
                    ResetXVelocity();
                }
            }
            else if (outRightBounds)
            {
                if (m_rigidBody.velocity.x < 0)
                {
                    ResetXVelocity();
                }
            }

            if (outTopBounds)
            {
                if (m_rigidBody.velocity.y < 0)
                {
                    ResetYVelocity();
                }
            }
            else if (outBottomBounds)
            {
                if (m_rigidBody.velocity.y > 0)
                {
                    ResetYVelocity();
                }
            }
        }
    }

    void ResetXVelocity()
    {
        Vector3 vel = m_rigidBody.velocity;
        vel.x = 0;
        m_rigidBody.velocity = vel;
    }

    void ResetYVelocity()
    {
        Vector3 vel = m_rigidBody.velocity;
        vel.y = 0;
        m_rigidBody.velocity = vel;
    }
}
