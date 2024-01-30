using Godot;
using System;
using Godot.Collections;
using Newtonsoft.Json;

public partial class Barracks : Node
{
	private HttpRequest _requester;
	public override void _Ready()
	{
		_requester = GetNode<HttpRequest>("HTTPRequest");
		_requester.RequestCompleted += OnRequestCompleted;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UnitRequested(int unit, Vector2I spawnPoint)
	{
		string[] headers = new string[] {"Content-Type: application/json"};
		Dictionary<string, string> dicc = new Dictionary<string, string>();
		dicc.Add("model_id", $"{unit}");
		dicc.Add("cell_id", $"{spawnPoint.X},{spawnPoint.Y}");
		dicc.Add("owner_id", $"{GetNode<User>("/root/User").GetUserId()}");
		string body = JsonConvert.SerializeObject(dicc);
		_requester.Request("http://127.0.0.1:8000/ocupants", headers, HttpClient.Method.Post, body);
	}


	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		GD.Print(result);
	}
}
