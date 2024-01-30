using Godot;
using System;

public partial class InBuildingGUI : Control
{
	private Barracks _barracks;
	private Vector2I _cellCoords;
	public void Init(Ocupant ocu, Vector2I cellCoords)
	{
		GetNode<Label>("Label").Text = ocu.Tag;
		_cellCoords = cellCoords;
	}
	public override void _Ready()
	{
		_barracks = GetNode<Barracks>("/root/map/Barracks");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnCloseButton()
	{
		QueueFree();
	}

	private void OnBarracksEnterButton()
	{
		GetNode<Control>("BarracksGUI").Visible = true;
	}
	
	private void OnBarracksOutButton()
	{
		GetNode<Control>("BarracksGUI").Visible = false;
	}

	private void OnUnitButton1()
	{
		_barracks.UnitRequested(2, _cellCoords);
	}
	private void OnUnitButton2()
	{
		_barracks.UnitRequested(1, _cellCoords);
	}
	private void OnUnitButton3()
	{
		
	}
}
