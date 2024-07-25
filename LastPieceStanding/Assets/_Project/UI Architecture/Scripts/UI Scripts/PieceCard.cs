using UnityEngine;
using UnityEngine.UI;

public class PieceCard : MonoBehaviour
{
    [SerializeField] private Text m_PieceName;
    [SerializeField] private Image m_PieceImage;
    [SerializeField] private Button m_CardButton;
    [SerializeField] private GameObject m_Toggle;

    private ePieceName e_PieceName;
    private GamePlayPanelView m_Controller;
    
    public void Init(Sprite pieceImage, ePieceName pieceName, GamePlayPanelView controller)
    {
        m_PieceImage.sprite = pieceImage;
        m_PieceName.text = pieceName.ToString();
        e_PieceName = pieceName;
        m_Controller = controller;
        SelectionStatus(false);
        m_CardButton.onClick.AddListener(OnCardClicked);
    }


    private void OnCardClicked()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        SoundManager.Instance.PlaySelectionHaptics();
        if ((m_Controller.SelectedCard != null && m_Controller.SelectedCard != this) || m_Controller.SelectedCard == null) 
        {
            if (m_Controller.SelectedCard != null)
                m_Controller.SelectedCard.SelectionStatus(false);
            BoardManager.Instance.GetTilesToMoveOn(e_PieceName);
            m_Controller.SelectedCard = this;
            SelectionStatus(true);
        }
        else
        {
            BoardManager.Instance.GetTilesToMoveOn(ePieceName.King);
            m_Controller.SelectedCard.SelectionStatus(false);
            m_Controller.SelectedCard = null;
        }
    }


    private void SelectionStatus(bool isSelected)
    {
        m_Toggle.SetActive(isSelected);
    }
    
    
    
}
