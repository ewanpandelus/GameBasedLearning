using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatePoolBalls : MonoBehaviour
{
    private bool moved = false;
    bool firstHalf = false;
    private List<Tuple<int, List<int>>> arraysToAnimate = new List<Tuple<int, List<int>>>();
    List<ArrayInformation> allArrays = new List<ArrayInformation>();
    List<Ball> balls = new List<Ball>();
    List<Tuple<Ball, Tuple<Ball, PoolBallHolder>>> moveList = new List<Tuple<Ball, Tuple<Ball, PoolBallHolder>>>();
    private void Awake()
    {
        allArrays = GameObject.FindObjectsOfType<ArrayInformation>().ToList();

    }
    public void AddBallToActive(Ball ball)
    {
        this.balls.Add(ball);
    }
    private IEnumerator MoveFowards(bool moving, Ball ball,Vector3 targetPosition)
    {
        float distanceThisFrame = 500f * 0.02f;
       
        Vector2 direction = targetPosition - ball.transform.position;
        while (moving)
        {
            ball.transform.Translate(direction.normalized * distanceThisFrame, Space.World);

            if (ball.transform.position.y - targetPosition.y <= 0.1f)
            {
               
                ball.SetFirstHalf(true);
                ball.SetCurrentBallHolder(ball.GetCurrentPoolBallHolder().GetDestinationPoolBallHolder());
                ball.GetCurrentPoolBallHolder().SetCurrentBall(ball);
                moving = false;
            }
            yield return new WaitForSecondsRealtime(0.01f);

        }
    
        yield break;
    }
    private void HighLightBalls(Ball ball1, Ball ball2)
    {
        ball1.GetOutline().SetOutlineImage(true);
        ball2.GetOutline().SetOutlineImage(true);
    }
    private IEnumerator MoveBackwards(bool moving, Ball ball, Vector3 targetPosition,PoolBallHolder desinationPoolBallHolder)
    {
   
        float distanceThisFrame = 350f* 0.02f;
        
        Vector2 direction = targetPosition - ball.transform.position;
        while (moving)
        {
           
            ball.transform.Translate(direction.normalized * distanceThisFrame, Space.World);

            if (targetPosition.y - ball.transform.position.y <= 0.1f)
            {
                ball.GetOutline().SetOutlineImage(false);
              
                
              

                moving = false;

            }
            yield return new WaitForSecondsRealtime(0.01f);

        }
        yield break;
    }
    public void StartAnimation()
    {
        StartCoroutine("FirstAnimation");
      
        StartCoroutine("IterateThroughArrays");
    }
    private IEnumerator FirstAnimation()
    {

        for (int i = 0; i < 3; i++)
        {

            foreach (Ball ball in balls)
            {
                yield return new WaitForSecondsRealtime(0.5f);
                StartCoroutine(MoveFowards(true, ball, ball.GetCurrentPoolBallHolder().GetDestinationPoolBallHolder().transform.position));
            }
        }
        yield return new WaitForSecondsRealtime(1f);
        firstHalf = true;
        yield break;


    }
    private IEnumerator MergingAnimation()
    {
        foreach(Tuple<Ball, Tuple<Ball, PoolBallHolder>> move in moveList)
        {

           
            yield return new WaitForSeconds(1.5f);
            if (move.Item1)
            {
                HighLightBalls(move.Item1, move.Item2.Item1);
            }
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(MoveBackwards(true, move.Item2.Item1, move.Item2.Item2.transform.position, move.Item2.Item2));


        }
       
    }

    public void SetArraysToAnimate(List<Tuple<int, List<int>>> _arraysToAnimate)
    {
        arraysToAnimate = _arraysToAnimate;
    }

    public IEnumerator  IterateThroughArrays()
    {
        
        yield return new WaitUntil(() => firstHalf == true);
       
        for (int i = 3; i > 0; i--)
            
        {
            List<ArrayInformation> arraysToMoveTo = allArrays.FindAll(x => x.GetLevel() == i-1);
            arraysToMoveTo.Reverse();
            List<Ball> leftPoolBalls = new List<Ball>();
            List<Ball> rightPoolBalls = new List<Ball>();
            foreach (ArrayInformation array in arraysToMoveTo)
            {
        
                List<ArrayInformation> mergingArrays = array.GetAssociatedMergingArrays();
                List<PoolBallHolder> LeftPoolBallHolders = mergingArrays[0].GetPoolBallHolders();
                List<PoolBallHolder> rightPoolBallHolders = mergingArrays[1].GetPoolBallHolders();

                foreach (PoolBallHolder poolBallHolder in LeftPoolBallHolders)
                {
                    leftPoolBalls.Add(poolBallHolder.GetCurrentBall());
                }
                foreach (PoolBallHolder poolBallHolder in rightPoolBallHolders)
                {
                    rightPoolBalls.Add(poolBallHolder.GetCurrentBall());
                }
                CompareLeftAndRightLists(leftPoolBalls, rightPoolBalls,array);
                
            }
          
        }
        StartCoroutine("MergingAnimation");
    }
    private void SetPoolBallHolder(PoolBallHolder destinationPoolBallHolder,Ball ball)
    {
        ball.SetCurrentBallHolder(destinationPoolBallHolder);
        ball.GetCurrentPoolBallHolder().SetCurrentBall(ball);
    }
    public void CompareLeftAndRightLists(List<Ball> leftBalls, List<Ball> rightBalls, ArrayInformation arrayToMoveTo)
    {
        Debug.Log("Array to Move to:" + arrayToMoveTo);

        int leftI = 0;
        int rightI = 0;
        List<Tuple<int, PoolBallHolder>> poolBallHolderIndexes = new List<Tuple<int, PoolBallHolder>>();
        List<PoolBallHolder> poolBallHolders = new List<PoolBallHolder>();
        foreach (PoolBallHolder poolballHolder in arrayToMoveTo.GetPoolBallHolders())
        {
            poolBallHolderIndexes.Add(new Tuple<int, PoolBallHolder>(poolballHolder.GetIndex(), poolballHolder));
        }
        for (int i = 0; i <(poolBallHolderIndexes.Count); i++)
        {
        

            PoolBallHolder moveToPoolBallHolder = poolBallHolderIndexes.Find(x => x.Item1 == i).Item2;
            Debug.Log("BallHolder to Move to" + moveToPoolBallHolder);
            if (rightBalls.Count ==rightI)
            {
                moveList.Add(new Tuple<Ball, Tuple<Ball, PoolBallHolder>>(null, new Tuple<Ball, PoolBallHolder>(leftBalls[leftI], moveToPoolBallHolder)));
                SetPoolBallHolder(moveToPoolBallHolder, leftBalls[leftI]);
                leftI++;

                continue;
            }
            if (leftBalls.Count == leftI)
            {
                moveList.Add(new Tuple<Ball, Tuple<Ball, PoolBallHolder>>(null, new Tuple<Ball, PoolBallHolder>(rightBalls[rightI], moveToPoolBallHolder)));
                SetPoolBallHolder(moveToPoolBallHolder, rightBalls[rightI]);
                rightI++;
        
                continue;
            }
            if (CompareElements(leftBalls[leftI], rightBalls[rightI]))
            {
                moveList.Add(new Tuple<Ball, Tuple<Ball, PoolBallHolder>>(rightBalls[rightI], new Tuple<Ball, PoolBallHolder>(leftBalls[leftI], moveToPoolBallHolder)));
                SetPoolBallHolder(moveToPoolBallHolder, leftBalls[leftI]);
                leftI++;
            }
            else
            {
                moveList.Add(new Tuple<Ball, Tuple<Ball, PoolBallHolder>>(leftBalls[leftI], new Tuple<Ball, PoolBallHolder>(rightBalls[rightI], moveToPoolBallHolder)));
                SetPoolBallHolder(moveToPoolBallHolder, rightBalls[rightI]);
                rightI++;
            }
        
        }
        rightBalls.Clear();
        leftBalls.Clear();
    }
    private bool CompareElements(Ball left,Ball right)
    {
        return Int32.Parse(left.name.Substring(0, 1)) < Int32.Parse(right.name.Substring(0, 1));
    }
}
