using System;
using ScriptableObjects.Sources.GameModes.Helicopter;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace GameModes.Helicopter
{
    [RequireComponent(typeof(Animator))]
    public class Helicopter : MonoBehaviour
    {
        #region EVENTS

        [HideInInspector] public UnityEvent DoorOpened;
        [HideInInspector] public UnityEvent DoorStartedOpening;

        #endregion
        
        #region ANIMATION_NAMES

        private static readonly string OpenDoorAnimation = "OpenDoor";

        #endregion
        
        private LetterCompare _letterCompare;
        
        [Header("Bobbing")] 
        [SerializeField] private float _bobbingOffsetY;
        [SerializeField] private float _bobbingSpeed;

        [Header("Horizontal Movement")] 
        [SerializeField] private float _horizontalMovementOffset;
        [SerializeField] private float _horizontalMovementSpeed;
        
        private bool _isFlyingUp;
        private bool _isFlyingRight;

        private bool _allowHorizontalMovement = true;
        private bool _allowBobbing = true;

        private Vector2 _maxPosition;
        private Vector2 _minPosition;

        private Animator _animator;

        public void Init(LetterCompare letterCompare)
        {
            _letterCompare = letterCompare;
            
            ListenToEvents();
        }

        #region MONO
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _animator.SetFloat("MovementSpeed", _horizontalMovementSpeed + _bobbingSpeed);
            ApplyMinAndMaxPosition();
        }

        private void OnDestroy()
        {
            StopListeningToEvents();
        }

        #endregion
        
        private void Update()
        {
            ApplyMovement();
        }

        private void ApplyMovement()
        {
            ApplyBobbing();
            ApplyHorizontalMovement();
        }

        private void ApplyHorizontalMovement()
        {
            if (!_allowHorizontalMovement)
            {
                return;
            }
            
            Vector2 movementDirection = _isFlyingRight ? Vector2.right : Vector2.left;

            transform.Translate(movementDirection * (_horizontalMovementSpeed * Time.deltaTime));
            
            if ( IsReachedMaxPosition() || IsReachedMinPosition())
            {
                _isFlyingRight = !_isFlyingRight;
                Flip();
            }

            return;

            bool IsReachedMaxPosition()
            {
                return _isFlyingRight && transform.position.x > _maxPosition.x;
            }

            bool IsReachedMinPosition()
            {
                return !_isFlyingRight && transform.position.x < _minPosition.x;
            }
        }

        private void ApplyBobbing()
        {
            if (!_allowBobbing)
            {
                return;
            }
            
            Vector2 movementDirection = _isFlyingUp ? Vector2.up : Vector2.down;

            transform.Translate(movementDirection * (_bobbingSpeed * Time.deltaTime));

            if (IsReachedMaxPosition() || IsReachedMinPosition())
            {
                _isFlyingUp = !_isFlyingUp;
            }
            
            return;
            
            bool IsReachedMaxPosition()
            {
                return _isFlyingUp && transform.position.y > _maxPosition.y;
            }

            bool IsReachedMinPosition()
            {
                return !_isFlyingUp && transform.position.y < _minPosition.y;
            }
        }
        
        private void OnCorrectLetterSelected()
        {
            _animator.SetTrigger(OpenDoorAnimation);
        }
        
        private void Flip()
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        private void ApplyMinAndMaxPosition()
        {
            _minPosition.x = transform.position.x - _horizontalMovementOffset;
            _maxPosition.x = transform.position.x + _horizontalMovementOffset;
            _minPosition.y = transform.position.y - _bobbingOffsetY;
            _maxPosition.y = transform.position.y + _bobbingOffsetY;
        }

        #region CALLED_FROM_ANIMATION

        //Called from animation
        private void OnDoorOpened()
        {
            DoorOpened?.Invoke();
        }
        
        //Called from animation
        private void OnDoorStartedOpening()
        {
            _allowHorizontalMovement = false;
            DoorStartedOpening?.Invoke();
        }
        
        //Called from animation
        private void OnDoorClosed()
        {
            _allowHorizontalMovement = true;
        }

        #endregion

        private void ListenToEvents()
        {
            _letterCompare.CorrectLetterSelected.AddListener(OnCorrectLetterSelected);
        }

        private void StopListeningToEvents()
        {
            _letterCompare.CorrectLetterSelected.RemoveListener(OnCorrectLetterSelected);
        }
    }
}