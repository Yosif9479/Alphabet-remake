using GameModes.MemoryGame;
using UnityEngine;

public class VictoryHandler : MonoBehaviour
{
    [Header("Card Spawner")]
    [Tooltip("Used to count spawned cards")]
    [SerializeField] private CardSpawner _cardSpawner;

    [Header("Card Compare")]
    [Tooltip("Used to handle if card disappeared")]
    [SerializeField] private CardCompare _cardCompare;

    [Header("Victory Panel")]
    [SerializeField] private GameObject _victoryPanel;

    private int _cardsCount;

    #region MONO
    private void Start()
    {
        _cardsCount = _cardSpawner.CardsCount;
    }

    private void OnEnable()
    {
        _cardCompare.CardDisappeared += OnCardDisappeared;
    }

    private void OnDisable()
    {
        _cardCompare.CardDisappeared -= OnCardDisappeared;
    }
    #endregion

    private void OnCardDisappeared()
    {
        _cardsCount--;

        if (_cardsCount == 0)
        {
            OnVictory();
        }
    }

    private void OnVictory()
    {
        _victoryPanel.SetActive(true);
    }
}
