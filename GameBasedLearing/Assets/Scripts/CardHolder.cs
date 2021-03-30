using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    private Card currentCard = null;
    private Card initialCard = null;
    private RectTransform rectTransform = null;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetInitialCard(Card card)
    {
        this.initialCard = card;
    }

    public Card GetInitialCard()
    {
        return this.initialCard;
    }

    public void SetCurrentCard(Card card)
    {
        this.currentCard = card;
    }

    public Card GetCurrentCard()
    {
        return this.currentCard;
    }
}