using Godot;
using System;
using Godot.Collections;

public partial class Cell : Node2D
{
	[Export] private string _tag;
	private Vector2I _cellCoords;

	public void Init(ProtoCell proto)
	{
		
		Name = proto.position;
		string[] coords = proto.position.Split(',');
		int x = int.Parse(coords[0]);
		int y = int.Parse(coords[1]);
		_cellCoords = new Vector2I(x, y);
		
		LoadOcupants(proto.ocupants);

	}


	public void LoadOcupants(Array<string> ocupants)
	{
		bool reWriteSprite = ocupants.Count == 1;
		Array<string> copy = ocupants;
		
		//si un ocupante previamente existente esta en la nueva lista de ocupantes (se queda en la cell), sacalo de 
		//la lista que vas a instanciar. Y si un ocupante existente no estan en la lista nueva, eliminalo del juego
		foreach (var child in GetChildren())
		{
			if (child is Ocupant ocu)
			{
				if (copy.Contains(ocu.Name))
				{
					copy.Remove(ocu.Name);
				}
				else
				{
					ocu.QueueFree();
				}
			}	
		}
		
		//instancian los ocupantes nuevos
		PackedScene ocupantScene = ResourceLoader.Load<PackedScene>("res://gameplay/ocupant.tscn");
		foreach (var ocu in copy)
		{
			Ocupant ocuInstance = ocupantScene.Instantiate<Ocupant>();
			ocuInstance.Init(ocu);
			AddChild(ocuInstance);
			ocuInstance.TreeExited += OnOcupantDeletion;
			ocuInstance.FinishedLoad += tag =>
			{
				if (reWriteSprite)
				{
					GetNode<Sprite2D>("Sprite2D").Texture = ResourceLoader.Load<Texture2D>($"res://gameplay/art/{tag}.png");
				}
			};

			//custom testing code
			//GetNode<Button>("Button").ButtonUp += ocuInstance.OnCustomButton;
		}
		
	}

	public Vector2I GetCellCoords()
	{
		return _cellCoords;
	}

	public int GetX()
	{
		return _cellCoords.X;
	}

	public int GetY()
	{
		return _cellCoords.Y;
	}


	private void OnOcupantDeletion()
	{
		bool delete = true;
		foreach (var child in GetChildren())
		{
			if (child is Ocupant)
			{
				delete = false;
				break;
			}	
		}

		if (delete)
		{
			QueueFree();
		}
	}

	public Array<Ocupant> GetOcupants()
	{
		Array<Ocupant> res = new Array<Ocupant>();
		foreach (var child in GetChildren())
		{
			if (child is Ocupant ocu)
			{
				res.Add(ocu);	
			}
		}

		return res;
	}
}
