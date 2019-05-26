using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public void IniMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartApp()
    {
        SceneManager.LoadScene("paap");
    }
}
