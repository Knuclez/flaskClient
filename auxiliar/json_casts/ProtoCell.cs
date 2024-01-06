using Godot;
using Godot.Collections;

public partial class ProtoCell : Node
{
	public Dictionary<string, string> _id { get; set; }
	public string position { get; set; }
	public Array<string> ocupants { get; set; }
}
