using Godot;
using System;
using Godot.Collections;

public partial class ProtoOcupant : Node
{
    public int id { get; set; }
    public string type { get; set; }
    public Array<int> flags { get; set; }
    //Los flags son 1 positivo, 0 negativo
    //[sePuedeMover, puedeProducirUnidades]
    public int damage { get; set; }
    public int hp { get; set; }
    public string status { get; set; }
    public int owner_id{get; set; }
}
