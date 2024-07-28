using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DataManager
{


    private static DataManager s_Instance;


    public static DataManager Instance => s_Instance ??= new DataManager();

    private DataManager()
    {
        // UIEvents.m_SoundToggleUpdate?.Invoke(IsSoundOn);
        // UIEvents.m_HepticsToggleUpdate?.Invoke(IsHapticsOn);
        // UIEvents.m_gameplayCurrencyUpdate?.Invoke(Currency);
        // UIEvents.m_gameplayScoreUpdate?.Invoke(Score);
    }

    private int m_Level = 1;
    private int m_Score = 0;
    private int m_Currency = 100;
    private PlayerModel m_playerModelData;




    public int Level
    {
        get
        {
            return m_Level;
        }
        set
        {
            m_Level = value;
            SetData();
        }
    }

    public int Score
    {
        get
        {
            return m_Score;
        }
        set
        {
            m_Score = value;
            SetData();
        }
    }

    public int Currency
    {
        get
        {
            return m_Currency;
        }
        set
        {
            m_Currency = value;
            SetData();
        }
    }

    public bool IsSoundOn
    {
        get => PlayerPrefs.GetInt(Constants.SoundKey, 1) == 1;
        set
        {
            PlayerPrefs.SetInt(Constants.SoundKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    
    public bool IsRandomLevel
    {
        get => PlayerPrefs.GetInt(Constants.SoundKey, 0) == 1;
        set
        {
            PlayerPrefs.SetInt(Constants.SoundKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public bool IsHapticsOn
    {
        get => PlayerPrefs.GetInt(Constants.HapticsKey, 1) == 1;
        set
        {
            PlayerPrefs.SetInt(Constants.HapticsKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public bool IsRated
    {
        get => PlayerPrefs.GetInt(Constants.RateUsKey, 0) == 1;
        set
        {
            PlayerPrefs.SetInt(Constants.RateUsKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }


    private void Awake()
    {
        s_Instance = this;
    }


    private void SetData()
    {
        m_playerModelData.level = m_Level;
        m_playerModelData.score = m_Score;
        m_playerModelData.currency = m_Currency;

        string json = JsonUtility.ToJson(m_playerModelData);
        PlayerPrefs.SetString("UserData", json);
    }



    private void GetData()
    {
        string jsonString = PlayerPrefs.GetString("UserData");
        m_playerModelData = JsonUtility.FromJson<PlayerModel>(jsonString);
        m_Level = m_playerModelData.level;
        m_Score = m_playerModelData.score;
        m_Currency = m_playerModelData.currency;
    }





}



[System.Serializable]
public class PlayerModel
{
    public int level;
    public int score;
    public int currency;
}