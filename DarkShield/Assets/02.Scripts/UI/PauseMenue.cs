using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenue : MonoBehaviour
{
    public GameObject pausemeue;

    bool isPaused = false;
    public void Continue()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pausemeue.SetActive(isPaused);
    }

    public void GameExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
