using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Material m_BlackMat;
    public Material m_WhiteMat;
    
    public int Level = 0;

    private void Start()
    {
    }

    public void InitializeLevel()
    {
        var data = LoadLevelFromResources();
        float delay = 0f;
        // Create Player Piece
        var king = PiecesManager.Instance.GetPiece(ePieceName.King);
        var kingPiece = Instantiate(king, transform);
        kingPiece.Init(ePieceType.White, m_WhiteMat);
        kingPiece.transform.position = data.playerPosition;
        data.playerPosition.y = 1.5f;
        kingPiece.name = ePieceName.King.ToString();
        
        var hashTableKing = iTween.Hash("position", data.playerPosition, "time", 0.25f, "delay", delay, "easetype", iTween.EaseType.easeOutBack);
        iTween.MoveFrom(kingPiece.gameObject,hashTableKing);
        delay += 0.15f;
        
        if (data.enemyPieces.Count > 0)
        {
            foreach (var enemyData in data.enemyPieces)
            {
                var enemy = PiecesManager.Instance.GetPiece(enemyData.pieceName);
                var enemyPiece = Instantiate(enemy, transform);
                enemyPiece.Init(ePieceType.Black, m_BlackMat);
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
