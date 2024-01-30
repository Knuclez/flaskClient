using Godot;
using System;

public partial class User : Node
{
	private int _userId;
	private string _username;

	public void Init(ProtoUser pUser)
	{
		_userId = pUser._id;
		_username = pUser.username;
		GD.Print(_userId);
	}
	
	public string GetUsername()
	{
		return _username;
	}

	public int GetUserId()
	{
		return _userId;
	}
}
