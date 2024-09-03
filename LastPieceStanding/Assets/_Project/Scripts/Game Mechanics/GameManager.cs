using System;
using System.Collections;
using System.Collections.Generic;
// using SupersonicWisdomSDK;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int m_PieceCounter = 0;

    public static GameManager Instance;

    private static bool IsInitialized = false;

    public bool IsGameEnded { get; set; } = true;

    private void Awake()
    {
        // Subscribe
        // SupersonicWisdom.Api.AddOnReadyListener(OnSupersonicWisdomReady);    
        // Then initialize
        // SupersonicWisdom.Api.Initialize();
    }


    void OnSupersonicWisdomReady()
    {
        Debug.Log("Supersonic SDK Intialized");
        IsInitialized = true;
        Invoke(nameof(StartGame),3f);
    }

    private void Start()
    {
        Instance = this;
        OnSupersonicWisdomReady();
        if (IsInitialized)
            Invoke(nameof(StartGame),3f);
    }


    private void StartGame()
    {
        IsGameEnded = false;
        // SupersonicWisdom.Api.NotifyLevelStarted(ESwLevelType.Regular,(long)(LevelManager.Instance.Level + 1),null);
    }
    
    
}
