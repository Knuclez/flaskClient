using Godot;
using System;

public partial class StateMachine : Node
{
	[Export] private State _initialState;
	private State _currentState;

	public void Init()
	{

		ChangeState(_initialState);
	}

	private void ChangeState(State newState)
	{
		if (_currentState != null)
		{
			_currentState.Exit();
		}

		_currentState = newState;
		_currentState.Enter();
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		State newState = _currentState.ProcessInput(@event);
		if (newState != null)
		{
			ChangeState(newState);
		}
	}

	private void OnCloseButton()
	{
		State newState = _currentState.CustomEvent("close_button");
		if (newState != null)
		{
			ChangeState(newState);
		}
	}

	public void OnMovingButton()
	{
		State newState = _currentState.CustomEvent("moving_button");
		if (newState != null)
		{
			ChangeState(newState);
		}
	}
}
