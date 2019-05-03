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
                UpdateCowntdownUI();
                break;
        }
    }

    void UpdateCowntdownUI()
    {
        Debug.Log("COUNITNG");
    }
}
