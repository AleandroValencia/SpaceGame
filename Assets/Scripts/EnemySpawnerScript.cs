using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour {

    public GameObject enemy;
    public int maxNumEnemies;

    private int m_enemyCount = 0;

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
}
