using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public class CubeBasedGameManagerController : GameObjectManager, IGameOver
{

    public int boardLengthX = 10;
    public int boardLengthY = 20;
    public int boardLengthZ = 0;
    public float currentPieceMovementDownInterval = 0.3f;
    public GameObject nextGamePieceGameObject;
    public GameObject gameOverText;

    private GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = new GameManager(this, this);

        // Instantiate first cube
        // Set spawn position at the top middle
        int pieceSpawnPositionX = (int)boardLengthX / 2;
        int pieceSpawnPositionY = boardLengthY - 1;
        int pieceSpawnPositionZ = 0;

        gameOverText.SetActive(false);
        gameManager.InstantiateBoard(boardLengthX, boardLengthY, 0, pieceSpawnPositionX, pieceSpawnPositionY, pieceSpawnPositionZ);
        gameManager.InstantiateNewCurrentGamePiece();
        InvokeRepeating("MoveCurrentGamePieceDown", 1, currentPieceMovementDownInterval); // Move the current piece down
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetAxis("Horizontal") < 0)
        //{
        //    gameManager.MoveCurrentGamePiece(PieceMovement.Left);
        //}
        //else if (Input.GetAxis("Horizontal") > 0)
        //{
        //    gameManager.MoveCurrentGamePiece(PieceMovement.Right);
        //}
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameManager.MoveCurrentGamePiece(PieceMovement.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameManager.MoveCurrentGamePiece(PieceMovement.Right);
        }


        if (Input.GetAxis("Vertical") < 0f)
        {
            gameManager.MoveCurrentGamePiece(PieceMovement.Down);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameManager.RotateCurrentGamePiece();
        }
    }

    void MoveCurrentGamePieceDown()
    {
        gameManager.MoveCurrentGamePiece(PieceMovement.Down);
    }

    void IGameOver.InitiateGameOver()
    {
        CancelInvoke();
        gameOverText.SetActive(true);
    }
}