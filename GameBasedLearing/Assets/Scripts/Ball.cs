using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ball : EventTrigger
{

    protected PoolBallHolder currentPoolBallHolder = null;
    protected RectTransform rectTransform = null;
    protected PoolBallHolder targetPoolBallHolder = null;
    private PoolBallHolder[] allPoolBallHolders = new PoolBallHolder[32];
    private Vector3 currentPosition = new Vector3(0f, 0f, 0f);
    private MergeSort mergeSort;
    private ArrayInformation currentArray;
    private ArrayInformation expectedArray;
    private bool belongsToArray = false;
    private bool finishedFirstHalf = false;
    private AnimatePoolBalls animatePoolBalls;


    private void Start()
    {
        currentPosition = this.transform.position;
        mergeSort = GameObject.Find("GameManager").GetComponent<MergeSort>();
        animatePoolBalls = GameObject.Find("GameManager").GetComponent<AnimatePoolBalls>();
        allPoolBallHolders = (PoolBallHolder[])Resources.FindObjectsOfTypeAll(typeof(PoolBallHolder));
        currentArray = currentPoolBallHolder.GetAssociatedArray();
    }
    public void SetCurrentBallHolder(PoolBallHolder poolBallHolder)
    {
        this.currentPoolBallHolder = poolBallHolder;
    }
    public PoolBallHolder GetCurrentPoolBallHolder()
    {
        return this.currentPoolBallHolder;
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (animatePoolBalls.GetAnimating())
        {
            return;
        }
        base.OnBeginDrag(eventData);
        this.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (animatePoolBalls.GetAnimating())
        {
            return;
        }
        base.OnDrag(eventData);
        transform.position += (Vector3)eventData.delta;
        foreach (PoolBallHolder holder in allPoolBallHolders)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(holder.GetRectTransform(), Input.mousePosition) && holder != currentPoolBallHolder)
            {
                targetPoolBallHolder = holder;
                break;
            }
        }

    }
    private void MoveBall()
    {

        if (mergeSort.CheckMoveIsCorrect(Int32.Parse(this.name.Substring(0, 1)), this.targetPoolBallHolder.GetAssociatedArray(), this.currentPoolBallHolder.GetAssociatedArray(), belongsToArray, targetPoolBallHolder))
        {
            this.transform.position = targetPoolBallHolder.transform.position;
            currentPosition = targetPoolBallHolder.transform.position;
            this.currentPoolBallHolder = targetPoolBallHolder;
            currentPoolBallHolder.SetCurrentBall(this);
            if (targetPoolBallHolder.GetAssociatedArray().CheckFinished())
            {
                mergeSort.CorrectExecution();
            }
        }
        else
        {
            transform.position = currentPosition;
            return;
        }
        currentArray = currentPoolBallHolder.GetAssociatedArray();

    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (animatePoolBalls.GetAnimating())
        {
            return;
        }
        base.OnEndDrag(eventData);
        this.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
        if (!targetPoolBallHolder)
        {
            transform.position = currentPosition;
            return;
        }
        MoveBall();
    }
    public Outline GetOutline()
    {
        return this.transform.GetChild(0).GetComponent<Outline>();
    }
    public void SetBelongsToArray(bool val)
    {
        belongsToArray = val;
    }
    public void SetFirstHalf(bool val)
    {
        this.finishedFirstHalf = val;
    }
    public bool GetFirstHalf()
    {
        return this.finishedFirstHalf;
    }
}




