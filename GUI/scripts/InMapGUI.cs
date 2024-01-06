using Godot;
using System;

public partial class InMapGUI : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OpenCellPanel(Cell cell)
	{
		PackedScene ocupantActions = ResourceLoader.Load<PackedScene>("res://GUI/ocu_panel.tscn");
		int i = 0;
		foreach (var child in cell.GetChildren())
		{
			if (child is Ocupant ocu)
			{
				Control cellActions = GetNode<Control>("cellControl");
				OcupantActions ocuActPanel = ocupantActions.Instantiate<OcupantActions>();
				ocuActPanel.Init(ocu);
				ocuActPanel.Position = new Vector2(5, (42 * i) + 5);
				cellActions.AddChild(ocuActPanel);
				i++;
			}
		}
		GetNode<Control>("cellControl").Visible = true;
	}

	public void CloseOpenPanels()
	{
		Control cellControl = GetNode<Control>("cellControl");
		cellControl.Visible = false;
		foreach (var child in cellControl.GetChildren())
		{
			if (child is OcupantActions ocuActs)
			{
				ocuActs.QueueFree();
			}	
		}
	}
}
