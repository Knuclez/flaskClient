using Godot;
using System;

public partial class Structs : Node
{
	//No real use hehe
}

public struct MoveCosas
{
	public string ocupant { get; set; }
	public string origin { get; set; }
	public string destiny { get; set; }
	public MoveCosas(string ocuNamex, Vector2I originx, Vector2I destinyx)
	{
		ocupant = ocuNamex;
		origin = $"{originx.X},{originx.Y}";
		destiny = $"{destinyx.X},{destinyx.Y}";
	}
}
