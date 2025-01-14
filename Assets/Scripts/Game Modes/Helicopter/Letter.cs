using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace GameModes.Helicopter
{
    [RequireComponent(typeof(TextMeshPro), typeof(RectTransform), typeof(AudioSource))]
    public class Letter : MonoBehaviour
    {
        #region EVENTS

        [HideInInspector] public UnityEvent ReachedVariant = new UnityEvent();

        #endregion
        
        [Header("Sound")]
        [SerializeField] private float _soundPlayInterval = 3f;
        
        [Header("Movement")]
        [SerializeField] private float _movementSpeed = 5f;

        [Header("Scaling")] 
        [SerializeField] private float _scalingSpeed = 1f;

        private Helicopter _helicopter;
        
        private TextMeshPro _textMesh;
        private Vector2 _targetPosition;
        private AudioSource _audioSource;

        private bool _canMove;
        private bool _canScaleUp;
        private bool _canPlaySound = true;
        
        public void Init(LetterInfo letterInfo, Helicopter helicopter, Vector2 targetPosition)
        {
            _textMesh.text = letterInfo.Character.ToString();
            _audioSource.clip = letterInfo.Sound;
            _targetPosition = targetPosition;
            _helicopter = helicopter;

            _textMesh.enabled = false;

            StartCoroutine(PlaySoundWithIntervalCoroutine());
            ListenToEvents();
        }
        
        #region MONO

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshPro>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnDestroy()
        {
            StopListeningToEvents();
        }

        #endregion
        
        private void Update()
        {
            if (_canMove)
            {
                ApplyMovement();
            }

            if (_canScaleUp)
            {
                ApplyScaling();
            }

            if (ReachedLetterVariant && _canMove)
            {
                _canMove = false;
                ReachedVariant?.Invoke();
            }
        }

        private bool ReachedLetterVariant => Vector2.Distance(transform.position, _targetPosition) <= 0.01;

        private void ApplyMovement()
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _movementSpeed * Time.deltaTime);
        }

        private void ApplyScaling()
        {
            transform.localScale += Vector3.one * (_scalingSpeed * Time.deltaTime);
            
            _canScaleUp = transform.localScale.x < 1;
        }

        private IEnumerator PlaySoundWithIntervalCoroutine()
        {
            yield return new WaitForSeconds(1);
            
            while (true)
            {
                if (_canPlaySound)
                {
                    _audioSource.Play();
                }
                
                yield return new WaitForSeconds(_soundPlayInterval);
            }
        }

        private void OnHelicopterDoorStartedOpening()
        {
            transform.localScale = GetNormalizedScale();

            _canPlaySound = false;
            _textMesh.enabled = true;
        }
        
        private void OnHelicopterDoorOpened()
        {
            Vector2 worldPosition = transform.position;
            
            transform.SetParent(null);

            transform.position = worldPosition;
            transform.localScale = math.abs(transform.localScale);
            
            _canMove = true;
            _canScaleUp = true;
        }

        private Vector3 GetNormalizedScale()
        {
            float x = transform.lossyScale.x > 0 ? transform.localScale.x : -transform.localScale.x;
            float y = transform.localScale.y;
            float z = transform.localScale.z;
            
            return new Vector3(x, y, z);
        }
        
        private void ListenToEvents()
        {
            _helicopter.DoorStartedOpening.AddListener(OnHelicopterDoorStartedOpening);
            _helicopter.DoorOpened.AddListener(OnHelicopterDoorOpened);
        }

        private void StopListeningToEvents()
        {
            _helicopter.DoorStartedOpening.RemoveListener(OnHelicopterDoorStartedOpening);
            _helicopter.DoorOpened.RemoveListener(OnHelicopterDoorOpened);
        }
    }
}