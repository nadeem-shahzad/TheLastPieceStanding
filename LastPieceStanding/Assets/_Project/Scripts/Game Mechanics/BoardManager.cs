using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BoardManager : MonoBehaviour
{
   [SerializeField] private Tile m_TilePrefab;
   [SerializeField] private int m_BoardSize = 8;
   [SerializeField] private UnityEvent m_OnCreatBoard;
   [SerializeField] private Piece m_Player;
   [SerializeField] private float m_PlayerMoveSpeed = 1f;
   private Dictionary<Vector3, Tile> m_AllTiles;

   private List<Tile> PreviousSelectedTiles;
   private List<Tile> CurrentSelectedTiles;

   public static BoardManager Instance;


   public Piece Player => m_Player;

   private void Start()
   {
      InitializeBoard();
      Instance = this;
   }


   private void InitializeBoard()
   {
      m_AllTiles = new Dictionary<Vector3, Tile>();
      PreviousSelectedTiles = new List<Tile>();
      CurrentSelectedTiles = new List<Tile>();
      StartCoroutine(CreateBoard());
   }


   public void SetPlayer(Piece piece)
   {
      if (piece ==  null)
         return;

      m_Player = piece;
   }

   public Tile GetTile(Vector3 pos)
   {
      return m_AllTiles[pos];
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


   public void GetTilesToMoveOn(ePieceName pieceName)
   {
      switch (pieceName)
      {
         case ePieceName.King:
            KingRule(m_Player.Position);
            break;
         case ePieceName.Queen:
            QueenRule(m_Player.Position);
            break;
         case ePieceName.Knight:
            KnightRule(m_Player.Position);
            break;
         case ePieceName.Bishop:
            BishopRule(m_Player.Position);
            break;
         case ePieceName.Rook:
            RookRule(m_Player.Position);
            break;
         case ePieceName.Pawn:
            // PawnRule(m_Player.Position);
            break;
         default:
            break;
            // throw new ArgumentOutOfRangeException(nameof(pieceName), pieceName, null);
      }
      
   }


   private void KingRule(Vector3 position)
   {
      var tiles = new List<Tile>();
      
      var x = (int)position.x;
      var y = (int)position.z;
      
      var possibleMoves = new List<(int, int)>
      {
         (x + 1, y), // Move right
         (x - 1, y), // Move left
         (x, y + 1), // Move up
         (x, y - 1), // Move down
         (x + 1, y + 1), // Move diagonally up-right
         (x - 1, y + 1), // Move diagonally up-left
         (x + 1, y - 1), // Move diagonally down-right
         (x - 1, y - 1) // Move diagonally down-left
      };

      foreach (var key in possibleMoves.Select(move => new Vector3(move.Item1, 0, move.Item2)))
      {
         if (m_AllTiles.ContainsKey(key))
         {
            tiles.Add(m_AllTiles[key]);
         }
      }

      PreviousSelectedTiles = CurrentSelectedTiles;
      CurrentSelectedTiles = tiles;
      HighlightTiles();
   }
   private void KnightRule(Vector3 position)
   {
      var tiles = new List<Tile>();
      
      var x = (int)position.x;
      var y = (int)position.z;
      
      var possibleMoves = new List<(int, int)>
      {
         (x + 2, y + 1), // Move two steps right, one step up
         (x + 2, y - 1), // Move two steps right, one step down
         (x - 2, y + 1), // Move two steps left, one step up
         (x - 2, y - 1), // Move two steps left, one step down
         (x + 1, y + 2), // Move one step right, two steps up
         (x + 1, y - 2), // Move one step right, two steps down
         (x - 1, y + 2), // Move one step left, two steps up
         (x - 1, y - 2)  // Move one step left, two steps down
      };
      
      foreach (var key in possibleMoves.Select(move => new Vector3(move.Item1, 0, move.Item2)))
      {
         if (m_AllTiles.ContainsKey(key))
         {
            tiles.Add(m_AllTiles[key]);
         }
      }

      PreviousSelectedTiles = CurrentSelectedTiles;
      CurrentSelectedTiles = tiles;
      HighlightTiles();
   }
   private void RookRule(Vector3 position)
   {
      var tiles = new List<Tile>();

      var x = (int)position.x;
      var y = (int)position.z;

      var possibleMoves = new List<(int, int)>();

      // Generate all horizontal and vertical moves within the bounds of the chessboard
      // Horizontal right
      for (int i = x + 1; i < 8; i++)
      {
         var move = (i, y);
         var key = new Vector3(move.Item1, 0, move.Item2);
         if (m_AllTiles.ContainsKey(key))
         {
            possibleMoves.Add(move);
            if (!m_AllTiles[key].IsEmpty) break;
         }
      }

      // Horizontal left
      for (int i = x - 1; i >= 0; i--)
      {
         var move = (i, y);
         var key = new Vector3(move.Item1, 0, move.Item2);
         if (m_AllTiles.ContainsKey(key))
         {
            possibleMoves.Add(move);
            if (!m_AllTiles[key].IsEmpty) break;
         }
      }

      // Vertical up
      for (int i = y + 1; i < 8; i++)
      {
         var move = (x, i);
         var key = new Vector3(move.Item1, 0, move.Item2);
         if (m_AllTiles.ContainsKey(key))
         {
            possibleMoves.Add(move);
            if (!m_AllTiles[key].IsEmpty) break;
         }
      }

      // Vertical down
      for (int i = y - 1; i >= 0; i--)
      {
         var move = (x, i);
         var key = new Vector3(move.Item1, 0, move.Item2);
         if (m_AllTiles.ContainsKey(key))
         {
            possibleMoves.Add(move);
            if (!m_AllTiles[key].IsEmpty) break;
         }
      }
      
      foreach (var key in possibleMoves.Select(move => new Vector3(move.Item1, 0, move.Item2)))
      {
         if (m_AllTiles.ContainsKey(key))
         {
            tiles.Add(m_AllTiles[key]);
         }
      }

      PreviousSelectedTiles = CurrentSelectedTiles;
      CurrentSelectedTiles = tiles;
      HighlightTiles();
   }
   private void BishopRule(Vector3 position)
   {
      var tiles = new List<Tile>();
      
      var x = (int)position.x;
      var y = (int)position.z;
      
      
      var possibleMoves = new List<(int, int)>();
      // Generate all diagonal moves within the bounds of the chessboard
      // Diagonally up-right
      for (int i = 1; i < 8; i++)
      {
         var move = (x + i, y + i);
         var key = new Vector3(move.Item1, 0, move.Item2);
         if (move.Item1 < 8 && move.Item2 < 8 && m_AllTiles.ContainsKey(key))
         {
            possibleMoves.Add(move);
            if (!m_AllTiles[key].IsEmpty) break;
         }
      }

      // Diagonally up-left
      for (int i = 1; i < 8; i++)
      {
         var move = (x - i, y + i);
         var key = new Vector3(move.Item1, 0, move.Item2);
         if (move.Item1 >= 0 && move.Item2 < 8 && m_AllTiles.ContainsKey(key))
         {
            possibleMoves.Add(move);
            if (!m_AllTiles[key].IsEmpty) break;
         }
      }

      // Diagonally down-right
      for (int i = 1; i < 8; i++)
      {
         var move = (x + i, y - i);
         var key = new Vector3(move.Item1, 0, move.Item2);
         if (move.Item1 < 8 && move.Item2 >= 0 && m_AllTiles.ContainsKey(key))
         {
            possibleMoves.Add(move);
            if (!m_AllTiles[key].IsEmpty) break;
         }
      }

      // Diagonally down-left
      for (int i = 1; i < 8; i++)
      {
         var move = (x - i, y - i);
         var key = new Vector3(move.Item1, 0, move.Item2);
         if (move.Item1 >= 0 && move.Item2 >= 0 && m_AllTiles.ContainsKey(key))
         {
            possibleMoves.Add(move);
            if (!m_AllTiles[key].IsEmpty) break;
         }
      }
      
      foreach (var key in possibleMoves.Select(move => new Vector3(move.Item1, 0, move.Item2)))
      {
         if (m_AllTiles.ContainsKey(key))
         {
            tiles.Add(m_AllTiles[key]);
         }
      }

      PreviousSelectedTiles = CurrentSelectedTiles;
      CurrentSelectedTiles = tiles;
      HighlightTiles();
   }
   private void QueenRule(Vector3 position)
   {
      
      var tiles = new List<Tile>();

      var x = (int)position.x;
      var y = (int)position.z;

      var possibleMoves = new List<(int, int)>();

      // Generate all horizontal and vertical moves within the bounds of the chessboard
      // Horizontal right
      for (int i = x + 1; i < 8; i++)
      {
          var move = (i, y);
          var key = new Vector3(move.Item1, 0, move.Item2);
          if (m_AllTiles.ContainsKey(key))
          {
              possibleMoves.Add(move);
              if (!m_AllTiles[key].IsEmpty) break;
          }
      }

      // Horizontal left
      for (int i = x - 1; i >= 0; i--)
      {
          var move = (i, y);
          var key = new Vector3(move.Item1, 0, move.Item2);
          if (m_AllTiles.ContainsKey(key))
          {
              possibleMoves.Add(move);
              if (!m_AllTiles[key].IsEmpty) break;
          }
      }

      // Vertical up
      for (int i = y + 1; i < 8; i++)
      {
          var move = (x, i);
          var key = new Vector3(move.Item1, 0, move.Item2);
          if (m_AllTiles.ContainsKey(key))
          {
              possibleMoves.Add(move);
              if (!m_AllTiles[key].IsEmpty) break;
          }
      }

      // Vertical down
      for (int i = y - 1; i >= 0; i--)
      {
          var move = (x, i);
          var key = new Vector3(move.Item1, 0, move.Item2);
          if (m_AllTiles.ContainsKey(key))
          {
              possibleMoves.Add(move);
              if (!m_AllTiles[key].IsEmpty) break;
          }
      }

      // Generate all diagonal moves within the bounds of the chessboard
      // Diagonally up-right
      for (int i = 1; i < 8; i++)
      {
          var move = (x + i, y + i);
          var key = new Vector3(move.Item1, 0, move.Item2);
          if (move.Item1 < 8 && move.Item2 < 8 && m_AllTiles.ContainsKey(key))
          {
              possibleMoves.Add(move);
              if (!m_AllTiles[key].IsEmpty) break;
          }
      }

      // Diagonally up-left
      for (int i = 1; i < 8; i++)
      {
          var move = (x - i, y + i);
          var key = new Vector3(move.Item1, 0, move.Item2);
          if (move.Item1 >= 0 && move.Item2 < 8 && m_AllTiles.ContainsKey(key))
          {
              possibleMoves.Add(move);
              if (!m_AllTiles[key].IsEmpty) break;
          }
      }

      // Diagonally down-right
      for (int i = 1; i < 8; i++)
      {
          var move = (x + i, y - i);
          var key = new Vector3(move.Item1, 0, move.Item2);
          if (move.Item1 < 8 && move.Item2 >= 0 && m_AllTiles.ContainsKey(key))
          {
              possibleMoves.Add(move);
              if (!m_AllTiles[key].IsEmpty) break;
          }
      }

      // Diagonally down-left
      for (int i = 1; i < 8; i++)
      {
          var move = (x - i, y - i);
          var key = new Vector3(move.Item1, 0, move.Item2);
          if (move.Item1 >= 0 && move.Item2 >= 0 && m_AllTiles.ContainsKey(key))
          {
              possibleMoves.Add(move);
              if (!m_AllTiles[key].IsEmpty) break;
          }
      }

      // Add the possible moves to the tiles list
      foreach (var key in possibleMoves.Select(move => new Vector3(move.Item1, 0, move.Item2)))
      {
          if (m_AllTiles.ContainsKey(key))
          {
              tiles.Add(m_AllTiles[key]);
          }
      }

      PreviousSelectedTiles = CurrentSelectedTiles;
      CurrentSelectedTiles = tiles;
      HighlightTiles();
   }
   

   private void PawnRule(Piece piece)
   {
      
   }



   private void HighlightTiles()
   {
      if (PreviousSelectedTiles.Count > 0)
      {
         foreach (var tile in PreviousSelectedTiles)
            tile.HighlightTile(false);
      }

      if (CurrentSelectedTiles.Count > 0)
      {
         foreach (var tile in CurrentSelectedTiles)
            tile.HighlightTile(true);
      }
   }


   public void MovePlayer(Tile tile)
   {
      if (!CurrentSelectedTiles.Contains(tile)) return;
      
      var hashTable = iTween.Hash("position", tile.Position, "speed", m_PlayerMoveSpeed, "easetype", iTween.EaseType.easeInOutCubic, 
         "oncompletetarget",gameObject, "oncomplete","OnCompletePlayerMove","oncompleteparams",tile);
      iTween.MoveTo(m_Player.gameObject,hashTable);
   }


   private void OnCompletePlayerMove(Tile tile)
   {
      KingRule(m_Player.Position);
      tile.SetPieceHere(m_Player);
   }
   
}
