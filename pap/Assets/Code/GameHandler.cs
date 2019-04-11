using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    private void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);

        Debug.Log("Health: " + healthSystem.GetHealthPercent());
        healthSystem.Damage(10);
        Debug.Log("Health: " + healthSystem.GetHealthPercent());
    }
}
