using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
   [SerializeField] private Material m_White;
   [SerializeField] private Material m_Black;
   [SerializeField] private MeshRenderer m_Renderer;
   [SerializeField] private bool m_IsEmpty = true;
   [SerializeField] private SpriteRenderer m_Dot;
   [SerializeField] private Piece m_CurrentPiece;
   
   public Vector3 Position => transform.position;
   public bool IsEmpty => m_IsEmpty;

   public void SetColor(bool isBlack)
   {
       m_Renderer.sharedMaterial = isBlack ? m_Black : m_White;
       m_Dot.gameObject.SetActive(false);
   }

   private void OnEnable()
   {
       StartCoroutine(nameof(ChangeStatus));
   }

   IEnumerator ChangeStatus()
   {
       while (true)
       {
           yield return new WaitForSeconds(0.5f);

           if (m_CurrentPiece == null)
               SetPieceHere(null, true);
       }
   }
   
   public void SetPieceHere(Piece  piece, bool isEmpty = false)
   {
       m_IsEmpty = isEmpty;
       
       if (piece == null)
       {
           return;
       }
       

       if ((m_CurrentPiece != null && m_CurrentPiece.IsPlayerPiece is false))
       {
           GameManager.Instance.m_PieceCounter--;
           LevelManager.Instance.RemoveEnemyPiece(m_CurrentPiece);

           if (GameManager.Instance.m_PieceCounter <= 0)
           {
               GameManager.Instance.IsGameEnded = true;
               UIViewManager.Show<WinPanelView>();
           } 
           
           
           

           Destroy(m_CurrentPiece.gameObject);
       }

       

       m_CurrentPiece = piece;
   }

   public void HighlightTile(bool isHighlight = true)
   {
       m_Dot.gameObject.SetActive(isHighlight);
   }
   

   private void OnMouseDown()
   {
       if (GameManager.Instance.IsGameEnded)
           return;

       BoardManager.Instance.GetTile(BoardManager.Instance.Player.Position).SetPieceHere(null,true);
       BoardManager.Instance.MovePlayer(this);
   }
   
   private void OnMouseUp()
   {
   }
}
