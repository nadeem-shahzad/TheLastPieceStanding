using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardManager : MonoBehaviour
{
   [SerializeField] private Tile m_TilePrefab;
   [SerializeField] private int m_BoardSize = 8;
   [SerializeField] private UnityEvent m_OnCreatBoard;
   private Dictionary<Vector3, Tile> m_AllTiles;
   

   void Start()
   {
      InitializeBoard();
   }


   private void InitializeBoard()
   {
      m_AllTiles = new Dictionary<Vector3, Tile>();
      StartCoroutine(CreateBoard());
   }


   IEnumerator CreateBoard()
   {
      float delay = 0f;
      for (var x = 0; x < m_BoardSize; x++)
      {
         for (var z = 0; z < m_BoardSize; z++)
         {
            float xPos = x;
            var yPos = 0f;
            float zPos = z;
            var position = new Vector3(xPos, yPos, zPos);

            // Create a new tile instance from the prefab
            var newTile = Instantiate(m_TilePrefab,transform);
            m_AllTiles.Add(position,newTile);
            newTile.transform.position = position;

            position.y = 1f;
            newTile.name = $"{xPos} {zPos}";
            newTile.SetColor((x + z) % 2 == 0);

            var hashTable = iTween.Hash("scale", Vector3.zero, "time", 0.25f, "delay", delay, "easetype", iTween.EaseType.easeOutBack);
            iTween.ScaleFrom(newTile.gameObject,hashTable);
            delay += 0.01f;

         }
      }
      yield return new WaitForSeconds(delay);
      m_OnCreatBoard?.Invoke();
   }


   public void GetTilesToMoveOn(Piece piece)
   {
      if (piece.PieceName == ePieceName.Pawn)
      {
         PawnRule(piece);
      }
   }



   private void PawnRule(Piece piece)
   {
      
   }
   
   
   
}
