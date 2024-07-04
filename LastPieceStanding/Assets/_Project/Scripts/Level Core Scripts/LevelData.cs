using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelData
{
    public List<EnemyPiece> enemyPieces;
    public Vector3 playerPosition;
    public List<ePieceName> playerAdditionalMoves;
}


[System.Serializable]
public class EnemyPiece
{
    public ePieceName pieceName;
    public Vector3 piecePosition;
}



