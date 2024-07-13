using UnityEngine;
using UnityEngine.UI;

public class PieceCard : MonoBehaviour
{
    [SerializeField] private Text m_PieceName;
    [SerializeField] private Image m_PieceImage;
    [SerializeField] private Button m_CardButton;

    private ePieceName e_PieceName;
    
    public void Init(Sprite pieceImage, ePieceName pieceName)
    {
        m_PieceImage.sprite = pieceImage;
        m_PieceName.text = pieceName.ToString();
        e_PieceName = pieceName;
        m_CardButton.onClick.AddListener(OnCardClicked);
    }


    private void OnCardClicked()
    {
        BoardManager.Instance.GetTilesToMoveOn(e_PieceName);
    }
    
    
    
}
