using Godot;
using System;
using Godot.Collections;

public partial class Cell : Node2D
{
	[Export] private string _tag;
	private Vector2I _cellCoords;

	public void Init(ProtoCell proto)
	{
		PackedScene ocupantScene = ResourceLoader.Load<PackedScene>("res://gameplay/ocupant.tscn");
		
		Name = proto.position;
		string[] coords = proto.position.Split(',');
		int x = int.Parse(coords[0]);
		int y = int.Parse(coords[1]);
		_cellCoords = new Vector2I(x, y);

		foreach (var ocu in proto.ocupants)
		{
			Ocupant ocuInstance = ocupantScene.Instantiate<Ocupant>();
			ocuInstance.Init(ocu);
			AddChild(ocuInstance);
			
			//custom testing code
			//GetNode<Button>("Button").ButtonUp += ocuInstance.OnCustomButton;
		}
	}
	public override void _Process(double delta)
	{
	}

	public Vector2I GetCellCoords()
	{
		return _cellCoords;
	}

	public int GetX()
	{
		return _cellCoords.X;
	}

	public int GetY()
	{
		return _cellCoords.Y;
	}
}
