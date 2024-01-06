using Godot;
using System;

public partial class Landing : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Label userTag = GetNode<Label>("Control/user_tag");
		userTag.Text = GetNode<User>("/root/User").GetUsername();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnMapButtonUp()
	{
		GetTree().ChangeSceneToFile("res://stages/map.tscn");
	}
}
