using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    [Range(0, 8)]
    public int index;

    private Camera m_camera;
    private float m_camHeight;
    private float m_camWidth;

	// Use this for initialization
	void Start ()
    {
        m_camera = Camera.main;
        m_camHeight = m_camera.orthographicSize * 2;
        m_camWidth = m_camHeight / 5.4f * 9.6f;

        Vector3 pos = new Vector3(0, 0, 10);

        switch (index)
        {
            case 0:
                pos.x = m_camera.transform.position.x - m_camWidth;
                pos.y = m_camera.transform.position.y + m_camHeight;
                break;
            case 1:
                pos.x = m_camera.transform.position.x;
                pos.y = m_camera.transform.position.y + m_camHeight;
                break;
            case 2:
                pos.x = m_camera.transform.position.x + m_camWidth;
                pos.y = m_camera.transform.position.y + m_camHeight;
                break;
            case 3:
                pos.x = m_camera.transform.position.x - m_camWidth;
                pos.y = m_camera.transform.position.y;
                break;
            case 4:
                pos.x = m_camera.transform.position.x;
                pos.y = m_camera.transform.position.y;
                break;
            case 5:
                pos.x = m_camera.transform.position.x + m_camWidth;
                pos.y = m_camera.transform.position.y;
                break;
            case 6:
                pos.x = m_camera.transform.position.x - m_camWidth;
                pos.y = m_camera.transform.position.y - m_camHeight;
                break;
            case 7:
                pos.x = m_camera.transform.position.x;
                pos.y = m_camera.transform.position.y - m_camHeight;
                break;
            case 8:
                pos.x = m_camera.transform.position.x + m_camWidth;
                pos.y = m_camera.transform.position.y - m_camHeight;
                break;
            default:
                break;
        }

        transform.position = pos;
    }
	
    public void MoveRight()
    {
        // 0,3,6
        if (index % 3 == 0)
        {
            index += 2;
            Vector3 pos = transform.position;
            pos.x += (3 * m_camWidth);
            transform.position = pos;
        }
        else
        {
            index -= 1;
        }
    }

    public void MoveLeft()
    {
        //2,5,8
        if((index + 1) % 3 == 0)
        {
            index -= 2;
            Vector3 pos = transform.position;
            pos.x -= (3 * m_camWidth);
            transform.position = pos;
        }
        else
        {
            index++;
        }
    }

    public void MoveUp()
    {
        // 6,7,8
        if (index > 5)
        {
            index -= 6;
            Vector3 pos = transform.position;
            pos.y += (3 * m_camHeight);
            transform.position = pos;
        }
        else
        {
            index += 3;
        }
    }

    public void MoveDown()
    {
        // 0,1,2
        if (index < 3)
        {
            index += 6;
            Vector3 pos = transform.position;
            pos.y -= (3 * m_camHeight);
            transform.position = pos;
        }
        else
        {
            index -= 3;
        }
    }
}
