using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtonManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;
    private void Awake()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        _startButton = buttons[0];
        _quitButton = buttons[1];

        _startButton.onClick.AddListener(StartGame);
        _quitButton.onClick.AddListener(QuipGame);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void QuipGame()
    {
        Application.Quit();
    }
}
