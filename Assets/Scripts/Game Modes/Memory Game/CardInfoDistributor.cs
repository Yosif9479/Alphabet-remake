using GameModes.MemoryGame;
using Models.GameModes.MemoryGame;
using ScriptableObjects.Sources.GameModes.MemoryGame;
using System.Collections.Generic;
using UnityEngine;

public class CardInfoDistributor
{
    private CardSet _cardSet;

    private Card[,] _cardsMatrix;
    private int _xMatrixSize;
    private int _yMatrixSize;

    private Queue<CardInfo> _cardInfoQueue = new Queue<CardInfo>();
    
    public CardInfoDistributor(Card[,] cardsMatrix, CardSet cardSet, int xSize, int ySize) 
    { 
        _cardsMatrix = cardsMatrix;
        _cardSet = cardSet;
        _xMatrixSize = xSize;
        _yMatrixSize = ySize;
    }

    public void DistributeCardInfos()
    {
        int iterationsCount = _xMatrixSize * _yMatrixSize;

        for (int i = 0; i < iterationsCount; i++)
        {
            CardInfo cardInfo;
            Vector2 cardPosition = GetRandomFreePos();

            if (_cardInfoQueue.Count == 0)
            {
                cardInfo = GetRandomCardInfo();

                _cardInfoQueue.Enqueue(cardInfo);

                InitCardOnPosition(cardPosition, cardInfo);

                continue;
            }

            cardInfo = _cardInfoQueue.Dequeue();

            InitCardOnPosition(cardPosition, cardInfo);
        }
    }

    private void InitCardOnPosition(Vector2 cardPos, CardInfo cardInfo)
    {
        int x = (int)cardPos.x;
        int y = (int)cardPos.y;

        _cardsMatrix[x, y].Init(cardInfo);
    }

    private CardInfo GetRandomCardInfo()
    {
        int index = Random.Range(0, _cardSet.CardInfos.Length);

        return _cardSet.CardInfos[index];
    }

    private Vector2 GetRandomFreePos()
    {
        while (true)
        {
            int x = Random.Range(0, _xMatrixSize);
            int y = Random.Range(0, _yMatrixSize);

            if (!_cardsMatrix[x, y].IsInitialized)
            {
                return new Vector2(x, y);
            }
        }
    }
}
