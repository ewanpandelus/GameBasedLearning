using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Card : EventTrigger
{
    protected List<Card> allCards = new List<Card>();
    protected CardHolder[] cardHolders = new CardHolder[9];
    protected CardHolder originalCardHolder = null;
    protected CardHolder currentCardHolder = null;
    protected RectTransform rectTransform = null;
    protected CardHolder targetCardHolder = null;
    BubbleSort bubbleSort;
    private Vector3 initialPosition = new Vector3(0f, 0f, 0f);
    protected Card originalCard = null;
    protected Card currentCard = null;
    protected Card targetCard = null;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        bubbleSort = GameObject.Find("GameManager").GetComponent<BubbleSort>();
        cardHolders = bubbleSort.GetAllCardHolders();
        allCards = bubbleSort.AllCards();
        currentCard = this;
        initialPosition = this.transform.position;
    }

    public void SetCurrentCardHolder(CardHolder cardHolder)
    {
        this.currentCardHolder = cardHolder;
    }

   public void SetInitialPosition(Vector3 position)
    {
        this.initialPosition = position;
    }

    private void MoveCard()
    {
        if (targetCard)
        {
            if(bubbleSort.CheckMoveIsCorrect(this.gameObject, targetCard.gameObject))
            {
                transform.position = targetCard.transform.position;
                currentCardHolder.SetCurrentCard(targetCard);
                targetCardHolder = targetCard.currentCardHolder;
                targetCard.SetCurrentCardHolder(currentCardHolder);
                targetCard.transform.position = initialPosition;
                targetCard.SetInitialPosition(initialPosition);
                currentCard.SetCurrentCardHolder(targetCardHolder);
                targetCardHolder.SetCurrentCard(this);
                initialPosition = transform.position;
                bubbleSort.TestIfFinished();
            }
            else
            {
                transform.position = initialPosition;
                return;
            }
        }
        targetCard = null;
    }
 
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (bubbleSort.GetSolved())
        {
            return;
        }
        base.OnBeginDrag(eventData);
        this.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);

    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (bubbleSort.GetSolved())
        {
            return;
        }
        base.OnDrag(eventData);
        transform.position += (Vector3)eventData.delta;
        foreach (Card card in allCards)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(card.GetRectTransform(), Input.mousePosition)&&card!=currentCard)
            {
                targetCard = card;
                break;
            }
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (bubbleSort.GetSolved())
        {
            return;
        }
        base.OnEndDrag(eventData);
        this.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
        if (!targetCard && currentCard)
        {
            transform.position = initialPosition;
            return;
        }
        MoveCard();
    }

    public void SetCurrentPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    public RectTransform GetRectTransform()
    {
        return this.rectTransform;
    }
}
