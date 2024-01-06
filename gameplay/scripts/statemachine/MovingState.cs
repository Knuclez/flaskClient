using Godot;
using System;

public partial class MovingState : State
{
	[Export] private State _idleState;

	[Export] private CellManager _cellManager;
	[Export] private TileMap _tileMap;

	public override State ProcessInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mb)
		{
			//chequeo que no este apretado asi solo toma el release y no hace doble llamada
			if (!mb.IsPressed())
			{
				Vector2I clickedCell = _tileMap.LocalToMap(mb.GlobalPosition);
				Vector2 pos = _tileMap.MapToLocal(clickedCell);
				_cellManager.MoveActiveOcupant(clickedCell, pos);
				return _idleState;
			}
		}

		return null;
	}
}
