using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject panel;

    bool isPaused = false;

    public void Menu_button()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        panel.SetActive(isPaused);
    }
}
