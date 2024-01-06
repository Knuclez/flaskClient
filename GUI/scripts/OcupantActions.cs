using Godot;
using System;

public partial class OcupantActions : Control
{
	public void Init(Ocupant ocu)
	{
		GetNode<Label>("Label").Text = ocu.Tag;
		GetNode<Button>("Button").ButtonUp += ocu.SetAsActiveOcupant;
	}
	public override void _Ready()
	{
		GetNode<Button>("Button").ButtonUp += GetNode<StateMachine>("/root/map/StateMachine").OnMovingButton;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
