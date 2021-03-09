using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _score;
    [SerializeField] private Card[] _cards;

    private string[] _selectedSpritesName;
    private GameLogic _gameLogic;
    private bool _gameIsBlocked;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _gameLogic = new GameLogic(this);
        _player = new Player();
        StartGame();
    }

    private void StartGame()
    {
        // Get random sprites name from DataBase
        _selectedSpritesName = GetRandomSpriteNames();
        // Set image in cards
        SetImageInCards(_selectedSpritesName, _cards);
        // Show cards at 5 seconds
        StartCoroutine(ShowCards(5));
    }


    IEnumerator ShowCards(int seconds) 
    {
        _gameIsBlocked = true;
        yield return new WaitForEndOfFrame();
        //Show Cards
        foreach (var card in _cards)
        {
            card.Show();
            card.TurnOverFront();
        }
        yield return new WaitForSeconds(seconds);
        foreach (var card in _cards)
        {
            card.TurnOverBack();
        }
        _gameIsBlocked = false;
    }

    /// <summary>
    /// Get random sprites name from DataBase
    /// </summary>
    private string[] GetRandomSpriteNames()
    {
        List<string> selectedSpritesNames = new List<string>();
        string[] spritesName = DataBase.Instance.GetDataBase().Keys.ToArray();
        if (spritesName.Length >= 3)
        {
            int spriteCounter = 1;
            while (spriteCounter <= 3)
            {
                int rand = UnityEngine.Random.Range(0, spritesName.Length);
                string findeName = spritesName[rand];
                if (! selectedSpritesNames.Contains(findeName))
                {
                    selectedSpritesNames.Add(findeName);
                    spriteCounter++;
                }
            }
        }
        return selectedSpritesNames.ToArray();
    }

    /// <summary>
    /// Set card settings. Name and Card
    /// </summary>
    /// <param name="selectedSpritesName">images names</param>
    /// <param name="cards">cards objects</param>
    private void SetImageInCards(string[] selectedSpritesName, Card[] cards)
    {
        // Clear cards
        foreach (var card in cards)
        {
            card.Clear();
        }


        for (int nameIndex = 0; nameIndex < selectedSpritesName.Length; nameIndex++)
        {
            List<Card> enptyCards = new List<Card>();
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].GetName() == "")
                {
                    enptyCards.Add(cards[i]);
                }
            }
            int rand1 = UnityEngine.Random.Range(0, enptyCards.Count);
            int rand2 = UnityEngine.Random.Range(0, enptyCards.Count);
            while (rand1 == rand2)
            {
                rand2 = UnityEngine.Random.Range(0, enptyCards.Count);
            }
            enptyCards[rand1].SetName(selectedSpritesName[nameIndex]);
            enptyCards[rand1].SetFrontImage(DataBase.Instance.GetDataBase()[selectedSpritesName[nameIndex]]);
            enptyCards[rand2].SetName(selectedSpritesName[nameIndex]);
            enptyCards[rand2].SetFrontImage(DataBase.Instance.GetDataBase()[selectedSpritesName[nameIndex]]);
        }
    }


    internal void OnClickCard(Card card)
    {
        if (!_gameIsBlocked)
        {
            _gameLogic.CardInGame(card);
        }
    }

    internal void CardIsCompare(Card card1, Card card2)
    {
        _player.Win();
        _score.text = _player.Score + "";
        StartCoroutine(HideCard(card1, card2, 1));
    }

    IEnumerator HideCard(Card card1, Card card2, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        card1.Hide();
        card2.Hide();
        yield return new WaitForSeconds(2f);
        RestartGame();
    }

    private void RestartGame()
    {
        bool restartGame = true;
        foreach (var card in _cards)
        {
            if (card.IsActive)
            {
                restartGame = false;
            }
        }
        if (restartGame)
        {
            StartGame();
        }
    }

    internal void CardIsNotCompare(Card card1, Card card2)
    {
        StartCoroutine(TurnBackCard(card1, card2, 1));
    }

    IEnumerator TurnBackCard(Card card1, Card card2, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        card1.TurnOverBack();
        card2.TurnOverBack();
    }

}
