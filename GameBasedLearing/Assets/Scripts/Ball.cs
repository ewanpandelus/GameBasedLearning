///BSD 3 - Clause License

/// Copyright(c) 2021, ewanpandelus
///All rights reserved.

///Redistribution and use in source and binary forms, with or without
///modification, are permitted provided that the following conditions are met:

///1.Redistributions of source code must retain the above copyright notice, this
///list of conditions and the following disclaimer.

///2. Redistributions in binary form must reproduce the above copyright notice,
///this list of conditions and the following disclaimer in the documentation
///and/or other materials provided with the distribution.

///3. Neither the name of the copyright holder nor the names of its
///contributors may be used to endorse or promote products derived from
///this software without specific prior written permission.

///THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
///AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
///IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
///DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
///FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
///DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
///SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
///CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
///OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
///OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

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
    
    /// <summary>
    ///This method icreases the size of the ball while being
    /// dragged and doesn't allow the ball to be dragged while the animations
    /// are occuring.
    /// </summary>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (animatePoolBalls.GetAnimating())
        {
            return;
        }
        base.OnBeginDrag(eventData);
        this.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
    }
   
    /// <summary>This method allows the pool balls to be dragged and 
    /// sets the target holder if hovered over</summary>
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
    
    /// <summary>
    /// This method transfers the position of the ball if the 
    /// move is the correct move. It also sets the current
    /// array of the ball, and if the puzzle is completed it calls a function
    /// from the MergeSort class to let players know they won
    ///</summary>
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

    /// <summary>
    ///This method moves the ball when the 
    /// player releases the mouse.
    ///</summary>
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




