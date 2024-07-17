using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int m_PieceCounter = 0;

    public static GameManager Instance;


    private void Start()
    {
        Instance = this;
    }
}
