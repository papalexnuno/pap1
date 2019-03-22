using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public enum SpawnState {Spawning, Waiting, Counting};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    private SpawnState state = SpawnState.Counting;

    void Start()
    {
        waveCountdown = timeBetweenWaves;    
    }

    void Update()
    {
        if(waveCountdown <= 0)
        {
            if(state != SpawnState.Spawning)
            {
                StartCoroutine ( SpawnWave (waves [ nextWave ] ) );
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.Spawning;

        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning enemy: " + _enemy.name);
    }
}
