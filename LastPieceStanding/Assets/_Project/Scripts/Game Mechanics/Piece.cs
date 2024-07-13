using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private ePieceName m_PieceName;
    [SerializeField] private ePieceType m_PieceType;
    [SerializeField] private Sprite m_Render;
    
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
        
        Invoke(nameof(MovePieceToTile),2f);
    }

    public void MovePieceToTile()
    {
        BoardManager.Instance.GetTile(transform.position).SetPieceHere(this);
    }
    
    

    private void OnMouseDown()
    {
        if (IsPlayerPiece is false)
            return;

        // var tiles= BoardManager.Instance.GetTilesToMoveOn(this);
        //
        // foreach (var tile in tiles)
        //     tile.HighlightTile(true);
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
