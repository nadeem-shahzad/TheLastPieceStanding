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
    private bool m_IsLocked = false;
    private ShopItemData m_Data;
    public void Initialize(ShopItemData data, int index)
    {
        m_Data = data;
        m_ItemImage.sprite = data.ItemImage;

        if (data.IsLocked)
        {
            m_TitleText.text = data.Price + " Coins";
            m_IsLocked = true;
        }
        else
        {
            m_TitleText.text = data.ItemName;
        }

        m_LockIcon.SetActive(data.IsLocked);
        m_ItemType = data.ItemType;
        m_Index = index;
        m_ItemButton.onClick.AddListener(OnClickItem);
    }



    private void OnClickItem()
    {
        if (m_IsLocked)
        {
            if (CurrencyManager.Instance.SpendCoins(m_Data.Price))
            {
                m_Data.IsLocked = false;
                m_IsLocked = false;
                m_TitleText.text = m_Data.ItemName;
                m_LockIcon.SetActive(m_Data.IsLocked);
                EquippedItem();
            }
            else
            {
                Toast.s_Instance.Show("Oops! Not enough coins! Keep playing to earn more!");
            }
        }
        else
        {
            EquippedItem();
        }
    }


    private void EquippedItem()
    {
        Toast.s_Instance.Show($"{m_Data.ItemName} {m_Data.ItemType} is Equipped !!");
        PlayerPrefs.SetInt(m_ItemType == eShopItemType.Pieces ? Constants.EquippedPiece : Constants.EquippedBoard, m_Index);
    }
    
    
    
    
    
}

public enum eShopItemType
{
    Pieces, Board
}
