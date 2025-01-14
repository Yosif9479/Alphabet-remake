using Models.GameModes.MemoryGame;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameModes.MemoryGame
{
    [RequireComponent(typeof(Animator), typeof(Button))]
    public class Card : MonoBehaviour
    {
        #region EVENTS

        public static event UnityAction<Card> Selected;
        public static event UnityAction<Card> Deselected;

        #endregion

        [Header("Particle Effect")]
        [SerializeField] private GameObject _particleEffect;

        public CardInfo Info { get; private set; }
        public bool IsInitialized { get; private set; }
       
        private Button _button;
        private Animator _animator;
        private TextMeshProUGUI _letterTextMesh;

        private bool _isSelected = false;
        private bool _isFlipping = false;
        private static int _activeCardsNumber = 0;

        #region MONO
        private void Awake()
        {
            _button = GetComponent<Button>();
            _animator = GetComponent<Animator>();
            _letterTextMesh = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void OnEnable()
        {
            _button.onClick.AddListener(Flip);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(Flip);
        }

        #endregion

        public void Init(CardInfo cardInfo)
        {
            Info = cardInfo;

            _letterTextMesh.text = cardInfo.Character;

            _letterTextMesh.gameObject.SetActive(false);

            IsInitialized = true;
        }

        public void Flip()
        {
            if (_activeCardsNumber >= 2 && !_isSelected)
            {
                return;
            }

            if (_isFlipping)
            {
                return;
            }

            if (!_isSelected)
            {
                _activeCardsNumber++;
            }

            _isFlipping = true;
            _animator.SetTrigger("Flip");

            _isSelected = !_isSelected;
        }

        public void Disappear()
        {
            Instantiate(_particleEffect).transform.position = (Vector2) transform.position;

            _activeCardsNumber--;

            Destroy(gameObject);
        }

        //Called from animation
        private void ToggleLetter()
        {
            _letterTextMesh.gameObject.SetActive(_isSelected);
        }

        //Called from animation
        private void OnCardFlipped()
        {
            _isFlipping = false;

            if (_isSelected)
            {
                Selected?.Invoke(this);
            }
            else
            {
                _activeCardsNumber--;
                Deselected?.Invoke(this);
            }
        }
    }
}