using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Material m_BlackMat;
    public Material m_WhiteMat;

    [SerializeField] private Material[] m_AllBlack; 
    [SerializeField] private Material[] m_AllWhite; 
    
    
    public static int Level = 8;

    private List<Piece> m_AllEnemyPieces;

    public static LevelManager Instance;
    
    private void Start()
    {
        Instance = this;
    }

    public void InitializeLevel()
    {
        var data = LoadLevelFromResources();
        float delay = 0f;
        var matIndex = PlayerPrefs.GetInt(Constants.EquippedPiece, 0);
        var whiteMat = m_AllWhite[matIndex];
        var blackMat = m_AllBlack[matIndex];
        
        // Create Player Piece
        var king = PiecesManager.Instance.GetPiece(ePieceName.King);
        var kingPiece = Instantiate(king, transform);
        kingPiece.Init(ePieceType.White, whiteMat);
        kingPiece.transform.position = data.playerPosition;
        data.playerPosition.y = 1.5f;
        kingPiece.name = ePieceName.King.ToString();
        
        var hashTableKing = iTween.Hash("position", data.playerPosition, "time", 0.25f, "delay", delay, "easetype", iTween.EaseType.easeOutBack);
        iTween.MoveFrom(kingPiece.gameObject,hashTableKing);
        delay += 0.15f;
        m_AllEnemyPieces = new List<Piece>();
        GameManager.Instance.m_PieceCounter = data.enemyPieces.Count;
        
        if (data.enemyPieces.Count > 0)
        {
            foreach (var enemyData in data.enemyPieces)
            {
                var enemy = PiecesManager.Instance.GetPiece(enemyData.pieceName);
                var enemyPiece = Instantiate(enemy, transform);
                m_AllEnemyPieces.Add(enemyPiece);
                enemyPiece.Init(ePieceType.Black, blackMat);
                enemyPiece.transform.position = enemyData.piecePosition;
                enemyData.piecePosition.y = 1.5f;
                enemyPiece.name = enemyData.pieceName.ToString();
                var hashTableEnemy = iTween.Hash("position", enemyData.piecePosition, "time", 0.25f, "delay", delay, "easetype", iTween.EaseType.easeOutBack);
                iTween.MoveFrom(enemyPiece.gameObject,hashTableEnemy);
                delay += 0.15f;
            }
        }
        
        
        if (data.playerAdditionalMoves.Count > 0)
        {
            var allPieces = data.playerAdditionalMoves.Select(enemyData => PiecesManager.Instance.GetPiece(enemyData)).ToList();
            UIViewManager.GetUIView<GamePlayPanelView>().InitializePieceCards(allPieces);
        }
        
    }


    public void RemoveEnemyPiece(Piece piece)
    {
        if (piece != null && m_AllEnemyPieces.Contains(piece))
            m_AllEnemyPieces.Remove(piece);
    }

    public void MoveAllEnemyPieces()
    {
        foreach (var enemyPiece in m_AllEnemyPieces)
        {
           enemyPiece.MoveEnemyPiece(); 
        }
    }
    
    

    private LevelData LoadLevelFromResources()
    {
        var jsonFile = Resources.Load<TextAsset>($"Levels/{Level}");
        return JsonUtility.FromJson<LevelData>(jsonFile.text);
    }

    public void OnStartClick()
    {
        UIViewManager.Show<GamePlayPanelView>();
    }
    
    
}
