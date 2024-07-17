using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private ePieceName m_PieceName;
    [SerializeField] private ePieceType m_PieceType;
    [SerializeField] private Sprite m_Render;
    [SerializeField] private EnemyLineToMove m_LineRenderer;
    [SerializeField] private Tile m_NextTile;
    [SerializeField] private Tile m_PreviousTile;
    
    private MeshRenderer[] m_AllRenderers;
    public ePieceName PieceName
    {
        get => m_PieceName;
        set => m_PieceName = value;
    }

    public Sprite PieceImage => m_Render;
    

    public bool IsPlayerPiece => m_PieceName == ePieceName.King && m_PieceType == ePieceType.White;

    // set => throw new NotImplementedException();
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    public void Init(ePieceType type, Material material)
    {
        m_AllRenderers = GetComponentsInChildren<MeshRenderer>();
        m_PieceType = type;
        
        foreach (var renderer in m_AllRenderers)
            renderer.sharedMaterial = material;

        if (IsPlayerPiece)
        {
            BoardManager.Instance.SetPlayer(this);
            Invoke(nameof(PlayerMove),1f);
        }
        // else
        // {
        //     SpawnLineRenderer();
        //     m_NextTile = BoardManager.Instance.GetTilesToMoveOnForEnemy(this);
        //     m_LineRenderer.UpdateLineCoordinates(1f,180f);
        // }
        
        Invoke(nameof(MovePieceToTile),2f);
    }

    public void MovePieceToTile()
    {
        if (m_NextTile == null)
        {
            m_PreviousTile = BoardManager.Instance.GetTile(transform.position);
            m_PreviousTile.SetPieceHere(this);
        }
        else
        {
            
        }
        
        
        if (IsPlayerPiece is false)
        {
            SpawnLineRenderer();
            m_NextTile = BoardManager.Instance.GetTilesToMoveOnForEnemy(this);
            var coordinates = CalculateCoordinates();
            m_LineRenderer.UpdateLineCoordinates(coordinates.zLength,coordinates.yRotation);
        }
    }

    private (float zLength, float yRotation) CalculateCoordinates()
    {
        float zLength = m_NextTile.Position.z - Position.z;
        if (m_NextTile.Position.x == Position.x)
        {
            return (zLength,0f);
        }
        else
        {
            float angle = Vector3.Angle(m_NextTile.Position, Position);
            float sign = Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(m_NextTile.Position, Position)));
            float yRotation = angle * sign;
            
            if (yRotation> 0)
                yRotation = 45;
            else
                yRotation = -45;
            
            return (zLength * 1.43f,yRotation);
        }

    }
    
    private void SpawnLineRenderer()
    {
        m_LineRenderer = Instantiate(m_LineRenderer, transform);
    }


    private void MoveEnemyPiece()
    {
        var tile = m_NextTile;
        
        var hashTable = iTween.Hash("position", tile.Position, "speed", 2.5f, "easetype", iTween.EaseType.easeInOutCubic, 
            "oncompletetarget",gameObject, "oncomplete","OnCompleteMove");
        iTween.MoveTo(gameObject, hashTable);
    }
    
    private void OnCompleteMove(Tile tile)
    {
        // KingRule(m_Player.Position);
    }
    
    private void PlayerMove()
    {
        BoardManager.Instance.GetTilesToMoveOn(ePieceName.King);
    }
}

public enum ePieceName
{
    King, Queen, Knight, Bishop, Rook, Pawn
}

public enum ePieceType
{
    Black, White
}
