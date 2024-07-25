using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChessShopView : UIView
{
    [SerializeField] private Button m_BackButton;
    [SerializeField] private Button m_PiecesButton;
    [SerializeField] private Button m_BoardButton;
    [SerializeField] private Transform m_PiecesParent;
    [SerializeField] private Transform m_BoardsParent;
    [SerializeField] private ShopItem m_ShopItemPrefab;
    [SerializeField] private Text m_CurrencyText;


    [Header("Boards")] 
    [SerializeField] private ShopItemData[] m_AllBoardsData;
    [Header("Pieces")] 
    [SerializeField] private ShopItemData[] m_AllPiecesData;
    
    
    public override void Initialize()
    {
        m_BackButton.onClick.AddListener(() =>
        {
            GameManager.Instance.IsGameEnded = false;
            SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
            SceneManager.LoadScene("Gameplay");
        });
        
        m_PiecesButton.onClick.AddListener(OnPiecesClick);
        m_BoardButton.onClick.AddListener(OnBoardsClick);
        UIEvents.a_UpdateCoins += UpdateCoins;
        InstantiateAllItems();
    }

    private void InstantiateAllItems()
    {
        
        foreach (var boardData in m_AllBoardsData)
        {
            var board = Instantiate(m_ShopItemPrefab, m_BoardsParent);
            board.Initialize(boardData,m_AllBoardsData.ToList().IndexOf(boardData));
        }
        
        foreach (var pieceData in m_AllPiecesData)
        {
            var board = Instantiate(m_ShopItemPrefab, m_PiecesParent);
            board.Initialize(pieceData,m_AllPiecesData.ToList().IndexOf(pieceData));
        }
        
        m_PiecesParent.gameObject.SetActive(true);
        m_BoardsParent.gameObject.SetActive(false);
    }


    private void OnPiecesClick()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        m_PiecesParent.gameObject.SetActive(true);
        m_BoardsParent.gameObject.SetActive(false);
    }
    
    private void OnBoardsClick()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        m_PiecesParent.gameObject.SetActive(false);
        m_BoardsParent.gameObject.SetActive(true);
    }
    private void UpdateCoins(int coins)
    {
        m_CurrencyText.text = coins.ToString();
    }
    
}

[System.Serializable]
public struct ShopItemData
{
    public string ItemName;
    public Sprite ItemImage;
    public eShopItemType ItemType;

    [field: SerializeField] public bool IsLocked
    {
        get => ((PlayerPrefs.GetInt(Constants.LockedItem + ItemType.ToString() + ItemName, 1) == 1 && Price > 0));
        set
        {
            PlayerPrefs.SetInt(Constants.LockedItem+ItemType.ToString()+ItemName,value ? 1 : 0 );
            PlayerPrefs.Save();
        }
        
    }

    public int Price;
}
