using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour {

    public GameObject enemy;
    public int maxNumEnemies;
    public AudioSource[] sounds;

    private int m_enemyCount = 0;
    private bool m_soundPlaying = true;

	// Use this for initialization
	void Start () {

        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight / 5.4f * 9.6f;
        Vector3 position;
        
        for (int i = m_enemyCount; i < maxNumEnemies; ++i)
        {
            if (Random.value > 0.5f)
            {
                position.x = Random.Range(-camWidth - 3.0f, -camWidth);
            }
            else
            {
                position.x = Random.Range(camWidth, camWidth + 3.0f);
            }

            if (Random.value > 0.5f)
            {
                position.y = Random.Range(-camHeight - 3.0f, -camHeight);
            }
            else
            {
                position.y = Random.Range(camHeight, camHeight + 3.0f);
            }

            position.z = 0;
            Instantiate(enemy, position, transform.rotation);
        }
	}

    public void ToggleSound()
    {
        m_soundPlaying = !m_soundPlaying;
        foreach(AudioSource sound in sounds)
        {
            if (m_soundPlaying)
            {
                sound.enabled = true;
            }
            else
            {
                sound.enabled = false;
            }
        }
    }
}
