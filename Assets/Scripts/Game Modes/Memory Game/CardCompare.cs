using UnityEngine;
using UnityEngine.Events;

namespace GameModes.MemoryGame
{
    [RequireComponent(typeof(AudioSource))]
    public class CardCompare : MonoBehaviour
    {
        #region EVENTS
        public event UnityAction CardDisappeared;
        #endregion

        private Card _recentSelectedCard;
        private AudioSource _audioSource;

        #region MONO

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            Card.Selected += OnCardSelected;
            Card.Deselected += OnCardDeselected;
        }

        private void OnDisable()
        {
            Card.Selected -= OnCardSelected;
            Card.Deselected -= OnCardDeselected;
        }

        #endregion

        private void OnCardSelected(Card card)
        {
            if (_recentSelectedCard is null)
            {
                _recentSelectedCard = card;
                return;
            }

            Card[] cards = new Card[] { _recentSelectedCard, card };

            _recentSelectedCard = null;
            
            CompareCards(cards);
        }

        private void OnCardDeselected(Card card)
        {
            _recentSelectedCard = null;
        }

        private void CompareCards(Card[] cards)
        {
            if (cards[0].Info.Character == cards[1].Info.Character)
            {
                _audioSource.clip = cards[0].Info.Sound;

                _audioSource.Play();

                foreach (Card card in cards)
                {
                    CardDisappeared?.Invoke();
                    card.Disappear();
                }

                return;
            }

            foreach (Card card in cards)
            {
                card.Flip();
            }
        }
    }
}