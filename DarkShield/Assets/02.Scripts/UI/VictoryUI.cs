using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    private Button _quitButton;

    private void Awake()
    {
        _quitButton = GetComponentInChildren<Button>();
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
