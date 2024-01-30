using Godot;
using System;
using Newtonsoft.Json;

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
		string stringedBody = System.Text.Encoding.UTF8.GetString(body);
		if (stringedBody == "no-user")
		{
			PopUIAlert("Wrong credentials");
			return;
		}

		ProtoUser pUser = JsonConvert.DeserializeObject<ProtoUser>(stringedBody);
		User userSingleton = GetNode<User>("/root/User");
		userSingleton.Init(pUser);

		GetTree().ChangeSceneToFile("res://stages/landing.tscn");
	}

	private void OnButtonUp()
	{
		string texto = GetNode<LineEdit>("Control/user_line").Text;
		if (texto == "")
		{
			return;
		}
		string[] headers = new string[] {"Content-Type: text/html"};
		_requester.Request("http://127.0.0.1:8000/users", headers, HttpClient.Method.Post, texto);
	}

	private async void PopUIAlert(string alertText)
	{
		Label alertLabel = GetNode<Label>("Control/alert");
		alertLabel.Text = alertText;
		alertLabel.Visible = true;
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
		alertLabel.Visible = false;
	}
}
