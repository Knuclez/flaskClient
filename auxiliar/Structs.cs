using Godot;
using System;

public partial class Structs : Node
{
	//No real use hehe
}

public struct MoveCosas
{
	public int Turn { get; set; }
	public string OcuName { get; set; }
	public string Origin { get; set; }
	public string Destiny { get; set; }
	public MoveCosas(int turn, string ocuName, Vector2I origin, Vector2I destiny)
	{
		Turn = turn;
		OcuName = ocuName;
		Origin = $"{origin.X},{origin.Y}";
		Destiny = $"{destiny.X},{destiny.Y}";
	}
}
