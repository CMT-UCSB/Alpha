using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadScene("Level 1"); 
    }

    public void OnOptions()
    {
        //TODO
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
