using Godot;
using System;

public partial class InCellState : State 
{
    [Export] private State _idleState;
    [Export] private State _movingState;
    
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
				//Vector2 pos = _tileMap.MapToLocal(clickedCell);
				_cellManager.MakeActiveCell(clickedCell);
			}
		}
        return null;
    }

    public override State CustomEvent(string evento)
    {
	    if (evento == "close_button")
	    {
		    _cellManager.MakeActiveCell(new Vector2I(-404,-404));
		    return _idleState;
	    }
	    else if (evento == "moving_button")
	    {
		    return _movingState;
	    }
	    return null;
    }
}
