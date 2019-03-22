using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public enum SpawnState {Spawning, Waiting, Counting };   

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
    public float waveCountdown = 0f;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;

    void Start()
    {
        waveCountdown = timeBetweenWaves;    
    }

    void Update()
    {
        if(state == SpawnState.Waiting)
        {
            if (EnemyIsAlive() == false)
            {
                Debug.Log("Wave completed!");
                return;
            }else
            {
                return;
            }
        }

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
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);
        state = SpawnState.Spawning;

        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning enemy: " + _enemy.name);
        Instantiate(_enemy, transform.position, transform.rotation);
    }
}
