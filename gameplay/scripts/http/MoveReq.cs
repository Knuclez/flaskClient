using Godot;
using System;
using Newtonsoft.Json;

public partial class MoveReq : Node
{
	private HttpRequest _requester;
	public override void _Ready()
	{
		_requester = GetNode<HttpRequest>("HTTPRequest");
		_requester.RequestCompleted += OnRequestCompleted;
	}

	public void RequestMoveAction(MoveCosas cositas)
	{
		
		string jason = PrepareMovementActionJson(cositas);
		string[] headers = new string[] {"Content-Type: application/json"};
		_requester.Request("http://127.0.0.1:8000/actions", headers, HttpClient.Method.Post, jason);
	}
	private string PrepareMovementActionJson(MoveCosas ojeto)
	{
		string res = JsonConvert.SerializeObject(ojeto);
		return res;
	}
	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		if (responseCode != 200)
		{
			string stringedBody = System.Text.Encoding.UTF8.GetString(body);
			GD.Print(stringedBody);
		}	
	}
}
