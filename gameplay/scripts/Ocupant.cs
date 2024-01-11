using Godot;
using System;

public partial class Ocupant : Node
{
	private HttpRequest _requester;
	private int _id;
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
		GD.Print("request completada");
		string stringedBody = System.Text.Encoding.UTF8.GetString(body);
		ProtoOcupant pOcupant = Newtonsoft.Json.JsonConvert.DeserializeObject<ProtoOcupant>(stringedBody);
		Tag = pOcupant.type;
	}

	public void SetAsActiveOcupant()
	{
		GetNode<CellManager>("/root/map/CellManager").MakeActiveOcupant(_id);
	}
}
