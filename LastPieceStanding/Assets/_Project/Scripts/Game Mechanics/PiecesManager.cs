using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PiecesManager : Singleton<PiecesManager>
{
    [SerializeField] private List<Piece> m_AllPiecesPrefabs;




    public Piece GetPiece(ePieceName name)
    {
        return m_AllPiecesPrefabs.FirstOrDefault(prefab => prefab.PieceName == name);
    }
}


