using Godot;
using System;
using Godot.Collections;
using Newtonsoft.Json;

public partial class CellManager : Node
{
    [Export] private InMapGUI _guiManager; 
	private Cell _activeCell;
	private Ocupant _activeOcupant;
	private Ocupant _agressedOcupant;
	public override void _Ready()
	{
		_activeCell = null;
		_activeOcupant = null;

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
		_activeOcupant = GetNode<Ocupant>($"{_activeCell.Name}/{ocuId}");
	}

	public Ocupant GetActiveOcupant()
	{
		return _activeOcupant;
	}

	public Vector2I GetActiveCell()
	{
		return _activeCell.GetCellCoords();
	}

	public Cell GetActiveCellTyped()
	{
		return _activeCell;
	}
	
	public void OrderOcupantMovement(Vector2I newParentCell)
	{
		_guiManager.CloseOpenPanels();
		
		MoveReq requester = GetNode<MoveReq>("MoveReq");
		requester.RequestMoveAction(new MoveCosas(_activeOcupant.Name,
                                    					 _activeCell.GetCellCoords(),
														newParentCell));
	}

	public void LoadCells(Array<ProtoCell> cellToLoad)
	{
		
		PackedScene cellScene = ResourceLoader.Load<PackedScene>("res://gameplay/cell.tscn");
		TileMap tilemap = GetNode<TileMap>("/root/map/TileMap");
		
		foreach (ProtoCell cellTL in cellToLoad)
		{
			if (CellAlreadyExists(cellTL.position))
			{
				GetNode<Cell>(cellTL.position).LoadOcupants(cellTL.ocupants);	
			}
			else
			{
				Cell celInstance = cellScene.Instantiate<Cell>();
				celInstance.Init(cellTL);
				celInstance.GlobalPosition = tilemap.MapToLocal(celInstance.GetCellCoords());
				AddChild(celInstance);
			}
			
		}	
		
	}
	
	private bool CellAlreadyExists(string position)
	{
		if (GetNodeOrNull(position) != null)
		{
			return true;
		}

		return false;
	}

}
