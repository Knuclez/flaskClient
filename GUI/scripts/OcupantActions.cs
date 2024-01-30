using Godot;
using System;

public partial class OcupantActions : Control
{
	private Ocupant _ocu;
	public void Init(Ocupant ocu)
	{
		_ocu = ocu;
		GetNode<Label>("Label").Text = ocu.Tag;
		if (ocu.GetFlag(0) == 1)
		{
			Button mButton = GetNode<Button>("MoveButton");
			mButton.ButtonUp += ocu.SetAsActiveOcupant;
			mButton.Visible = true;
		}
		if (ocu.GetFlag(1) == 1)
		{
			Button enterButton = GetNode<Button>("EnterButton");	
			enterButton.ButtonUp += ocu.SetAsActiveOcupant;
			enterButton.Visible = true;
		}

		GetNode<Label>("ownerLabel").Text = ocu.GetOwnerId().ToString();
	}
	public override void _Ready()
	{
		GetNode<Button>("MoveButton").ButtonUp += GetNode<StateMachine>("/root/map/StateMachine").OnMovingButton;
		GetNode<Button>("EnterButton").ButtonUp += GetNode<InMapGUI>("/root/map/gui").OpenBuildingPanel;
		GetNode<Button>("AttackButton").ButtonUp += GetNode<StateMachine>("/root/map/StateMachine").OnAttackButton;
		
		if ( _ocu.GetOwnerId() != GetNode<User>("/root/User").GetUserId())
		{
			Button attackButton = GetNode<Button>("AttackButton");
			attackButton.ButtonUp += _ocu.AttackThisOcupant;
			attackButton.Visible = true;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
