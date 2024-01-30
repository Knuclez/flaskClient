using Godot;
using System;
using System.Linq;
using Godot.Collections;

public partial class BattleManager : Node
{
    private CellManager _cellManager;

    public override void _Ready()
    {
        _cellManager = GetNode<CellManager>("/root/map/CellManager");
    }

    public void FightOcupant(int attckedPlayerid)
    {
        Array<Ocupant> ocus = _cellManager.GetActiveCellTyped().GetOcupants();
        foreach (var ocu in ocus)
        {
            if (ocu.GetOwnerId() == GetNode<User>("/root/User").GetUserId())
            {
                GD.Print("danio atacante");
                GD.Print(ocu.GetDamage());
            }
            if (ocu.GetOwnerId() == attckedPlayerid)
            {
                GD.Print("danio defensor");
                GD.Print(ocu.GetDamage());
            }
        }
    }
}
