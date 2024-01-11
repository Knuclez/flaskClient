using Godot;
using System;

public partial class TurnManager : Node
{
	[Export] private int _turn;
	public override void _Ready()
	{
	}

	public void PassTurn()
	{
		CallTurnPassInSv();
		_turn++;
	}

	private void CallTurnPassInSv()
	{
		
	}
}