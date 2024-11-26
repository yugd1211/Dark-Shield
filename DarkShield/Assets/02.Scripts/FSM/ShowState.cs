using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowState : MonoBehaviour
{
    private Text text;
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        text = GetComponent<Text>();
    }

    private void Start()
    {
        player.playerStateMachine.StateChanged += ChangeState;
    }
    private void OnDestroy()
    {
        player.playerStateMachine.StateChanged -= ChangeState;
    }

    private void ChangeState(IState state)
    {
        if (state != null)
        {
            text.text = state.GetType().ToString();
        }
    }
}
