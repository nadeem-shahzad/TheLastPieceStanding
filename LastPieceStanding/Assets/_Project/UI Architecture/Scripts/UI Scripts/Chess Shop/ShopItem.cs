using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Image m_ItemImage;
    [SerializeField] private TMP_Text m_TitleText;
    [SerializeField] private GameObject m_LockIcon;
    [SerializeField] private Button m_ItemButton;
    [SerializeField] private int m_Index = 0;

    private eShopItemType m_ItemType;

    public void Initialize(ShopItemData data, int index)
    {
        m_ItemImage.sprite = data.ItemImage;
        m_TitleText.text = data.ItemName;
        m_LockIcon.SetActive(data.IsLocked);
        m_ItemType = data.ItemType;
        m_Index = index;
        m_ItemButton.onClick.AddListener(OnClickItem);
    }



    private void OnClickItem()
    {
        if (m_ItemType == eShopItemType.Pieces)
        {
            PlayerPrefs.SetInt(Constants.EquippedPiece, m_Index);
            
            Debug.LogError("Piece Clicked");
        }
        else
        {
            PlayerPrefs.SetInt(Constants.EquippedBoard, m_Index);
            Debug.LogError("Board Clicked");
        }
    }
    
    
    
    
    
}

public enum eShopItemType
{
    Pieces, Board
}
