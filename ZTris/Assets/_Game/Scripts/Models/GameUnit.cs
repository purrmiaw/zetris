using UnityEngine;
using System.Collections;

public class GameUnit {

    //public GameObject Unit { get; set; }

    public int GameObjectInstanceId { get; set; }
    /// <summary>
    /// Local Id. Only to be used within the same GamePiece.
    /// </summary>
    public int LocalId { get; set; }
    public int RelativePositionX { get; set; }
    public int RelativePositionY { get; set; }
    public int RelativePositionZ { get; set; }

    public int RelativePosition0X { get; set; }
    public int RelativePosition0Y { get; set; }
    public int RelativePosition0Z { get; set; }

    public int RelativePosition270X { get; set; }
    public int RelativePosition270Y { get; set; }
    public int RelativePosition270Z { get; set; }

    public int RelativePosition180X { get; set; }
    public int RelativePosition180Y { get; set; }
    public int RelativePosition180Z { get; set; }

    // collection
    public GamePiece GamePiece { get; set; }
    public GameTile GameTile { get; set; }
}
