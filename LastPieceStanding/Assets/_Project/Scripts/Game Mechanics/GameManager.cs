using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int m_PieceCounter = 0;

    public static GameManager Instance;


    public bool IsGameEnded { get; set; } = true;

    private void Start()
    {
        Instance = this;
        Invoke(nameof(StartGame),3f);
    }


    private void StartGame()
    {
        IsGameEnded = false;
    }
    
    
}
