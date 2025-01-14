using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Math = System.Math;
using Random = Additionals.Random;
using Models.GameModes.Balloons;
using ScriptableObjects.Sources.GameModes.Balloons;
using System.Threading.Tasks;

namespace GameModes.Balloons
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(AudioSource))]
    public class Balloon : MonoBehaviour
    {
        #region EVENTS

        public event UnityAction Died;

        #endregion

        [Header("Balloon Set")]
        [SerializeField] private BalloonSet _balloonSet;

        [Header("Balloon Settings")]
        [SerializeField] private float _lifeTime = 10;
        [SerializeField] private float _flySpeed = 1;
        [SerializeField] private float _xMovementSpeed = 1f;
        [SerializeField] private Letter _letterPrefab;

        [Header("Wobble Settings")]
        [SerializeField] private float _wobbleDegreee = 15;
        [SerializeField] private float _wobbleSpeed = 1;

        [Header("Particle Settings")]
        [SerializeField] private GameObject _particle;

        private AudioSource _audioSource;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidBody;
        private Color _color;

        private bool _isRotatingRight;

        private IEnumerator _lifeTimeCoroutine;

        #region MONO

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
        }

        
        #endregion

        public void Init(Vector2 position)
        {
            transform.position = position;
            SetRandomBalloonInfo();
            _lifeTimeCoroutine = LifeTimeCoroutine();
            StartCoroutine(_lifeTimeCoroutine);
        }

        private void Update()
        {
            ApplyRotation();
            ApplyMovement();
        }

        private void OnMouseDown()
        {
            Burst();
        }
        
        private void Burst()
        {
            _audioSource.Play();
            SpawnLetter();
            SpawnParticles();
            Died?.Invoke();
        }

        private void ApplyMovement()
        {
            float xVelocity = _isRotatingRight ? _xMovementSpeed : -_xMovementSpeed;

            Vector2 velocity = new Vector2(xVelocity, _flySpeed);

            _rigidBody.linearVelocity = velocity;
        }

        private void ApplyRotation()
        {
            float targetRotation = _isRotatingRight ? _wobbleSpeed : -_wobbleSpeed;
            targetRotation *= Time.deltaTime;

            transform.Rotate(0, 0, targetRotation);

            if (Math.Abs(transform.rotation.eulerAngles.z) >= _wobbleDegreee)
            {
                _isRotatingRight = !_isRotatingRight;
            }
        }

        private void SetRandomBalloonInfo()
        {
            int balloonInfoIndex = Random.Range(0, _balloonSet.BalloonInfos.Length);

            BalloonInfo balloonInfo = _balloonSet.BalloonInfos[balloonInfoIndex];

            _spriteRenderer.sprite = balloonInfo.Sprite;
            _color = balloonInfo.Color;
        }

        private void SpawnLetter()
        {
            Vector2 letterPosition = GetClickPosition();
            Letter letter = Instantiate(_letterPrefab);
            letter.Init(letterPosition, _color);
        }

        private void SpawnParticles()
        {
            Vector2 particlePosition = GetClickPosition();
            Instantiate(_particle).transform.position = particlePosition;
        }

        private IEnumerator LifeTimeCoroutine()
        {
            yield return new WaitForSeconds(_lifeTime);
            Died?.Invoke();
            yield break;
        }

        private static Vector2 GetClickPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

    }
}
