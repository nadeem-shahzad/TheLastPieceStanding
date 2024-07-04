using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private ePieceName m_PieceName;
    [SerializeField] private ePieceType m_PieceType;

    private MeshRenderer[] m_AllRenderers;
    public ePieceName PieceName => m_PieceName;

    public void Init(ePieceType type, Material material)
    {
        m_AllRenderers = GetComponentsInChildren<MeshRenderer>();
        m_PieceType = type;
        
        foreach (var renderer in m_AllRenderers)
            renderer.sharedMaterial = material;
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
