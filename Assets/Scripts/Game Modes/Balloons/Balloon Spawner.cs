using System.Collections;
using UnityEngine;

namespace GameModes.Balloons
{
    public class BalloonSpawner : MonoBehaviour
    {
        [Header("Balloon")]
        [SerializeField] private Balloon _balloonPrefab;

        [Header("Spawn Settings")]
        [SerializeField] private float _safeZoneX;
        [SerializeField] private float _offsetY;

        [Header("Burst Settings")]
        [SerializeField] private float _timeBetweenBursts;
        [SerializeField] private float _timeBetweenSpawns;
        [SerializeField] private int _balloonsPerBurst;

        private float minSpawnX;
        private float maxSpawnX;

        #region MONO

        private void Start()
        {
            minSpawnX = Camera.main.ScreenToWorldPoint(Vector2.zero).x + _safeZoneX;
            maxSpawnX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - _safeZoneX;

            StartCoroutine(StartBursting());
        }

        #endregion

        private void OnBalloonDied(Balloon balloon)
        {
            balloon.Died -= () => OnBalloonDied(balloon);
            Destroy(balloon.gameObject, 0.1f);
        }

        private void SpawnBalloon()
        {
            Balloon balloon = Instantiate(_balloonPrefab);

            Vector2 position = GetRandomBalloonPosition();

            balloon.Init(position);
            balloon.Died += () => OnBalloonDied(balloon);
        }

        private IEnumerator StartBursting()
        {
            while (true)
            {
                StartCoroutine(BurstCoroutine());
                yield return new WaitForSeconds(_timeBetweenBursts);
            }
        }

        private IEnumerator BurstCoroutine()
        {
            for (int i = 0; i < _balloonsPerBurst; i++)
            {
                yield return new WaitForSeconds(_timeBetweenSpawns);
                SpawnBalloon();
            }
            yield break;
        }

        private Vector2 GetRandomBalloonPosition()
        {
            Vector2 result = new Vector2();

            result.x = Random.Range(minSpawnX, maxSpawnX);
            result.y = _offsetY;

            return result;
        }
    }
}
