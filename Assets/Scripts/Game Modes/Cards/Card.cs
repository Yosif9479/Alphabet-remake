using UnityEngine;
using UnityEngine.UI;
using Models.GameModes.Cards;
using TMPro;

namespace GameModes.Cards
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _letter;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _icon;

        public void Init(CardInfo cardInfo)
        {
            _letter.text = cardInfo.Letter;
            _name.text = cardInfo.Name;
            _icon.sprite = cardInfo.Icon;
        }
    }
}