using Godot;
using System;
using Godot.Collections;

public partial class Ocupant : Node
{
	[Signal]
	public delegate void FinishedLoadEventHandler(string tag);
	
	private HttpRequest _requester;
	private int _id;
	private Array<int> _flags;
	private string _status;
	private int _ownerId;
	private int _hp;
	private int _damage;
	public string Tag { get; set; }
	public void Init(string id)
	{
		Name = id;
		_id = int.Parse(id);
		
	}

	public override void _Ready()
	{
		_requester = GetNode<HttpRequest>("HTTPRequest");
		_requester.Request($"http://127.0.0.1:8000/ocupants/{_id}");
		_requester.RequestCompleted += OnRequestCompleted;
	}

	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		string stringedBody = System.Text.Encoding.UTF8.GetString(body);
		ProtoOcupant pOcupant = Newtonsoft.Json.JsonConvert.DeserializeObject<ProtoOcupant>(stringedBody);
		Tag = pOcupant.type;
		_flags = pOcupant.flags;
		_status = pOcupant.status;
		_ownerId = pOcupant.owner_id;
		_damage = pOcupant.damage;
		_hp = pOcupant.hp;
		GD.Print($"{Tag}, {_hp}");
		
		EmitSignal(SignalName.FinishedLoad, Tag);

		if (_ownerId == GetNode<User>("/root/User").GetUserId())
		{
			TileMap mapita = GetNode<TileMap>("/root/map/TileMap");
			mapita.SetCell(1,GetParent<Cell>().GetCellCoords(),1,new Vector2I(0,0));
		}
	}

	public void SetAsActiveOcupant()
	{
		GetNode<CellManager>("/root/map/CellManager").MakeActiveOcupant(_id);
	}

	public void AttackThisOcupant()
	{
		GetNode<BattleManager>("/root/map/BattleManager").FightOcupant(_ownerId);
	}
	public int GetFlag(int position)
	{
		return _flags[position];
	}

	public int GetOwnerId()
	{
		return _ownerId;
	}

	public int GetDamage()
	{
		return _damage;
	}
}
