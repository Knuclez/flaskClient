using Godot;
using System;
using Godot.Collections;

public partial class CellManager : Node
{
    [Export] private InMapGUI _guiManager; 
	private Cell _activeCell;
	private int _activeOcupant;
	public override void _Ready()
	{
		_activeCell = null;
		_activeOcupant = -1;
	}

	public void MakeActiveCell(Vector2I clickedCell)
	{
		GD.Print(clickedCell);
		string curatedName = $"{clickedCell.X},{clickedCell.Y}";
		_activeCell = GetNodeOrNull<Cell>(curatedName);
		if (_activeCell == null)
		{
			_guiManager.CloseOpenPanels();
		}
		else
		{
			_guiManager.CloseOpenPanels();
			_guiManager.OpenCellPanel(_activeCell);
		}
	}

	public void MakeActiveOcupant(int ocuId)
	{
		_activeOcupant = ocuId;
	}
	public void MoveActiveOcupant(Vector2I newParentCell, Vector2 newPosition)
	{
		_guiManager.CloseOpenPanels();
		
		Ocupant ocu = GetNode<Ocupant>($"{_activeCell.Name}/{_activeOcupant}");
		Cell newParentNode = GetNodeOrNull<Cell>($"{newParentCell.X},{newParentCell.Y}");
		if (newParentNode == null)
		{
			PackedScene cellScene = ResourceLoader.Load<PackedScene>("res://gameplay/cell.tscn");
			newParentNode = cellScene.Instantiate<Cell>();
			newParentNode.Name = $"{newParentCell.X},{newParentCell.Y}";
			AddChild(newParentNode);
			newParentNode.Position = newPosition;
		}
		ocu.Reparent(GetNode($"{newParentNode.Name}"));
		
		DeleteActiveCellIfNoChildren();
		
		_activeCell = null;
		_activeOcupant = -1;
	}

	public void LoadCells(Array<ProtoCell> cellToLoad)
	{
		PackedScene cellScene = ResourceLoader.Load<PackedScene>("res://gameplay/cell.tscn");
		TileMap tilemap = GetNode<TileMap>("/root/map/TileMap");
		foreach (var cellTL in cellToLoad)
		{
			Cell celInstance = cellScene.Instantiate<Cell>();
			celInstance.Init(cellTL);
			celInstance.GlobalPosition = tilemap.MapToLocal(celInstance.GetCellCoords());
			AddChild(celInstance);
			
		}	
	}

	private void DeleteActiveCellIfNoChildren()
	{
		//count igual a uno por q siempre esta el sprite
		if (_activeCell.GetChildren().Count == 1)
		{
			_activeCell.QueueFree();	
		}
	}
}
