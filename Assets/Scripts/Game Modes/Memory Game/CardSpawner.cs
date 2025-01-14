using ScriptableObjects.Sources.GameModes.MemoryGame;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace GameModes.MemoryGame
{
    public class CardSpawner : MonoBehaviour
    {
        #region EVENTS
        public event UnityAction<Card[]> CardsSpawned;
        #endregion

        [Header("Card Set")]
        [SerializeField] private CardSet _cardSet;

        [Header("Prefabs")]
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private GameObject _cardRowPrefab;

        [Header("Card Matrix")]
        [SerializeField] private int _xMatrixSize;
        [SerializeField] private int _yMatrixSize;

        [Header("Parent Object")]
        [SerializeField] private Transform _parentTransform;

        public int CardsCount
        {
            get
            {
                return _xMatrixSize * _yMatrixSize;
            }
        }

        private Card[,] _cardsMatrix;
        private CardInfoDistributor _distributor;

        private void Start()
        {
            _cardsMatrix = new Card[_xMatrixSize, _yMatrixSize];

            InitMatrix();

            _distributor = new CardInfoDistributor(_cardsMatrix, _cardSet, _xMatrixSize, _yMatrixSize);

            _distributor.DistributeCardInfos();
        }

        private void InitMatrix()
        {
            for (int i = 0; i < _xMatrixSize; i++)
            {
                GameObject row = Instantiate(_cardRowPrefab);

                row.transform.SetParent(_parentTransform, false);

                for (int j = 0; j < _yMatrixSize; j++)
                {
                    Card card = Instantiate(_cardPrefab);
                    
                    card.transform.SetParent(row.transform, false);

                    _cardsMatrix[i, j] = card;
                }
            }

            CardsSpawned?.Invoke(_cardsMatrix.Cast<Card>().ToArray());
        }

        private void OnValidate()
        {
            if ((_xMatrixSize * _yMatrixSize) % 2 != 0)
            {
                Debug.LogError("Incorrect matrix size set");
            }
        }
    }
}
