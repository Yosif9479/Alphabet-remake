using Models.GameModes.Cards;
using ScriptableObjects.Sources.GameModes.Cards;
using UnityEngine;

namespace GameModes.Cards
{
    public class CardSwitcher : MonoBehaviour
    {
        [Header("Card Set")]
        [SerializeField] private CardSet _cardSet;

        [Header("Main Card")]
        [SerializeField] private Card _card;

        private int _currentCardId;

        private CardInfo _currentCardInfo;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            UpdateCard();
        }

        public void PlayAudio()
        {
            _audioSource.clip = _currentCardInfo.AudioClip;

            _audioSource.Play();
        }

        private void UpdateCard()
        {
            UpdateCurrentCardInfo();

            PlayAudio();

            _card.Init(_currentCardInfo);
        }

        private void UpdateCurrentCardInfo()
        {
            _currentCardInfo = _cardSet.CardInfos[_currentCardId];
        }

        public void Next()
        {
            _currentCardId++;

            if (_currentCardId == _cardSet.CardInfos.Length)
            {
                _currentCardId = 0;
            }

            UpdateCard();
        }

        public void Previous()
        {
            _currentCardId--;

            if (_currentCardId == -1)
            {
                _currentCardId = _cardSet.CardInfos.Length - 1;
            }

            UpdateCard();
        }
    }
}
