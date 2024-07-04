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
   
   public void SetColor(bool isBlack)
   {
       m_Renderer.sharedMaterial = isBlack ? m_Black : m_White;
       m_Dot.gameObject.SetActive(false);
   }


   public void SetPieceHere()
   {
       
   }


   private void OnMouseDown()
   {
       Debug.LogError($"Tile Down :: {this.name}");
   }
   
   private void OnMouseUp()
   {
       Debug.LogError($"Tile Up :: {this.name}");
   }
}
