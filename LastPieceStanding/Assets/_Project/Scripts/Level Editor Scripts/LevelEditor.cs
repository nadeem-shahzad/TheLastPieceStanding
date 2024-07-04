using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{

    public ePieceName m_PieceName;
    public ePieceType m_PieceType;
    public Transform m_EnemyParent;
    public Transform m_PlayerAdditionalMovesParent;
    public Transform m_PlayerPiece;
    public Material m_BlackMat;
    public Material m_WhiteMat;
    public int m_Level = 0;


    [ContextMenu("SpawnNewPiece")]
    public void SpawnPiece()
    {
        var piece = PiecesManager.Instance.GetPiece(m_PieceName);
        var isEnemy = m_PieceType == ePieceType.Black;
        var newPiece = Instantiate(piece, isEnemy ? m_EnemyParent : m_PlayerAdditionalMovesParent);
        newPiece.Init(m_PieceType, isEnemy ? m_BlackMat : m_WhiteMat);
        newPiece.name = m_PieceName.ToString();
    }

    [ContextMenu("ShowJSON")]
    public void CreateLevelData()
    {
        var levelData = new LevelData
        {
            enemyPieces = new List<EnemyPiece>(),
            playerPosition = m_PlayerPiece.position,
            playerAdditionalMoves = new List<ePieceName>()
        };

        if (m_EnemyParent.childCount > 0)
        {
            for (var i = 0; i < m_EnemyParent.childCount; i++)
            {
                var piece = m_EnemyParent.GetChild(i).GetComponent<Piece>();
                var enemyPiece = new EnemyPiece
                {
                    piecePosition = piece.transform.position,
                    pieceName = piece.PieceName
                };
                levelData.enemyPieces.Add(enemyPiece);
            }
        }

        if (m_PlayerAdditionalMovesParent.childCount > 0)
        {
            for (var i = 0; i < m_PlayerAdditionalMovesParent.childCount; i++)
            {
                var piece = m_PlayerAdditionalMovesParent.GetChild(i).GetComponent<Piece>();
                levelData.playerAdditionalMoves.Add(piece.PieceName);
            }
        }

        var jsonData = JsonUtility.ToJson(levelData);
        Debug.Log("--------------------- JSON Data ---------------------");
        Debug.Log(jsonData);
        Debug.Log("--------------------- JSON Data ---------------------");

        string resourcesPath = Path.Combine(Application.dataPath, "Resources/Levels");
        string fileName = $"{m_Level}.json";

        if (!Directory.Exists(resourcesPath))
            Directory.CreateDirectory(resourcesPath);

        string filePath = Path.Combine(resourcesPath, fileName);

        try
        {
            using StreamWriter writer = new StreamWriter(filePath, false);
            writer.Write(jsonData);
        }
        catch (IOException e)
        {
            Debug.LogError($"Error saving JSON file: {e.Message}");
        }
    }
}
    
