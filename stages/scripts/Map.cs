using Godot;
using Godot.Collections;
using Newtonsoft.Json;

public partial class Map : Node
{
	private CellManager _cellManager;
	private HttpRequest _requester;
	public override void _Ready()
	{
		_cellManager = GetNode<CellManager>("CellManager");
		GetNode<StateMachine>("StateMachine").Init();
		_requester = GetNode<HttpRequest>("HTTPRequest");
		_requester.RequestCompleted += OnRequestCompleted;

		RequestCellInfo();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void OnRefreshButton()
	{
		RequestCellInfo();
	}
	
	private void RequestCellInfo()
	{
		_requester.Request("http://127.0.0.1:8000/cells");
	}

	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		GD.Print(responseCode);
		string stringedBody = System.Text.Encoding.UTF8.GetString(body);
		Array<ProtoCell> jason = Newtonsoft.Json.JsonConvert.DeserializeObject<Array<ProtoCell>>(stringedBody);
		_cellManager.LoadCells(jason);
	}
}
