using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    private Card currentCard = null;
    private Card initialCard = null;
    private RectTransform rectTransform = null;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public RectTransform GetRectTransform()
    {
        return this.rectTransform;
    }
}
