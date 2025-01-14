using Models.GameModes.Balloons;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using ScriptableObjects.Sources.GameModes.Balloons;

namespace GameModes.Balloons
{
    [RequireComponent(typeof(TextMeshPro), typeof(AudioSource))]
    public class Letter : MonoBehaviour
    {
        [Header("Letter Set")]
        [SerializeField] private LetterSet _letterSet;

        [Header("Life Time")]
        [SerializeField] private float _lifeTime;

        public event UnityAction Died;
        
        private LetterInfo _info;
        private TextMeshPro _textMesh;
        private AudioSource _audioSource;

        private IEnumerator _lifeTimeCoroutine;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshPro>();
            _audioSource = GetComponent<AudioSource>();

            _lifeTimeCoroutine = LifeTimeCoroutine();
        }

        public void Init(Vector2 position, Color color)
        {
            transform.position = position;
            SetRandomLetterInfo();

            _textMesh.text = _info.Character;
            _textMesh.color = color;

            _audioSource.clip = _info.Sound;
            _audioSource.Play();
        }

        private void OnEnable()
        {
            Died += OnDied;
            StartCoroutine(_lifeTimeCoroutine);
        }


        private void OnDisable()
        {
            Died -= OnDied;
            StopAllCoroutines();
        }

        private void OnDied()
        {
            Destroy(gameObject);
        }

        private void SetRandomLetterInfo()
        {
            int index = Random.Range(0, _letterSet.LetterInfos.Length);

            _info = _letterSet.LetterInfos[index];
        }

        private IEnumerator LifeTimeCoroutine()
        {
            yield return new WaitForSeconds(_lifeTime);
            Died?.Invoke();
            yield break;
        }
    }
}
