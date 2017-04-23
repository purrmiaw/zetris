using System;
using System.Collections.Generic;
using System.Linq;

public class GameManager
{
    public float CurrentPieceMovementDownInterval = 0.3f;

    private int _pieceSpawnPositionX;
    private int _pieceSpawnPositionY;
    private int _pieceSpawnPositionZ;
    private int _unitsX;
    private int _unitsY;
    private int _unitsZ;

    public int GetPieceSpawnPositionX()
    {
        return _pieceSpawnPositionX;
    }
    public int GetPieceSpawnPositionY()
    {
        return _pieceSpawnPositionY;
    }
    public int GetPieceSpawnPositionZ()
    {
        return _pieceSpawnPositionZ;
    }

    public GamePiece CurrentGamePiece;
    //private GamePiece nextGamePiece;

    private ICollection<GameTile> gameTiles = new List<GameTile>();
    private ICollection<GamePiece> gamePieces = new List<GamePiece>();

    private IGameObjectManager IGameObjectManager;
    private IGameOver IGameOver;

    public GameManager(IGameObjectManager IGameObjectManager, IGameOver iGameOver)
    {
        this.IGameObjectManager = IGameObjectManager;
        this.IGameOver = iGameOver;
    }

    /// <summary>
    /// Instantiates board. Zero-based. Left Bottom is (0, 0). Upper Right is (boardLengthX-1, boardLengthY-1). 
    /// Currently Z doesn't do anything.
    /// </summary>
    /// <param name="boardLengthX"></param>
    /// <param name="boardLengthY"></param>
    /// <param name="boardLengthZ"></param>
    /// <param name="spawnPositionX"></param>
    /// <param name="spawnPositionY"></param>
    /// <param name="spawnPositionZ"></param>
    public void InstantiateBoard(int boardLengthX, int boardLengthY, int boardLengthZ,
        int spawnPositionX, int spawnPositionY, int spawnPositionZ)
    {

        // Initiate
        _unitsX = boardLengthX;
        _unitsY = boardLengthY;
        _unitsZ = boardLengthZ;

        // Set spawn position
        if (spawnPositionX >= boardLengthX || spawnPositionY >= boardLengthY)
        {
            throw new Exception("Spawn position outside of board. Please set the spawn position to be inside the board.");
        }

        _pieceSpawnPositionX = spawnPositionX;
        _pieceSpawnPositionY = spawnPositionY;
        _pieceSpawnPositionZ = spawnPositionZ;

        // Initiate tiles
        for (int x = 0; x < boardLengthX; x++)
        {
            for (int y = 0; y < boardLengthY; y++)
            {
                // TODO: streamline this
                GameTile gameTile = new GameTile();
                gameTile.GameUnits = new List<GameUnit>();
                gameTile.X = x;
                gameTile.Y = y;
                gameTile.Z = 0;
                gameTile.GameObjectInstanceId = IGameObjectManager.AddTileGameObject(x, y);
                gameTiles.Add(gameTile);
            }
        }
    }

    public IEnumerable<GameTile> GetGameTiles()
    {
        return gameTiles;
    }

    public IEnumerable<GamePiece> GetGamePieces()
    {
        return gamePieces;
    }

    public void InstantiateNewCurrentGamePiece()
    {
        int randomInt = new Random().Next(0, 6);
        GamePiece gamePiece = null;
        
        switch (randomInt)
        {
            case 0:
                gamePiece = new GamePieceL();
                break;
            case 1:
                gamePiece = new GamePieceLInverse();
                break;
            case 2:
                gamePiece = new GamePieceI();
                break;
            case 3:
                gamePiece = new GamePieceO();
                break;
            case 4:
                gamePiece = new GamePieceT();
                break;
            case 5:
                gamePiece = new GamePieceZ();
                break;
            case 6:
                gamePiece = new GamePieceZInverse();
                break;
        }

        AddGamePiece(gamePiece, _pieceSpawnPositionX, _pieceSpawnPositionY, _pieceSpawnPositionZ);

        CurrentGamePiece = gamePiece;
    }

    public void InstantiateNewCurrentGamePiece(GamePiece gamePiece)
    {
        AddGamePiece(gamePiece, _pieceSpawnPositionX, _pieceSpawnPositionY, _pieceSpawnPositionZ);

        CurrentGamePiece = gamePiece;
    }

    public void MoveCurrentGamePiece(PieceMovement pieceMovement)
    {

        if (CurrentGamePiece == null)
        {
            return;
        }

        switch (pieceMovement)
        {
            case PieceMovement.Left:
                if (GamePieceCanMove(CurrentGamePiece, PieceMovement.Left))
                {
                    MoveGamePiece(CurrentGamePiece, PieceMovement.Left);
                }
                break;

            case PieceMovement.Right:
                if (GamePieceCanMove(CurrentGamePiece, PieceMovement.Right))
                {
                    MoveGamePiece(CurrentGamePiece, PieceMovement.Right);
                }
                break;

            case PieceMovement.Down:
                if (GamePieceCanMove(CurrentGamePiece, PieceMovement.Down))
                {
                    MoveGamePiece(CurrentGamePiece, PieceMovement.Down);
                }
                else
                {
                    // Piece cannot move down anymore. So now do a check, clear and instantiate.

                    // Check if game over
                    bool atLeastOneTileInTopRowOccupied = IsAtLeastOneTileInTopRowOccupied();
                    if (atLeastOneTileInTopRowOccupied)
                    {
                        // game over
                        IGameOver.InitiateGameOver();
                        return;
                    }

                    // Clear and instantiate
                    CheckClearRowsFilledWithGameUnits();
                    InstantiateNewCurrentGamePiece();
                }
                break;
        }
    }

    public void CheckClearRowsFilledWithGameUnits()
    {
        // check if tiles in one row are used
        // loop though y

        // determine rows that are full
        List<int> rowsThatAreFull = new List<int>();

        for (int row = 0; row < _unitsY; row++)
        {
            int tilesUsedInCurrentRow = gameTiles.Where(a => a.Y == row && a.IsOccupied()).Count();

            if (tilesUsedInCurrentRow == _unitsX)
            {
                // row is full
                rowsThatAreFull.Add(row);
            }
        }

        // clear rows starting from above. 
        // then move each row above the cleared row down
        // then clear the next clearable row below
        // then move each row above the newly cleared row down too
        // repeat
        foreach (int clearableRow in rowsThatAreFull.OrderByDescending(x => x))
        {
            ClearRow(clearableRow);

            // loop above rows and determine the gameunits to move down
            List<GameUnit> gameUnitsToMoveDown = new List<GameUnit>();
            for (int aboveRow = (clearableRow + 1); aboveRow < _unitsY; aboveRow++)
            {
                for (int theColumn = 0; theColumn < _unitsX; theColumn++)
                {
                    GameTile gameTile = gameTiles.Where(a => a.Y == aboveRow && a.X == theColumn).FirstOrDefault();
                    if (gameTile.GameUnits != null)
                    {
                        foreach (GameUnit gameUnit in gameTile.GameUnits)
                        {
                            gameUnitsToMoveDown.Add(gameUnit);
                        }
                    }
                }
            }

            foreach (GameUnit gameUnit in gameUnitsToMoveDown)
            {
                MoveGameUnit(gameUnit, PieceMovement.Down);
            }
        }

    }

    public void ClearRow(int row)
    {
        // clear all tiles in this row
        List<GameTile> gameTilesThisRow = gameTiles.Where(a => a.Y == row && a.IsOccupied()).ToList();

        foreach (GameTile gameTile in gameTilesThisRow)
        {
            foreach (GameUnit gameUnit in gameTile.GameUnits.ToList())
            {
                RemoveGameUnit(gameUnit);
            }
        }
    }

    public void MoveRowDown(int theRow)
    {
        for (int theColumn = 0; theColumn < _unitsX; theColumn++)
        {
            GameTile gameTile = gameTiles.Where(a => a.X == theColumn && a.Y == theRow)
                .FirstOrDefault();

            foreach (GameUnit gameUnit in gameTile.GameUnits.ToList())
            {
                if (gameUnit != null)
                {
                    MoveGameUnit(gameUnit, PieceMovement.Down);
                }
            }

        }
    }

    public void MoveGamePiece(GamePiece gamePiece, PieceMovement pieceMovement)
    {
        foreach (GameUnit gameUnit in gamePiece.GameUnits)
        {
            MoveGameUnit(gameUnit, pieceMovement);
        }
    }

    public bool GamePieceCanMove(GamePiece gamePiece, PieceMovement pieceMovement)
    {
        // only move if every Game Unit can move
        bool everyoneCanMove = true;
        foreach (GameUnit gameUnit in gamePiece.GameUnits)
        {
            // if the target game tile is occupied
            if (!GameUnitCanMove(gameUnit, pieceMovement, true))
            {
                everyoneCanMove = false;
            }
        }

        return everyoneCanMove;
    }

    public bool GameUnitCanMove(GameUnit gameUnit, PieceMovement pieceMovement, bool canMoveToTileIfOccupiedByGameUnitFromSameGamePiece = false)
    {
        GameTile nextTile = null;
        nextTile = DetermineNextTile(gameUnit, pieceMovement);

        if (GameUnitCanMove(gameUnit, nextTile, canMoveToTileIfOccupiedByGameUnitFromSameGamePiece))
        {
            return true;
        }

        //// if at least one of the game unit in next tile can move, then can move.
        //// i don't know why i did this. no, i know but it's long to explain as f that.
        //if (nextTile != null)
        //{
        //    bool gameUnitInNextTileCanMove = false;
        //    foreach (GameUnit gameUnitInNextTile in nextTile.GameUnits)
        //    {
        //        gameUnitInNextTileCanMove = GameUnitCanMove(gameUnitInNextTile, pieceMovement);
        //        if (gameUnitInNextTileCanMove)
        //        {
        //            return true;
        //        }
        //    }
        //}

        return false;

    }

    public bool GameUnitCanMove(GameUnit gameUnit, GameTile targetGameTile, bool canMoveToTileIfOccupiedByGameUnitFromSameGamePiece = false)
    {
        // validates whether or not a gameunit can move into a tile or not
        if (targetGameTile == null)
        {
            return false;
        }

        // if target game tile is occupied
        //if (targetGameTile.IsOccupied() && targetGameTile.GameUnits.Where(x => x.GamePiece == gameUnit.GamePiece).Count() == 0)
        if (targetGameTile.IsOccupied())
        {
            if (!canMoveToTileIfOccupiedByGameUnitFromSameGamePiece)
            {
                return false;
            }

            // determine if the target game tile is occupied by a unit from the same game piece
            // ignore if the next unit it's colliding is a unit from the same piece
            GameUnit gameUnitInTargetGameTileSameGamePiece = targetGameTile.GameUnits.Where(x => x.GamePiece == gameUnit.GamePiece).FirstOrDefault();
            if (gameUnitInTargetGameTileSameGamePiece == null)
            {
                // if a game unit from the same game piece is in the target game tile, then the user can move.to that tile
                // if the unit in the tile does not belong to the game piece, then cannot move to it.                
                return false;
            }

        }

        return true;
    }

    /// <summary>
    /// Moves game unit. Doesn't do any checking.
    /// </summary>
    /// <param name="gameUnit"></param>
    /// <param name="pieceMovement"></param>
    /// <param name="isCurrentPiece"></param>
    public void MoveGameUnit(GameUnit gameUnit, PieceMovement pieceMovement)
    {

        // determine the next gameTile
        GameTile nextTile = DetermineNextTile(gameUnit, pieceMovement);
        MoveGameUnit(gameUnit, nextTile);

    }

    /// <summary>
    /// Moves game unit. Doesn't do any checking.
    /// </summary>
    /// <param name="gameUnit"></param>
    /// <param name="targetGameTile"></param>
    public void MoveGameUnit(GameUnit gameUnit, GameTile targetGameTile)
    {

        // clear
        gameUnit.GameTile.GameUnits.Remove(gameUnit);
        gameUnit.GameTile = null;

        // add
        gameUnit.GameTile = targetGameTile;
        targetGameTile.GameUnits.Add(gameUnit);

        // move game object of GameUnit
        IGameObjectManager.Move(gameUnit.GameObjectInstanceId, targetGameTile.GameObjectInstanceId);

    }

    /// <summary>
    /// Determines the next tile
    /// </summary>
    /// <param name="gameUnit"></param>
    /// <param name="pieceMovement"></param>
    /// <returns></returns>
    public GameTile DetermineNextTile(GameUnit gameUnit, PieceMovement pieceMovement)
    {

        // determine the next gameTile
        GameTile currentTile = gameUnit.GameTile;
        GameTile nextTile = null;

        switch (pieceMovement)
        {
            case PieceMovement.Down:
                nextTile = gameTiles.Where(a => a.X == currentTile.X && a.Y == (currentTile.Y - 1)).FirstOrDefault();
                break;

            case PieceMovement.Left:
                nextTile = gameTiles.Where(a => a.X == (currentTile.X - 1) && a.Y == (currentTile.Y)).FirstOrDefault();
                break;

            case PieceMovement.Right:
                nextTile = gameTiles.Where(a => a.X == (currentTile.X + 1) && a.Y == (currentTile.Y)).FirstOrDefault();
                break;
        }

        return nextTile;
    }

    /// <summary>
    /// Remove game unit from the board
    /// </summary>
    /// <param name="gameUnit"></param>
    public void RemoveGameUnit(GameUnit gameUnit)
    {
        // clear from tile
        if (gameUnit.GameTile != null)
        {
            gameUnit.GameTile.GameUnits.Remove(gameUnit);
            gameUnit.GameTile = null;
        }

        // Remove from gamePiece
        gameUnit.GamePiece.GameUnits.Remove(gameUnit);
        gameUnit.GamePiece = null;

        // Destroy
        IGameObjectManager.Remove(gameUnit.GameObjectInstanceId);

    }

    /// <summary>
    /// Add game piece to the default spawn position. Default orientation.
    /// </summary>
    /// <param name="gamePiece"></param>
    public void AddGamePiece(GamePiece gamePiece)
    {
        this.AddGamePiece(gamePiece, _pieceSpawnPositionX, _pieceSpawnPositionY, _pieceSpawnPositionZ);
    }

    /// <summary>
    /// Add game piece
    /// </summary>
    /// <param name="gamePiece"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void AddGamePiece(GamePiece gamePiece, int x, int y, int z)
    {
        // instantiate
        foreach (GameUnit gameUnit in gamePiece.GameUnits)
        {
            int gameUnitX = x + gameUnit.RelativePositionX;
            int gameUnitY = y + gameUnit.RelativePositionY;
            int gameUnitZ = z + gameUnit.RelativePositionZ;

            gameUnit.GameTile = gameTiles.Where(a => a.X == gameUnitX
                && a.Y == gameUnitY
                && a.Z == gameUnitZ).FirstOrDefault();

            gameUnit.GameTile.GameUnits.Add(gameUnit);

            gameUnit.GameObjectInstanceId = IGameObjectManager.Add(gameUnitX, gameUnitY, gameUnitZ);
        }

        // add to overall
        gamePieces.Add(gamePiece);

    }

    /// <summary>
    /// Rotates Game Piece. No check is done.
    /// </summary>
    /// <param name="gamePiece"></param>
    /// <param name="gamePieceOrientation"></param>
    public void RotateGamePiece(GamePiece gamePiece, GamePieceOrientation gamePieceOrientation)
    {

        int gamePieceX = gamePiece.GetGamePiecePositionX();
        int gamePieceY = gamePiece.GetGamePiecePositionY();
        int gamePieceZ = gamePiece.GetGamePiecePositionZ();

        foreach (GameUnit gameUnit in gamePiece.GameUnits)
        {
            int gameUnitNewX = 0;
            int gameUnitNewY = 0;
            int gameUnitNewZ = 0;

            switch (gamePieceOrientation)
            {
                case GamePieceOrientation.Default:
                    gameUnitNewX = gamePieceX + gameUnit.RelativePositionX;
                    gameUnitNewY = gamePieceY + gameUnit.RelativePositionY;
                    gameUnitNewZ = gamePieceZ + gameUnit.RelativePositionZ;
                    break;

                case GamePieceOrientation.Degree0:
                    gameUnitNewX = gamePieceX + gameUnit.RelativePosition0X;
                    gameUnitNewY = gamePieceY + gameUnit.RelativePosition0Y;
                    gameUnitNewZ = gamePieceZ + gameUnit.RelativePosition0Z;
                    break;

                case GamePieceOrientation.Degree270:
                    gameUnitNewX = gamePieceX + gameUnit.RelativePosition270X;
                    gameUnitNewY = gamePieceY + gameUnit.RelativePosition270Y;
                    gameUnitNewZ = gamePieceZ + gameUnit.RelativePosition270Z;
                    break;

                case GamePieceOrientation.Degree180:
                    gameUnitNewX = gamePieceX + gameUnit.RelativePosition180X;
                    gameUnitNewY = gamePieceY + gameUnit.RelativePosition180Y;
                    gameUnitNewZ = gamePieceZ + gameUnit.RelativePosition180Z;
                    break;
            }

            GameTile targetGameTile = gameTiles.Where(a => a.X == gameUnitNewX && a.Y == gameUnitNewY && a.Z == gameUnitNewZ).FirstOrDefault();
            MoveGameUnit(gameUnit, targetGameTile);
        }

        // Update Game Piece Orientation
        gamePiece.CurrentGamePieceOrientation = gamePieceOrientation;

        return;

    }

    /// <summary>
    /// Checks if game Piece can rotate.
    /// </summary>
    /// <param name="gamePiece"></param>
    /// <param name="gamePieceOrientation"></param>
    /// <returns></returns>
    public bool GamePieceCanRotate(GamePiece gamePiece, GamePieceOrientation gamePieceOrientation)
    {
        int gamePieceX = gamePiece.GetGamePiecePositionX();
        int gamePieceY = gamePiece.GetGamePiecePositionY();
        int gamePieceZ = gamePiece.GetGamePiecePositionZ();

        foreach (GameUnit gameUnit in gamePiece.GameUnits)
        {
            int gameUnitOldX = gameUnit.GameTile.X;
            int gameUnitOldY = gameUnit.GameTile.Y;
            int gameUnitOldZ = gameUnit.GameTile.Z;

            int gameUnitNewX = 0;
            int gameUnitNewY = 0;
            int gameUnitNewZ = 0;

            switch (gamePieceOrientation)
            {
                case GamePieceOrientation.Default:
                    gameUnitNewX = gamePieceX + gameUnit.RelativePositionX;
                    gameUnitNewY = gamePieceY + gameUnit.RelativePositionY;
                    gameUnitNewZ = gamePieceZ + gameUnit.RelativePositionZ;
                    break;

                case GamePieceOrientation.Degree0:
                    gameUnitNewX = gamePieceX + gameUnit.RelativePosition0X;
                    gameUnitNewY = gamePieceY + gameUnit.RelativePosition0Y;
                    gameUnitNewZ = gamePieceZ + gameUnit.RelativePosition0Z;
                    break;

                case GamePieceOrientation.Degree270:
                    gameUnitNewX = gamePieceX + gameUnit.RelativePosition270X;
                    gameUnitNewY = gamePieceY + gameUnit.RelativePosition270Y;
                    gameUnitNewZ = gamePieceZ + gameUnit.RelativePosition270Z;
                    break;

                case GamePieceOrientation.Degree180:
                    gameUnitNewX = gamePieceX + gameUnit.RelativePosition180X;
                    gameUnitNewY = gamePieceY + gameUnit.RelativePosition180Y;
                    gameUnitNewZ = gamePieceZ + gameUnit.RelativePosition180Z;
                    break;
            }

            if (gameUnitOldX == gameUnitNewX && gameUnitOldY == gameUnitNewY && gameUnitOldZ == gameUnitNewZ)
            {
                // skip checking if location is the same
                continue;
            }

            GameTile targetGameTile = gameTiles.Where(a => a.X == gameUnitNewX && a.Y == gameUnitNewY && a.Z == gameUnitNewZ).FirstOrDefault();
            bool canRotate = true;
            canRotate = GameUnitCanMove(gameUnit, targetGameTile, true);

            if (!canRotate)
            {
                return false;
            }
        }

        return true;
    }

    public GamePieceOrientation DetermineNextGamePieceOrientation(GamePieceOrientation gamePieceOrientation)
    {
        switch (gamePieceOrientation)
        {
            case GamePieceOrientation.Default:
                return GamePieceOrientation.Degree0;
            case GamePieceOrientation.Degree0:
                return GamePieceOrientation.Degree270;
            case GamePieceOrientation.Degree270:
                return GamePieceOrientation.Degree180;
            case GamePieceOrientation.Degree180:
                return GamePieceOrientation.Default;
        }

        return GamePieceOrientation.Default;
    }

    public void RotateCurrentGamePiece()
    {
        GamePieceOrientation nextOrientation = DetermineNextGamePieceOrientation(CurrentGamePiece.CurrentGamePieceOrientation);

        bool canRotate = GamePieceCanRotate(CurrentGamePiece, nextOrientation);
        if (canRotate)
        {
            RotateGamePiece(CurrentGamePiece, nextOrientation);
        }
    }

    public bool IsAtLeastOneColumnFull()
    {

        bool atLeastOneColumnIsFull = false;

        for (int x = 0; x < _unitsX; x++)
        {

            int numberOfOccupiedTilesInThisColumn = gameTiles.Where(a => a.X == x && a.IsOccupied()).Count();

            if (numberOfOccupiedTilesInThisColumn == _unitsY)
            {
                // all tiles in this column is occupied. That means at least one column is full.
                return true;
            }

        }

        return atLeastOneColumnIsFull;
    }

    public bool IsAtLeastOneTileInTopRowOccupied()
    {

        IEnumerable<GameTile> topRowGameTiles = this.GetGameTiles().Where(a => a.Y == (_unitsY - 1)).ToList();

        if (topRowGameTiles.Where(a => a.IsOccupied()).Count() > 0)
        {
            return true;
        }

        return false;
    }
}

public enum PieceMovement
{
    Down,
    Left,
    Right
}