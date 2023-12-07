using Godot;
using System;

public partial class Gateway : Node
{
	private HttpRequest _requester;
	public override void _Ready()
	{
		_requester = GetNode<HttpRequest>("HTTPRequest");
		_requester.RequestCompleted += OnRequestCompleted;
	}

	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		GD.Print(responseCode);
	}

	private void OnButtonUp()
	{
		//seguir aca
		//string[] headers = 
		//_requester.Request("127.0.0.1:8000/chat/canario_brutal", headers, HttpClient.Method.Post, json);
	}
}
