using System.Diagnostics;

public class WalkState : IState
{
	public Player _player;
	public WalkState(Player player)
	{
		this._player = player;
	}

	public void OnEnter()
	{
		_player.playerAnimator.SetBool("Walk", true);
	}

	public void OnUpdate()
	{
		//Die
		if (_player.playerHealth.Death)
		{
			_player.playerStateMachine.TransitionTo(_player.playerStateMachine.dieState);
		}

		//Skill
		if (_player.playerInputManager.IsSkill)
		{
			_player.playerStateMachine.TransitionTo(_player.playerStateMachine.skillState);
		}
		//Dash
		else if (_player.playerInputManager.IsDash)
		{
			_player.playerStateMachine.TransitionTo(_player.playerStateMachine.dashState);
		}
		//Hit
		else if (_player.playerHealth.IsHit)
		{
			_player.playerStateMachine.TransitionTo(_player.playerStateMachine.hitState);
		}
		//Idle
		else if (_player.playerInputManager.InputMoveDir.magnitude < 0.1f)
		{
			_player.playerStateMachine.TransitionTo(_player.playerStateMachine.idleState);
		}
	}

	public void OnExit()
	{
		//_player.playerAnimator.SetBool("Walk", false);
	}

}
