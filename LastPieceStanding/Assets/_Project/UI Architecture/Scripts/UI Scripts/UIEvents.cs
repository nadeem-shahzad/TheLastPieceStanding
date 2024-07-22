using UnityEngine;
using  System;


public class UIEvents
{
    public static Action<int> m_gameplayScoreUpdate;  
    public static Action<int> m_gameplayCurrencyUpdate;
    public static Action<bool> m_MusicToggleUpdate;
    public static Action<bool> m_SoundToggleUpdate;
    public static Action<bool> m_HepticsToggleUpdate;


    public static Action a_DeleteSelectedCard;
    public static Action<int> a_UpdateCoins;
}
