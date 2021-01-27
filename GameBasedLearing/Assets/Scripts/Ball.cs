using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : EventTrigger
{

    protected PoolBallHolder currentPoolBallHolder = null;
    protected RectTransform rectTransform = null;
    protected PoolBallHolder targetPoolBallHolder = null;
    private PoolBallHolder[] allPoolBallHolders = new PoolBallHolder[32];
    private Vector3 initialPosition = new Vector3(0f, 0f, 0f);
    private MergeSort mergeSort;
    private ArrayInformation currentArray;
    private ArrayInformation expectedArray;

    private void Start()
    {
       
        initialPosition = this.transform.position;
        mergeSort = GameObject.Find("GameManager").GetComponent<MergeSort>();
        allPoolBallHolders = (PoolBallHolder[])Resources.FindObjectsOfTypeAll(typeof(PoolBallHolder));
        currentArray = currentPoolBallHolder.GetAssociatedArray();
    }
    public void SetCurrentBallHolder(PoolBallHolder poolBallHolder)
    {
        this.currentPoolBallHolder = poolBallHolder;
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        this.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        

    }
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        // Follow pointer
        transform.position += (Vector3)eventData.delta;
        foreach (PoolBallHolder holder in allPoolBallHolders)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(holder.GetRectTransform(), Input.mousePosition) && holder != currentPoolBallHolder)
            {
                // If the mouse is within a valid cell, get it, and break.
                targetPoolBallHolder = holder;
                break;
            }
        }

    }
    private void MoveBall()
    {
        if (mergeSort.CheckMoveIsCorrect(Int32.Parse(this.name.Substring(0,1)),this.targetPoolBallHolder.GetAssociatedArray()))
        {
            this.transform.position = targetPoolBallHolder.transform.position;
            this.currentPoolBallHolder = targetPoolBallHolder;
            currentPoolBallHolder.SetCurrentBall(this);
        }
        else
        {
            transform.position = initialPosition;
            return;
        }
        currentArray = currentPoolBallHolder.GetAssociatedArray();
       
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        this.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);




        // Return to original position
        if (!targetPoolBallHolder)
        {
            transform.position = initialPosition;
            return;
        }

        // Move to new cell
        MoveBall();


    }
}
   


    
