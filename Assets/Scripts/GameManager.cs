using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Sprite[] CardFrontFaces;
    public GameObject Container;
    public GameObject Card;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI LevelText;

    private int level = 1;
    private int score = 0;
    private int matchedPairs = 0;

    private List<Card> activeCards = new List<Card>();
    private List<Card> flippedCards = new List<Card>();

    private void Start()
    {
        ShowCurrentText();
        GenerateCards();
    }

    void ShowCurrentText()
    {
        LevelText.text = "Level : " + level;
        scoreText.text = "Score : " + score;
    }

    int RandomIndex()
    {
        return Random.Range(0, CardFrontFaces.Length);
    }

    void GenerateCards()
    {
        int pairCount = level == 1 ? 4 : level == 2 ? 5 : 6;
        List<Sprite> usedSprites = new();

        for (int i = 0; i < pairCount; i++)
        {
            int idx;
            Sprite chosen;

            do
            {
                idx = RandomIndex();
                chosen = CardFrontFaces[idx];
            } while (usedSprites.Contains(chosen));

            usedSprites.Add(chosen);

            GameObject cardObj1 = Instantiate(Card, Container.transform);
            GameObject cardObj2 = Instantiate(Card, Container.transform);

            Card card1 = cardObj1.GetComponent<Card>();
            Card card2 = cardObj2.GetComponent<Card>();

            card1.cardFace = chosen;
            card2.cardFace = chosen;

            activeCards.Add(card1);
            activeCards.Add(card2);
        }

        ShuffleCards();
    }

    void ShuffleCards()
    {
        for (int i = activeCards.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (activeCards[i], activeCards[j]) = (activeCards[j], activeCards[i]);
        }

        for (int i = 0; i < activeCards.Count; i++)
        {
            activeCards[i].transform.SetSiblingIndex(i);
        }
    }

    public bool CanFlip(Card card)
    {
        if (flippedCards.Contains(card) || flippedCards.Count >= 2)
            return false;

        flippedCards.Add(card);

        if (flippedCards.Count == 2)
        {
            Invoke(nameof(CheckMatch), 0.5f);
        }

        return true;
    }

    void CheckMatch()
    {
        if (flippedCards.Count < 2) return;

        Card c1 = flippedCards[0];
        Card c2 = flippedCards[1];

        if (c1.cardFace == c2.cardFace)
        {
            score++;
            matchedPairs++;
        }
        else
        {
            c1.FlipBack();
            c2.FlipBack();
        }

        flippedCards.Clear();
        ShowCurrentText();

        int pairCount = level == 1 ? 4 : level == 2 ? 5 : 6;
        if (matchedPairs >= pairCount)
        {
            level++;
            matchedPairs = 0;
            ClearBoard();
            GenerateCards();
            ShowCurrentText();
        }
    }

    void ClearBoard()
    {
        foreach (Card card in activeCards)
        {
            Destroy(card.gameObject);
        }
        activeCards.Clear();
        flippedCards.Clear();
    }
}
