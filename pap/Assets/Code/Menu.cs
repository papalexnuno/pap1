﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour{

    public void StartApp()
    {
        SceneManager.LoadScene("paap");
    }

    public void ExitApp()
    {
        Debug.Log("Saiu");
        Application.Quit();
    }

}
