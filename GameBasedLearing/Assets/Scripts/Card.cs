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
    private int value;
    private bool movedVertically = false;
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

    // Update is called once per frame
    void Update()
    {

    }
    public void SetCurrentCardHolder(CardHolder cardHolder)
    {
        this.currentCardHolder = cardHolder;
    }
    protected virtual void Move()
    {
      
        originalCardHolder = currentCardHolder;
     
        // Switch cells
        if (targetCardHolder)
        {
            int i = int.Parse(targetCardHolder.name);
            int j = int.Parse(currentCardHolder.name);

            Vector3 tempPosition = currentCardHolder.transform.position;
            targetCardHolder.GetCurrentCard().transform.position = tempPosition;
            currentCardHolder = targetCardHolder;
            transform.position = currentCardHolder.transform.position;
            targetCardHolder = originalCardHolder;
            SwapCardHolder(i, j);
            Debug.Log(transform.position);
        }

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
                targetCard.transform.position = initialPosition;
                targetCard.SetInitialPosition(initialPosition);
                initialPosition = transform.position;
            }
            else
            {
                transform.position = initialPosition;
                return;
            }
           


        }
        targetCard = null;
    }
    private void SwapCardHolder(int i, int j)
    {
        Card tmp = allCards[i];
        allCards[i] = allCards[j];
        allCards[j] = tmp;
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        this.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);

    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        // Follow pointer
        transform.position += (Vector3)eventData.delta;
        foreach (Card card in allCards)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(card.GetRectTransform(), Input.mousePosition)&&card!=currentCard)
            {
                // If the mouse is within a valid cell, get it, and break.
                targetCard = card;
                break;
            }
        }
      
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        this.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);



        // Return to original position
        if (!targetCard && currentCard)
        {
            transform.position = initialPosition;
            return;
        }

        // Move to new cell
        MoveCard();


    }
    public void  SetCurrentPosition(Vector3 position)
    {
        this.transform.position = position;
    }
    public RectTransform GetRectTransform()
    {
        return this.rectTransform;
    }
}
