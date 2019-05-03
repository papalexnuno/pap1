using UnityEngine;
using UnityEngine.UI;


public class WaveUI : MonoBehaviour
{
    [SerializeField]
    EnemySpawn spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Image waveCountdownImage;

    [SerializeField]
    Image waveCountImage;

    private EnemySpawn.SpawnState previousState;

    // Start is called before the first frame update
    void Start()
    {
        if (spawner == null)
        {
            Debug.LogError("No spawner referenced!");
            this.enabled = false;
        }

        if (waveAnimator == null)
        {
            Debug.LogError("No waveAnimator referenced!");
            this.enabled = false;
        }

        if (waveCountdownImage == null)
        {
            Debug.LogError("No waveCountdownImage referenced!");
            this.enabled = false;
        }

        if (waveCountImage == null)
        {
            Debug.LogError("No waveCountImage referenced!");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (spawner.State)
        {
            case EnemySpawn.SpawnState.COUNTING:
                UpdateCountingUI();
                break;

            case EnemySpawn.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;
        }

        previousState = spawner.State;

    }

    void UpdateCountingUI()
    {
        if(previousState != EnemySpawn.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveCounter", false);
            waveAnimator.SetBool("WaveCountdown", true);
            Debug.Log("COUNTING");
        }
    }

    void UpdateSpawningUI()
    {
        if (previousState != EnemySpawn.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveCounter", true);
            Debug.Log("SPAWNING");
        }
    }
}
