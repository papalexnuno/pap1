using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Controls_BackBtn : MonoBehaviour
{
    public void BackButton()
    {
        SceneManager.LoadScene("Menu");
    }
}