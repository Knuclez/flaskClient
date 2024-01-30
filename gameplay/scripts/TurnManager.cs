using Godot;
using System;

public partial class TurnManager : Node
{
	[Export] private int _turn;
	public override void _Ready()
	{
	}

	private void OnPassTurnButton()
	{
		GetNode<HttpRequest>("HTTPRequest").Request("http://127.0.0.1:8000/passturn");
	}
}