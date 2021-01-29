using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatePoolBalls : MonoBehaviour
{
  
    private List<Tuple<int, List<int>>> arraysToAnimate = new List<Tuple<int, List<int>>>();
    List<ArrayInformation> allArrays = new List<ArrayInformation>();
    List<Ball> balls = new List<Ball>();

    private void Awake()
    {
        allArrays = GameObject.FindObjectsOfType<ArrayInformation>().ToList();
    
    }
    public void AddBallToActive(Ball ball)
    {
        this.balls.Add(ball);
    }
    private IEnumerator Move(bool moving, Ball ball)
    {
        float distanceThisFrame = 100f * 0.02f;
        Vector3 targetPosition = ball.GetCurrentPoolBallHolder().GetDestinationPoolBallHolder().transform.position;
        Vector2 direction = targetPosition - ball.transform.position;
        while (moving)
        {
            ball.transform.Translate(direction.normalized * distanceThisFrame, Space.World);

            if (ball.transform.position.y - targetPosition.y <= 0.1f)
            {
                moving = false;
                ball.SetCurrentBallHolder(ball.GetCurrentPoolBallHolder().GetDestinationPoolBallHolder());
            }
            yield return new WaitForSecondsRealtime(0.01f);

        }
        yield break;
    }
    public void StartAnimation()
    {
        StartCoroutine("FirstAnimation");
    }
    private IEnumerator FirstAnimation()
    {
       
        for (int i = 0; i < 3; i++)
        {

            foreach(Ball ball in balls)
            {
                yield return new WaitForSecondsRealtime(1f);
                StartCoroutine(Move(true, ball));
            }
        }
        yield break;
        
        
    }
    public void SetupExpectedLists(List<Tuple<int, List<int>>> arraysAtUserLevel)
    {
        
        foreach(Tuple<int, List<int>> temp in arraysAtUserLevel)
        {
            ArrayInformation arrayToSet = (allArrays.Find(x => x.GetLevel() == temp.Item1));
            arrayToSet.SetExpectedArrayValues(temp.Item2);
            arraysAtUserLevel.Remove(temp);

        }
    }
    public void SetArraysToAnimate(List<Tuple<int, List<int>>> _arraysToAnimate)
    {
        arraysToAnimate = _arraysToAnimate;
    }
    public void SplittingAnimation()
    {
        for (int userLevel= 1; userLevel < 4; userLevel++)
        {
            List<Tuple<int,List<int>> >arraysAtUserLevel = arraysToAnimate.FindAll(x => x.Item1 == userLevel);
            AnimateToArray(arraysAtUserLevel);
        }
       
    }
    private void AnimateToArray( List<Tuple<int, List<int>>> arraysAtUserLevel)
    {
        SetupExpectedLists(arraysAtUserLevel);
    }
    public void MergingAnimation()
    {
    }
}
