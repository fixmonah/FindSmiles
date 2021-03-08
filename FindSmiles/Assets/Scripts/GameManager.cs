using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Card[] _cards;
    //[SerializeField] private Sprite _front;

    private string[] _selectedSpritesName;

    // Start is called before the first frame update
    void Start()
    {
        //_card.SetFrontImage(_front);

        // Get random sprites name from DataBase
        _selectedSpritesName = GetRandomSpriteNames();
        // Set images in cards
        SetImageInCards(_selectedSpritesName, _cards);

        StartCoroutine(ShowCards());
    }
    IEnumerator ShowCards() 
    {
        yield return new WaitForEndOfFrame();
        //Show Cards
        foreach (var card in _cards)
        {
            card.Show();
            card.TurnOverFront();
        }
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

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.H)){ _card.Hide(); }
        //if (Input.GetKeyUp(KeyCode.S)) { _card.Show(); }
        //if (Input.GetKeyUp(KeyCode.F)) { _card.TurnOverFront(); }
        //if (Input.GetKeyUp(KeyCode.B)) { _card.TurnOverBack(); }
    }
}
