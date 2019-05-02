using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool EmPausa = false;
    public GameObject PauseMenuUi;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EmPausa)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    void Pause()
    {
        PauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        EmPausa = true;
    }

    public void Resume()
    {
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        EmPausa = false;
    }
    public void IniMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitApp()
    {
        Application.Quit();
    }
}

public class Controls_Back : MonoBehaviour
{
    public void BackButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
