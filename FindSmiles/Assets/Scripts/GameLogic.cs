using UnityEngine;


public class GameLogic
{
    public bool IsBlockedGame { get; private set; }

    private Card _card1 = null;
    private Card _card2 = null;
    private GameManager _gameManager;
    private bool _resetCard;

    public GameLogic(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    internal void CardInGame(Card card)
    {
        if (_resetCard)
        {
            _card1 = null;
            _card2 = null;
            _resetCard = false;
        }

        if (CheckForDuplicate(card, _card1, _card2))
        {
            return;
        }

        card.TurnOverFront();

        if (_card1 == null)
        {
            _card1 = card;
        }
        else if (_card2 == null)
        {
            _card2 = card;
        }
        CompareCard(_card1, _card2);
    }

    private bool CheckForDuplicate(Card card, Card card1, Card card2)
    {
        bool answer = false;
        if (card1 != null && card.GetHashCode() == card1.GetHashCode())
        {
            answer = true;
        }
        else if (card2 != null && card.GetHashCode() == card2.GetHashCode())
        {
            answer = true;
        }
        return answer;
    }

    private void CompareCard(Card card1, Card card2)
    {
        if (_card1 == null || _card2 == null)
        {
            return;
        }

        if (_card1.GetName() == _card2.GetName())
        {
             _gameManager.CardIsCompare(card1, card2);
        }
        else
        {
            _gameManager.CardIsNotCompare(card1, card2);
        }

        _resetCard = true;
    }
}
