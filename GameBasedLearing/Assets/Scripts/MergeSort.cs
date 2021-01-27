using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class MergeSort : MonoBehaviour
{
    [SerializeField] ArrayInformation initialArray;
    [SerializeField]
    Ball ballPrefab1, ballPrefab2, ballPrefab3, ballPrefab4,
        ballPrefab5, ballPrefab6, ballPrefab7, ballPrefab8;
    private List<Ball> allBalls = new List<Ball>();
    private List<int> numbers = new List<int>();
    private PoolBallHolder[] allPoolBallHolders = new PoolBallHolder[8];
    private GameObject[] allPoolBallHoldersGO;
    private List<Ball> inGameBalls = new List<Ball>();
    private GameObject aboveAll;
    GameObject ballObj;
    List<int> balls = new List<int>();
    private int userLevel = 1;
    private List<Tuple<int, List<int>>> expectedArrays = new List<Tuple<int, List<int>>>();
    Dictionary<List<int>, bool> checkExpectedListFull = new Dictionary<List<int>, bool>();
    List<ArrayInformation> allArrays = new List<ArrayInformation>();
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  

    void Start()
    {
        allArrays = GameObject.FindObjectsOfType<ArrayInformation>().ToList();
        aboveAll = GameObject.Find("AboveAll");
        ballObj = GameObject.Find("TopArray");
        allBalls = new List<Ball> { ballPrefab1, ballPrefab2, ballPrefab3, ballPrefab4,
            ballPrefab5, ballPrefab6, ballPrefab7, ballPrefab8};
        numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
        allPoolBallHoldersGO = GameObject.FindGameObjectsWithTag("TopArrayHolder");
        int counter = 0;
        foreach (GameObject GO in allPoolBallHoldersGO)
        {
            allPoolBallHolders[counter] = GO.GetComponent<PoolBallHolder>();
            counter++;
        }
        Shuffle();
        balls = ConvertToIntArray(inGameBalls);
        expectedArrays.Add(new Tuple<int, List<int>>(0, balls));
        initialArray.SetExpectedArrayValues(balls);
        CreateExpectedArrays(balls, 1);
        foreach(Tuple<int, List<int>> expectedArray in expectedArrays)
        {
            checkExpectedListFull[expectedArray.Item2] = false;
        }
        
    }
    public void Shuffle()
    {
        RandomiseBalls();

        foreach (PoolBallHolder poolHolder in allPoolBallHolders)
        {
            Ball ballGo = Instantiate(poolHolder.GetCurrentBall(), poolHolder.transform);
            poolHolder.SetCurrentBall(ballGo);
            poolHolder.SetInitialBall(ballGo);
            ballGo.SetCurrentBallHolder(poolHolder);
            inGameBalls.Add(ballGo);
            ballGo.transform.SetParent(aboveAll.transform);
            ballGo.transform.position = poolHolder.transform.position;

        }
    }
    public void RandomiseBalls()
    {
        for (int i = 0; i < 8; i++)
        {
            int random = UnityEngine.Random.Range(1, numbers.Count + 1);
            allPoolBallHolders[i].SetCurrentBall(allBalls[random - 1]);
            numbers.RemoveAt(random - 1);
            allBalls.RemoveAt(random - 1);
        }
    }
    public bool CheckMoveIsCorrect(int ballNumber, ArrayInformation associatedArray,ArrayInformation previousArray,bool belongsToArray)

    {
        List<Tuple<int, List<int>>> ArraysForLevel;
        int prevBallIndex = FindPreviousBallPosition(ballNumber);
        Debug.Log(prevBallIndex);
        ArraysForLevel = expectedArrays.FindAll(x => x.Item1 == userLevel);
        List<int> currrentArray = expectedArrays.Find(x => x.Item2.Contains(ballNumber)&&x.Item1 ==userLevel ).Item2;
        if (associatedArray.GetLevel() == userLevel)
        {
            if (associatedArray.GetEmpty()&&!belongsToArray)
            {

                associatedArray.SetExpectedArrayValues(currrentArray);
                ManipulateArrays(associatedArray, previousArray, ballNumber,prevBallIndex);
                return true;
            }
            if(!associatedArray.GetEmpty() && associatedArray.GetExpectedArrayValues() == currrentArray)
            {
                ManipulateArrays(associatedArray, previousArray, ballNumber,prevBallIndex);
                CheckIfUserLevelShouldIncrement();
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    private int FindPreviousBallPosition(int ballNumber)

    {
        List<Tuple<int, List<int>>> Arrays1= expectedArrays.FindAll(x => x.Item2.Contains(ballNumber));
        List<Tuple<int, List<int>>> Arrays2 = expectedArrays.FindAll(x=> x.Item1 == userLevel -1);

        return expectedArrays.Find(x => x.Item2.Contains(ballNumber) && x.Item1 == userLevel - 1).Item2.IndexOf(ballNumber) ;
    }
    private void CheckIfUserLevelShouldIncrement()
    {
        bool increment = true;
        List<ArrayInformation> arraysToCheck = new List<ArrayInformation>();
        arraysToCheck = allArrays.FindAll(x => x.GetLevel() == userLevel);
        foreach(ArrayInformation array in arraysToCheck)
        {
            increment = increment && array.GetFull();
            if (!increment)
            {
                return;
            }
        }
        if (increment)
        {
           foreach(ArrayInformation array in arraysToCheck)
            {
                array.SetBallsOfExpectedArray(false);
            }
           userLevel++;
        }
       
    }
    private void ManipulateArrays(ArrayInformation associatedArray, ArrayInformation previousArray, int ballNumber,int prevBallIndex)
    {
        associatedArray.UpdateIsArrayOccupied(ballNumber, true,prevBallIndex);
        previousArray.UpdateIsArrayOccupied(ballNumber, false,prevBallIndex);
        checkExpectedListFull[associatedArray.GetExpectedArrayValues()] = associatedArray.GetFull(); ;
        checkExpectedListFull[previousArray.GetExpectedArrayValues()] = previousArray.GetFull();

    }   
    private void CreateExpectedArrays(List<int> ballsNumbers, int level)
    {

        if (ballsNumbers.Count == 1)
        {
            return;
        }
        else
        {
            List<int> L = new List<int>();
            List<int> R = new List<int>();
            L = ballsNumbers.GetRange(0, ballsNumbers.Count / 2);
            R = ballsNumbers.GetRange((ballsNumbers.Count / 2), ballsNumbers.Count/2);
            expectedArrays.Add(new Tuple<int, List<int>>(level,R));
            expectedArrays.Add(new Tuple<int, List<int>>(level, L));
            CreateExpectedArrays(L, level + 1);
            CreateExpectedArrays(R, level + 1);

        }
    }
    private List<int> ConvertToIntArray(List<Ball> balls)
    {
        List<int> ballsNumbers = new List<int>();
        int counter = 0;
        foreach (Ball ball in balls)
        {
            int cardNumber = int.Parse(Regex.Match(ball.name, @"\d+").Value);
            ballsNumbers.Add(cardNumber);
            counter++;
        }
        return ballsNumbers;

    }
 
}
