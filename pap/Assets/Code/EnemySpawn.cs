using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    float XPos;
    Vector2 SitioSpawn;
    public float TempoSpawn = 2f;
    float ProxSpawn = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > ProxSpawn)
        {
            ProxSpawn = Time.time + TempoSpawn;
            XPos = Random.Range(-38f, 34f);
            SitioSpawn = new Vector2(XPos, transform.position.y);
            Instantiate(Enemy, SitioSpawn, Quaternion.identity);
        }
    }
}
