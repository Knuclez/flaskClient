using Godot;
using System;

public partial class User : Node
{
	private string _username;

	public string GetUsername()
	{
		return _username;
	}
	public void SetUsername(string usern)
	{
		_username = usern;
	}
}
