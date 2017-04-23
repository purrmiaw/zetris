using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameTile
{
    public GameTile()
    {
        GameUnits = new List<GameUnit>();
    }

    public bool IsOccupied()
    {
        if (GameUnits == null) { return false; }
        if (GameUnits.Count() == 0) { return false; }
        return true;
    }

    public int GameObjectInstanceId { get; set; }

    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    // Collection
    public ICollection<GameUnit> GameUnits { get; set; }
}
