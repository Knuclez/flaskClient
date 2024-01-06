using Godot;
using System;

public partial class State : Node
{

	public virtual void Enter()
	{
	}
	public virtual void Exit(){}

	public virtual State ProcessPhysics(double delta)
	{
		return null;
	}
	
	public virtual State ProcessInput(InputEvent evento)
	{
		return null;
	}
	
	public virtual State CustomEvent(string evento)
	{
		return null;
	}

}
