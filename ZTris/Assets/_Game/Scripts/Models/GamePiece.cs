using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GamePiece
{
    // GamePiece relative position is assumed at 0,0.
    public GamePiece()
    {
        this.GameUnits = new List<GameUnit>();
        this.CurrentGamePieceOrientation = GamePieceOrientation.Default;
    }

    public ICollection<GameUnit> GameUnits;
    public GamePieceOrientation CurrentGamePieceOrientation;

    // Determine the coordinate to use as relative position calculation basis. 
    // Basically, get the location of at least one unit
    public int GetGamePiecePositionX()
    {
        GameUnit defaultGameUnit = GameUnits.FirstOrDefault();

        if (defaultGameUnit == null)
        {
            return 0;
        }
        if (defaultGameUnit.GameTile == null)
        {
            return 0;
        }

        switch (CurrentGamePieceOrientation)
        {
            case GamePieceOrientation.Default:
                return defaultGameUnit.GameTile.X - defaultGameUnit.RelativePositionX;
            case GamePieceOrientation.Degree0:
                return defaultGameUnit.GameTile.X - defaultGameUnit.RelativePosition0X;
            case GamePieceOrientation.Degree270:
                return defaultGameUnit.GameTile.X - defaultGameUnit.RelativePosition270X;
            case GamePieceOrientation.Degree180:
                return defaultGameUnit.GameTile.X - defaultGameUnit.RelativePosition180X;
        }

        return 0;
    }

    public int GetGamePiecePositionY()
    {
        GameUnit defaultGameUnit = GameUnits.FirstOrDefault();

        if (defaultGameUnit == null)
        {
            return 0;
        }
        if (defaultGameUnit.GameTile == null)
        {
            return 0;
        }

        switch (CurrentGamePieceOrientation)
        {
            case GamePieceOrientation.Default:
                return defaultGameUnit.GameTile.Y - defaultGameUnit.RelativePositionY;
            case GamePieceOrientation.Degree0:
                return defaultGameUnit.GameTile.Y - defaultGameUnit.RelativePosition0Y;
            case GamePieceOrientation.Degree270:
                return defaultGameUnit.GameTile.Y - defaultGameUnit.RelativePosition270Y;
            case GamePieceOrientation.Degree180:
                return defaultGameUnit.GameTile.Y - defaultGameUnit.RelativePosition180Y;
        }

        return 0;
    }

    public int GetGamePiecePositionZ()
    {
        GameUnit defaultGameUnit = GameUnits.FirstOrDefault();

        if (defaultGameUnit == null)
        {
            return 0;
        }
        if (defaultGameUnit.GameTile == null)
        {
            return 0;
        }

        switch (CurrentGamePieceOrientation)
        {
            case GamePieceOrientation.Default:
                return defaultGameUnit.GameTile.Z - defaultGameUnit.RelativePositionZ;
            case GamePieceOrientation.Degree0:
                return defaultGameUnit.GameTile.Z - defaultGameUnit.RelativePosition0Z;
            case GamePieceOrientation.Degree270:
                return defaultGameUnit.GameTile.Z - defaultGameUnit.RelativePosition270Z;
            case GamePieceOrientation.Degree180:
                return defaultGameUnit.GameTile.Z - defaultGameUnit.RelativePosition180Z;
        }

        return 0;
    }
}

public enum GamePieceOrientation
{
    Default, // 90 degree
    Degree0, // 0 degree
    Degree270,
    Degree180
}

public class GamePieceOne : GamePiece
{
    public GamePieceOne()
    {
        GameUnit gameUnit = new GameUnit();
        gameUnit.LocalId = 1;
        gameUnit.RelativePositionX = 0;
        gameUnit.RelativePositionY = 0;
        gameUnit.RelativePositionZ = 0;
        gameUnit.RelativePosition0X = 0;
        gameUnit.RelativePosition0Y = 0;
        gameUnit.RelativePosition0Z = 0;
        gameUnit.RelativePosition270X = 0;
        gameUnit.RelativePosition270Y = 0;
        gameUnit.RelativePosition270Z = 0;
        gameUnit.RelativePosition180X = 0;
        gameUnit.RelativePosition180Y = 0;
        gameUnit.RelativePosition180Z = 0;
        gameUnit.GamePiece = this;

        this.GameUnits.Add(gameUnit);

    }
}

public class GamePieceTwoDown : GamePiece
{
    public GamePieceTwoDown()
    {
        GameUnit gameUnit = new GameUnit();
        gameUnit.LocalId = 1;
        gameUnit.RelativePositionX = 0;
        gameUnit.RelativePositionY = 0;
        gameUnit.RelativePositionZ = 0;
        gameUnit.RelativePosition0X = 1;
        gameUnit.RelativePosition0Y = -1;
        gameUnit.RelativePosition0Z = 0;
        gameUnit.RelativePosition270X = 0;
        gameUnit.RelativePosition270Y = -2;
        gameUnit.RelativePosition270Z = 0;
        gameUnit.RelativePosition180X = -1;
        gameUnit.RelativePosition180Y = -1;
        gameUnit.RelativePosition180Z = 0;
        gameUnit.GamePiece = this;

        GameUnit gameUnit2 = new GameUnit();
        gameUnit2.LocalId = 2;
        gameUnit2.RelativePositionX = 0;
        gameUnit2.RelativePositionY = -1;
        gameUnit2.RelativePositionZ = 0;
        gameUnit2.RelativePosition0X = 0;
        gameUnit2.RelativePosition0Y = -1;
        gameUnit2.RelativePosition0Z = 0;
        gameUnit2.RelativePosition270X = 0;
        gameUnit2.RelativePosition270Y = -1;
        gameUnit2.RelativePosition270Z = 0;
        gameUnit2.RelativePosition180X = 0;
        gameUnit2.RelativePosition180Y = -1;
        gameUnit2.RelativePosition180Z = 0;
        gameUnit2.GamePiece = this;

        this.GameUnits.Add(gameUnit);
        this.GameUnits.Add(gameUnit2);
    }

}

public class GamePieceL : GamePiece
{
    public GamePieceL()
    {
        GameUnit gameUnit = new GameUnit();
        gameUnit.LocalId = 1;
        gameUnit.RelativePositionX = 0;
        gameUnit.RelativePositionY = 0;
        gameUnit.RelativePositionZ = 0;
        gameUnit.RelativePosition0X = 2;
        gameUnit.RelativePosition0Y = 0;
        gameUnit.RelativePosition0Z = 0;
        gameUnit.RelativePosition270X = 1;
        gameUnit.RelativePosition270Y = -2;
        gameUnit.RelativePosition270Z = 0;
        gameUnit.RelativePosition180X = 0;
        gameUnit.RelativePosition180Y = -1;
        gameUnit.RelativePosition180Z = 0;
        gameUnit.GamePiece = this;

        GameUnit gameUnit2 = new GameUnit();
        gameUnit2.LocalId = 2;
        gameUnit2.RelativePositionX = 0;
        gameUnit2.RelativePositionY = -1;
        gameUnit2.RelativePositionZ = 0;
        gameUnit2.RelativePosition0X = 1;
        gameUnit2.RelativePosition0Y = 0;
        gameUnit2.RelativePosition0Z = 0;
        gameUnit2.RelativePosition270X = 1;
        gameUnit2.RelativePosition270Y = -1;
        gameUnit2.RelativePosition270Z = 0;
        gameUnit2.RelativePosition180X = 1;
        gameUnit2.RelativePosition180Y = -1;
        gameUnit2.RelativePosition180Z = 0;
        gameUnit2.GamePiece = this;

        GameUnit gameUnit3 = new GameUnit();
        gameUnit3.LocalId = 3;
        gameUnit3.RelativePositionX = 0;
        gameUnit3.RelativePositionY = -2;
        gameUnit3.RelativePositionZ = 0;
        gameUnit3.RelativePosition0X = 0;
        gameUnit3.RelativePosition0Y = 0;
        gameUnit3.RelativePosition0Z = 0;
        gameUnit3.RelativePosition270X = 1;
        gameUnit3.RelativePosition270Y = 0;
        gameUnit3.RelativePosition270Z = 0;
        gameUnit3.RelativePosition180X = 2;
        gameUnit3.RelativePosition180Y = -1;
        gameUnit3.RelativePosition180Z = 0;
        gameUnit3.GamePiece = this;

        GameUnit gameUnit4 = new GameUnit();
        gameUnit4.LocalId = 4;
        gameUnit4.RelativePositionX = 1;
        gameUnit4.RelativePositionY = -2;
        gameUnit4.RelativePositionZ = 0;
        gameUnit4.RelativePosition0X = 0;
        gameUnit4.RelativePosition0Y = -1;
        gameUnit4.RelativePosition0Z = 0;
        gameUnit4.RelativePosition270X = 0;
        gameUnit4.RelativePosition270Y = 0;
        gameUnit4.RelativePosition270Z = 0;
        gameUnit4.RelativePosition180X = 2;
        gameUnit4.RelativePosition180Y = 0;
        gameUnit4.RelativePosition180Z = 0;
        gameUnit4.GamePiece = this;

        this.GameUnits.Add(gameUnit);
        this.GameUnits.Add(gameUnit2);
        this.GameUnits.Add(gameUnit3);
        this.GameUnits.Add(gameUnit4);
    }
}

public class GamePieceLInverse : GamePiece
{
    public GamePieceLInverse()
    {
        GameUnit gameUnit = new GameUnit();
        gameUnit.LocalId = 1;
        gameUnit.RelativePositionX = 1;
        gameUnit.RelativePositionY = 0;
        gameUnit.RelativePositionZ = 0;
        gameUnit.RelativePosition0X = 2;
        gameUnit.RelativePosition0Y = -1;
        gameUnit.RelativePosition0Z = 0;
        gameUnit.RelativePosition270X = 0;
        gameUnit.RelativePosition270Y = -2;
        gameUnit.RelativePosition270Z = 0;
        gameUnit.RelativePosition180X = 0;
        gameUnit.RelativePosition180Y = 0;
        gameUnit.RelativePosition180Z = 0;
        gameUnit.GamePiece = this;

        GameUnit gameUnit2 = new GameUnit();
        gameUnit2.LocalId = 2;
        gameUnit2.RelativePositionX = 1;
        gameUnit2.RelativePositionY = -1;
        gameUnit2.RelativePositionZ = 0;
        gameUnit2.RelativePosition0X = 1;
        gameUnit2.RelativePosition0Y = -1;
        gameUnit2.RelativePosition0Z = 0;
        gameUnit2.RelativePosition270X = 0;
        gameUnit2.RelativePosition270Y = -1;
        gameUnit2.RelativePosition270Z = 0;
        gameUnit2.RelativePosition180X = 1;
        gameUnit2.RelativePosition180Y = 0;
        gameUnit2.RelativePosition180Z = 0;
        gameUnit2.GamePiece = this;

        GameUnit gameUnit3 = new GameUnit();
        gameUnit3.LocalId = 3;
        gameUnit3.RelativePositionX = 1;
        gameUnit3.RelativePositionY = -2;
        gameUnit3.RelativePositionZ = 0;
        gameUnit3.RelativePosition0X = 0;
        gameUnit3.RelativePosition0Y = -1;
        gameUnit3.RelativePosition0Z = 0;
        gameUnit3.RelativePosition270X = 0;
        gameUnit3.RelativePosition270Y = 0;
        gameUnit3.RelativePosition270Z = 0;
        gameUnit3.RelativePosition180X = 2;
        gameUnit3.RelativePosition180Y = 0;
        gameUnit3.RelativePosition180Z = 0;
        gameUnit3.GamePiece = this;

        GameUnit gameUnit4 = new GameUnit();
        gameUnit4.LocalId = 4;
        gameUnit4.RelativePositionX = 0;
        gameUnit4.RelativePositionY = -2;
        gameUnit4.RelativePositionZ = 0;
        gameUnit4.RelativePosition0X = 0;
        gameUnit4.RelativePosition0Y = 0;
        gameUnit4.RelativePosition0Z = 0;
        gameUnit4.RelativePosition270X = 1;
        gameUnit4.RelativePosition270Y = 0;
        gameUnit4.RelativePosition270Z = 0;
        gameUnit4.RelativePosition180X = 2;
        gameUnit4.RelativePosition180Y = -1;
        gameUnit4.RelativePosition180Z = 0;
        gameUnit4.GamePiece = this;

        this.GameUnits.Add(gameUnit);
        this.GameUnits.Add(gameUnit2);
        this.GameUnits.Add(gameUnit3);
        this.GameUnits.Add(gameUnit4);
    }
}

public class GamePieceZ : GamePiece
{
    public GamePieceZ()
    {
        GameUnit gameUnit = new GameUnit();
        gameUnit.LocalId = 1;
        gameUnit.RelativePositionX = 1;
        gameUnit.RelativePositionY = 0;
        gameUnit.RelativePositionZ = 0;
        gameUnit.RelativePosition0X = 2;
        gameUnit.RelativePosition0Y = -1;
        gameUnit.RelativePosition0Z = 0;
        gameUnit.RelativePosition270X = 1;
        gameUnit.RelativePosition270Y = 0;
        gameUnit.RelativePosition270Z = 0;
        gameUnit.RelativePosition180X = 2;
        gameUnit.RelativePosition180Y = -1;
        gameUnit.RelativePosition180Z = 0;
        gameUnit.GamePiece = this;

        GameUnit gameUnit2 = new GameUnit();
        gameUnit2.LocalId = 2;
        gameUnit2.RelativePositionX = 1;
        gameUnit2.RelativePositionY = -1;
        gameUnit2.RelativePositionZ = 0;
        gameUnit2.RelativePosition0X = 1;
        gameUnit2.RelativePosition0Y = -1;
        gameUnit2.RelativePosition0Z = 0;
        gameUnit2.RelativePosition270X = 1;
        gameUnit2.RelativePosition270Y = -1;
        gameUnit2.RelativePosition270Z = 0;
        gameUnit2.RelativePosition180X = 1;
        gameUnit2.RelativePosition180Y = -1;
        gameUnit2.RelativePosition180Z = 0;
        gameUnit2.GamePiece = this;

        GameUnit gameUnit3 = new GameUnit();
        gameUnit3.LocalId = 3;
        gameUnit3.RelativePositionX = 0;
        gameUnit3.RelativePositionY = -1;
        gameUnit3.RelativePositionZ = 0;
        gameUnit3.RelativePosition0X = 1;
        gameUnit3.RelativePosition0Y = 0;
        gameUnit3.RelativePosition0Z = 0;
        gameUnit3.RelativePosition270X = 0;
        gameUnit3.RelativePosition270Y = -1;
        gameUnit3.RelativePosition270Z = 0;
        gameUnit3.RelativePosition180X = 1;
        gameUnit3.RelativePosition180Y = 0;
        gameUnit3.RelativePosition180Z = 0;
        gameUnit3.GamePiece = this;

        GameUnit gameUnit4 = new GameUnit();
        gameUnit4.LocalId = 4;
        gameUnit4.RelativePositionX = 0;
        gameUnit4.RelativePositionY = -2;
        gameUnit4.RelativePositionZ = 0;
        gameUnit4.RelativePosition0X = 0;
        gameUnit4.RelativePosition0Y = 0;
        gameUnit4.RelativePosition0Z = 0;
        gameUnit4.RelativePosition270X = 0;
        gameUnit4.RelativePosition270Y = -2;
        gameUnit4.RelativePosition270Z = 0;
        gameUnit4.RelativePosition180X = 0;
        gameUnit4.RelativePosition180Y = 0;
        gameUnit4.RelativePosition180Z = 0;
        gameUnit4.GamePiece = this;

        this.GameUnits.Add(gameUnit);
        this.GameUnits.Add(gameUnit2);
        this.GameUnits.Add(gameUnit3);
        this.GameUnits.Add(gameUnit4);
    }
}

public class GamePieceZInverse : GamePiece
{
    public GamePieceZInverse()
    {
        GameUnit gameUnit = new GameUnit();
        gameUnit.LocalId = 1;
        gameUnit.RelativePositionX = 0;
        gameUnit.RelativePositionY = 0;
        gameUnit.RelativePositionZ = 0;
        gameUnit.RelativePosition0X = 0;
        gameUnit.RelativePosition0Y = -1;
        gameUnit.RelativePosition0Z = 0;
        gameUnit.RelativePosition270X = 0;
        gameUnit.RelativePosition270Y = 0;
        gameUnit.RelativePosition270Z = 0;
        gameUnit.RelativePosition180X = 0;
        gameUnit.RelativePosition180Y = -1;
        gameUnit.RelativePosition180Z = 0;
        gameUnit.GamePiece = this;

        GameUnit gameUnit2 = new GameUnit();
        gameUnit2.LocalId = 2;
        gameUnit2.RelativePositionX = 0;
        gameUnit2.RelativePositionY = -1;
        gameUnit2.RelativePositionZ = 0;
        gameUnit2.RelativePosition0X = 1;
        gameUnit2.RelativePosition0Y = -1;
        gameUnit2.RelativePosition0Z = 0;
        gameUnit2.RelativePosition270X = 0;
        gameUnit2.RelativePosition270Y = -1;
        gameUnit2.RelativePosition270Z = 0;
        gameUnit2.RelativePosition180X = 1;
        gameUnit2.RelativePosition180Y = -1;
        gameUnit2.RelativePosition180Z = 0;
        gameUnit2.GamePiece = this;

        GameUnit gameUnit3 = new GameUnit();
        gameUnit3.LocalId = 3;
        gameUnit3.RelativePositionX = 1;
        gameUnit3.RelativePositionY = -1;
        gameUnit3.RelativePositionZ = 0;
        gameUnit3.RelativePosition0X = 1;
        gameUnit3.RelativePosition0Y = 0;
        gameUnit3.RelativePosition0Z = 0;
        gameUnit3.RelativePosition270X = 1;
        gameUnit3.RelativePosition270Y = -1;
        gameUnit3.RelativePosition270Z = 0;
        gameUnit3.RelativePosition180X = 1;
        gameUnit3.RelativePosition180Y = 0;
        gameUnit3.RelativePosition180Z = 0;
        gameUnit3.GamePiece = this;

        GameUnit gameUnit4 = new GameUnit();
        gameUnit4.LocalId = 4;
        gameUnit4.RelativePositionX = 1;
        gameUnit4.RelativePositionY = -2;
        gameUnit4.RelativePositionZ = 0;
        gameUnit4.RelativePosition0X = 2;
        gameUnit4.RelativePosition0Y = 0;
        gameUnit4.RelativePosition0Z = 0;
        gameUnit4.RelativePosition270X = 1;
        gameUnit4.RelativePosition270Y = -2;
        gameUnit4.RelativePosition270Z = 0;
        gameUnit4.RelativePosition180X = 2;
        gameUnit4.RelativePosition180Y = 0;
        gameUnit4.RelativePosition180Z = 0;
        gameUnit4.GamePiece = this;

        this.GameUnits.Add(gameUnit);
        this.GameUnits.Add(gameUnit2);
        this.GameUnits.Add(gameUnit3);
        this.GameUnits.Add(gameUnit4);
    }
}

public class GamePieceO : GamePiece
{
    public GamePieceO()
    {
        GameUnit gameUnit = new GameUnit();
        gameUnit.LocalId = 1;
        gameUnit.RelativePositionX = 0;
        gameUnit.RelativePositionY = 0;
        gameUnit.RelativePositionZ = 0;
        gameUnit.RelativePosition0X = 0;
        gameUnit.RelativePosition0Y = 0;
        gameUnit.RelativePosition0Z = 0;
        gameUnit.RelativePosition270X = 0;
        gameUnit.RelativePosition270Y = 0;
        gameUnit.RelativePosition270Z = 0;
        gameUnit.RelativePosition180X = 0;
        gameUnit.RelativePosition180Y = 0;
        gameUnit.RelativePosition180Z = 0;
        gameUnit.GamePiece = this;

        GameUnit gameUnit2 = new GameUnit();
        gameUnit2.LocalId = 2;
        gameUnit2.RelativePositionX = 1;
        gameUnit2.RelativePositionY = 0;
        gameUnit2.RelativePositionZ = 0;
        gameUnit2.RelativePosition0X = 1;
        gameUnit2.RelativePosition0Y = 0;
        gameUnit2.RelativePosition0Z = 0;
        gameUnit2.RelativePosition270X = 1;
        gameUnit2.RelativePosition270Y = 0;
        gameUnit2.RelativePosition270Z = 0;
        gameUnit2.RelativePosition180X = 1;
        gameUnit2.RelativePosition180Y = 0;
        gameUnit2.RelativePosition180Z = 0;
        gameUnit2.GamePiece = this;

        GameUnit gameUnit3 = new GameUnit();
        gameUnit3.LocalId = 3;
        gameUnit3.RelativePositionX = 0;
        gameUnit3.RelativePositionY = -1;
        gameUnit3.RelativePositionZ = 0;
        gameUnit3.RelativePosition0X = 0;
        gameUnit3.RelativePosition0Y = -1;
        gameUnit3.RelativePosition0Z = 0;
        gameUnit3.RelativePosition270X = 0;
        gameUnit3.RelativePosition270Y = -1;
        gameUnit3.RelativePosition270Z = 0;
        gameUnit3.RelativePosition180X = -1;
        gameUnit3.RelativePosition180Y = 0;
        gameUnit3.RelativePosition180Z = 0;
        gameUnit3.GamePiece = this;

        GameUnit gameUnit4 = new GameUnit();
        gameUnit4.LocalId = 4;
        gameUnit4.RelativePositionX = 1;
        gameUnit4.RelativePositionY = -1;
        gameUnit4.RelativePositionZ = 0;
        gameUnit4.RelativePosition0X = 1;
        gameUnit4.RelativePosition0Y = -1;
        gameUnit4.RelativePosition0Z = 0;
        gameUnit4.RelativePosition270X = 1;
        gameUnit4.RelativePosition270Y = -1;
        gameUnit4.RelativePosition270Z = 0;
        gameUnit4.RelativePosition180X = 1;
        gameUnit4.RelativePosition180Y = -1;
        gameUnit4.RelativePosition180Z = 0;
        gameUnit4.GamePiece = this;

        this.GameUnits.Add(gameUnit);
        this.GameUnits.Add(gameUnit2);
        this.GameUnits.Add(gameUnit3);
        this.GameUnits.Add(gameUnit4);
    }
}

public class GamePieceI : GamePiece
{
    public GamePieceI()
    {
        GameUnit gameUnit = new GameUnit();
        gameUnit.LocalId = 1;
        gameUnit.RelativePositionX = 0;
        gameUnit.RelativePositionY = 0;
        gameUnit.RelativePositionZ = 0;
        gameUnit.RelativePosition0X = 0;
        gameUnit.RelativePosition0Y = 0;
        gameUnit.RelativePosition0Z = 0;
        gameUnit.RelativePosition270X = 0;
        gameUnit.RelativePosition270Y = 0;
        gameUnit.RelativePosition270Z = 0;
        gameUnit.RelativePosition180X = 0;
        gameUnit.RelativePosition180Y = 0;
        gameUnit.RelativePosition180Z = 0;
        gameUnit.GamePiece = this;

        GameUnit gameUnit2 = new GameUnit();
        gameUnit2.LocalId = 2;
        gameUnit2.RelativePositionX = 0;
        gameUnit2.RelativePositionY = -1;
        gameUnit2.RelativePositionZ = 0;
        gameUnit2.RelativePosition0X = 1;
        gameUnit2.RelativePosition0Y = 0;
        gameUnit2.RelativePosition0Z = 0;
        gameUnit2.RelativePosition270X = 0;
        gameUnit2.RelativePosition270Y = -1;
        gameUnit2.RelativePosition270Z = 0;
        gameUnit2.RelativePosition180X = 1;
        gameUnit2.RelativePosition180Y = 0;
        gameUnit2.RelativePosition180Z = 0;
        gameUnit2.GamePiece = this;

        GameUnit gameUnit3 = new GameUnit();
        gameUnit3.LocalId = 3;
        gameUnit3.RelativePositionX = 0;
        gameUnit3.RelativePositionY = -2;
        gameUnit3.RelativePositionZ = 0;
        gameUnit3.RelativePosition0X = 2;
        gameUnit3.RelativePosition0Y = 0;
        gameUnit3.RelativePosition0Z = 0;
        gameUnit3.RelativePosition270X = 0;
        gameUnit3.RelativePosition270Y = -2;
        gameUnit3.RelativePosition270Z = 0;
        gameUnit3.RelativePosition180X = 2;
        gameUnit3.RelativePosition180Y = 0;
        gameUnit3.RelativePosition180Z = 0;
        gameUnit3.GamePiece = this;

        GameUnit gameUnit4 = new GameUnit();
        gameUnit4.LocalId = 4;
        gameUnit4.RelativePositionX = 0;
        gameUnit4.RelativePositionY = -3;
        gameUnit4.RelativePositionZ = 0;
        gameUnit4.RelativePosition0X = 3;
        gameUnit4.RelativePosition0Y = 0;
        gameUnit4.RelativePosition0Z = 0;
        gameUnit4.RelativePosition270X = 0;
        gameUnit4.RelativePosition270Y = -3;
        gameUnit4.RelativePosition270Z = 0;
        gameUnit4.RelativePosition180X = 3;
        gameUnit4.RelativePosition180Y = 0;
        gameUnit4.RelativePosition180Z = 0;
        gameUnit4.GamePiece = this;

        this.GameUnits.Add(gameUnit);
        this.GameUnits.Add(gameUnit2);
        this.GameUnits.Add(gameUnit3);
        this.GameUnits.Add(gameUnit4);
    }
}

public class GamePieceT : GamePiece
{
    public GamePieceT()
    {
        GameUnit gameUnit = new GameUnit();
        gameUnit.LocalId = 1;
        gameUnit.RelativePositionX = 0;
        gameUnit.RelativePositionY = 0;
        gameUnit.RelativePositionZ = 0;
        gameUnit.RelativePosition0X = 2;
        gameUnit.RelativePosition0Y = 0;
        gameUnit.RelativePosition0Z = 0;
        gameUnit.RelativePosition270X = 2;
        gameUnit.RelativePosition270Y = -1;
        gameUnit.RelativePosition270Z = 0;
        gameUnit.RelativePosition180X = 0;
        gameUnit.RelativePosition180Y = -2;
        gameUnit.RelativePosition180Z = 0;
        gameUnit.GamePiece = this;

        GameUnit gameUnit2 = new GameUnit();
        gameUnit2.LocalId = 2;
        gameUnit2.RelativePositionX = 1;
        gameUnit2.RelativePositionY = 0;
        gameUnit2.RelativePositionZ = 0;
        gameUnit2.RelativePosition0X = 2;
        gameUnit2.RelativePosition0Y = -1;
        gameUnit2.RelativePosition0Z = 0;
        gameUnit2.RelativePosition270X = 1;
        gameUnit2.RelativePosition270Y = -1;
        gameUnit2.RelativePosition270Z = 0;
        gameUnit2.RelativePosition180X = 0;
        gameUnit2.RelativePosition180Y = -1;
        gameUnit2.RelativePosition180Z = 0;
        gameUnit2.GamePiece = this;

        GameUnit gameUnit3 = new GameUnit();
        gameUnit3.LocalId = 3;
        gameUnit3.RelativePositionX = 2;
        gameUnit3.RelativePositionY = 0;
        gameUnit3.RelativePositionZ = 0;
        gameUnit3.RelativePosition0X = 2;
        gameUnit3.RelativePosition0Y = -2;
        gameUnit3.RelativePosition0Z = 0;
        gameUnit3.RelativePosition270X = 0;
        gameUnit3.RelativePosition270Y = -1;
        gameUnit3.RelativePosition270Z = 0;
        gameUnit3.RelativePosition180X = 0;
        gameUnit3.RelativePosition180Y = 0;
        gameUnit3.RelativePosition180Z = 0;
        gameUnit3.GamePiece = this;

        GameUnit gameUnit4 = new GameUnit();
        gameUnit4.LocalId = 4;
        gameUnit4.RelativePositionX = 1;
        gameUnit4.RelativePositionY = -1;
        gameUnit4.RelativePositionZ = 0;
        gameUnit4.RelativePosition0X = 1;
        gameUnit4.RelativePosition0Y = -1;
        gameUnit4.RelativePosition0Z = 0;
        gameUnit4.RelativePosition270X = 1;
        gameUnit4.RelativePosition270Y = 0;
        gameUnit4.RelativePosition270Z = 0;
        gameUnit4.RelativePosition180X = 1;
        gameUnit4.RelativePosition180Y = -1;
        gameUnit4.RelativePosition180Z = 0;
        gameUnit4.GamePiece = this;

        this.GameUnits.Add(gameUnit);
        this.GameUnits.Add(gameUnit2);
        this.GameUnits.Add(gameUnit3);
        this.GameUnits.Add(gameUnit4);
    }
}