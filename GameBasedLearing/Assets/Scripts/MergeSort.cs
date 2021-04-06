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
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class MergeSort : MonoBehaviour
{
    private bool solved = false;
    [SerializeField] ArrayInformation initialArray;
    [SerializeField]
    Ball ballPrefab1, ballPrefab2, ballPrefab3, ballPrefab4,
        ballPrefab5, ballPrefab6, ballPrefab7, ballPrefab8;
    [SerializeField] GameObject topArray;
    private DynamicUI dynamicUI;
    private List<Ball> allBalls = new List<Ball>();
    private List<int> numbers = new List<int>();
    private PoolBallHolder[] allPoolBallHolders = new PoolBallHolder[8];
    private List<GameObject> allPoolBallHoldersGO = new List<GameObject>();
    private List<Ball> inGameBalls = new List<Ball>();
    private GameObject aboveAll;
    private GameObject ballObj;
    List<int> balls = new List<int>();
    private int userLevel = 1;
    private List<Tuple<int, List<int>>> expectedArrays = new List<Tuple<int, List<int>>>();
    Dictionary<List<int>, bool> checkExpectedListFull = new Dictionary<List<int>, bool>();
    List<ArrayInformation> allArrays = new List<ArrayInformation>();
    private bool merging = false;
    private List<Tuple<int, List<int>>> sortedArrays = new List<Tuple<int, List<int>>>();
    private AnimatePoolBalls animatePoolBalls;
    private AudioManager audioManager;
    private GlobalDataHolder globalDataHolder;


    void Start()
    {
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        globalDataHolder.SetMergeSort(true);
        dynamicUI = GameObject.Find("GameManager").GetComponent<DynamicUI>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Bar");
        animatePoolBalls = gameObject.GetComponent<AnimatePoolBalls>();
        allArrays = GameObject.FindObjectsOfType<ArrayInformation>().ToList();
        aboveAll = GameObject.Find("AboveAll");
        ballObj = GameObject.Find("TopArray1");
        for(int i = 0; i < topArray.transform.childCount; i++)
        {
            allPoolBallHoldersGO.Add(topArray.transform.GetChild(i).gameObject);
        }
        int counter = 0;
        foreach (GameObject GO in allPoolBallHoldersGO)
        {
            allPoolBallHolders[counter] = GO.GetComponent<PoolBallHolder>();
            counter++;
        }
        InitialiseGame();
    }

    /// <summary>
    /// This method resets the game
    /// </summary>
    public void Reset()
    {
        if (!animatePoolBalls.GetAnimating())
        {
            animatePoolBalls.ClearAnimationObjects();
            foreach (ArrayInformation array in allArrays)
            {
                array.SetEmpty(true);
                array.SetFull(false);
                array.SetAllOccupiedFalse();
            }
            foreach (Ball ball in inGameBalls)
            {
                GameObject ballGO = ball.gameObject;
                Destroy(ballGO);
            }
            userLevel = 1;
            inGameBalls.Clear();
            InitialiseGame();
        }
    }

    private void InitialiseGame()
    {
        expectedArrays.Clear();
        numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
        allBalls = new List<Ball> { ballPrefab1, ballPrefab2, ballPrefab3, ballPrefab4,
            ballPrefab5, ballPrefab6, ballPrefab7, ballPrefab8};
        Shuffle();
        balls = ConvertToIntArray(inGameBalls);
        expectedArrays.Add(new Tuple<int, List<int>>(0, balls));
        initialArray.SetExpectedArrayValues(balls);
        CreateExpectedArrays(balls, 1);
        foreach (Tuple<int, List<int>> expectedArray in expectedArrays)
        {
            checkExpectedListFull[expectedArray.Item2] = false;
        }
        merging = false;
    }

    /// <summary>
    /// This method graphically solves the puzzle/algorithm
    /// </summary>
    public void Solve()
    {
        if (!solved)
        {
            Reset();
            InitiateSolveAnimation();
        }
        else
        {
            if (!animatePoolBalls.GetAnimating())
            {
                Reset();
                InitiateSolveAnimation();
            }
        }
    }

    private void InitiateSolveAnimation()
    {
        animatePoolBalls.StartAnimation();
        foreach (ArrayInformation array in allArrays)
        {
            array.SetEmpty(true);
            array.SetFull(false);
        }
    }
   
    public void Replay()
    {
        Reset();
        dynamicUI.ReplayGame();
    }

    /// <summary>
    /// This method tells the player when they have won the game
    /// and adds cherries to the count 
    /// </summary>
    public void CorrectExecution()
    {
        solved = true;
        dynamicUI.WinGame();
        dynamicUI.ShowCherryAdd(6);
        audioManager.Play("WinGame");
    }
    /// <summary>
    /// This method shuffles the pool balls
    /// in the scene so they are unsorted
    /// </summary>
    public void Shuffle()
    {
        audioManager.Play("Pool");
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
            animatePoolBalls.AddBallToActive(ballGo);
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

    /// <summary>
    /// Checks if move is correct at the splitting stage
    /// </summary>
    /// <param name="ballNumber">the number of the ball in question</param>
    /// <param name="associatedArray">the array the ball is being moved to.</param>
    /// <param name="previousArray">the array the ball is being moved from.</param>
    /// <param name="belongsToArray">represents if the ball belongs to the associated array</param>
    /// <param name="poolBallHolder">the pool ball holder the ball is being moved to</param>
    /// <returns> true if the move was correct, false otherwise </returns>
    private bool Splitting(int ballNumber, ArrayInformation associatedArray, ArrayInformation previousArray, bool belongsToArray)
    {
        List<Tuple<int, List<int>>> ArraysForLevel;
        int prevBallIndex = FindPreviousBallPosition(ballNumber);
        ArraysForLevel = expectedArrays.FindAll(x => x.Item1 == userLevel);
        List<int> currrentArray = expectedArrays.Find(x => x.Item2.Contains(ballNumber) && x.Item1 == userLevel).Item2;
        if (associatedArray == previousArray)
        {
            return false;
        }
        if (associatedArray.GetLevel() == userLevel)
        {
            if (associatedArray.GetEmpty() && !belongsToArray)
            {

                associatedArray.SetExpectedArrayValues(currrentArray);
                ManipulateArrays(associatedArray, previousArray, ballNumber, prevBallIndex);
                if (userLevel == 3)
                {
                    CheckIfUserLevelShouldChange();
                }
                return true;
            }
            if (!associatedArray.GetEmpty() && associatedArray.GetExpectedArrayValues() == currrentArray)
            {
                ManipulateArrays(associatedArray, previousArray, ballNumber, prevBallIndex);
                CheckIfUserLevelShouldChange();
                return true;
            }
            else
            {
                dynamicUI.ChangeWrongPathText("The ball doesn't belong to this array",1.5f);
                return false;
            }
        }
        dynamicUI.ChangeWrongPathText("The ball doesn't belong to this array",1.5f);
        return false;
    }

    /// <summary>
    /// Checks if move is correct at the merging stage
    /// </summary>
    /// <param name="ballNumber">the number of the ball in question</param>
    /// <param name="associatedArray">the array the ball is being moved to.</param>
    /// <param name="previousArray">the array the ball is being moved from.</param>
    /// <param name="belongsToArray">represents if the ball belongs to the associated array</param>
    /// <param name="poolBallHolder">the pool ball holder the ball is being moved to</param>
    /// <returns> true if the move was correct, false otherwise </returns>
    public bool Merging(int ballNumber, ArrayInformation associatedArray, ArrayInformation previousArray, bool belongsToArray, PoolBallHolder poolBallHolder)
    {
        List<Tuple<int, List<int>>> arraysForLevel;
        int prevBallIndex = FindPreviousBallPosition(ballNumber);
        arraysForLevel = sortedArrays.FindAll(x => x.Item1 == userLevel-1);
        List<int> currrentArray = arraysForLevel.Find(x => x.Item2.Contains(ballNumber)).Item2;
       
        if (associatedArray.GetLevel() == userLevel-1)
        {
            if (associatedArray.GetEmpty() && !belongsToArray)
            {
                List<int> ascendingIndices = CreateAscendingIndices(associatedArray.GetSize());
                associatedArray.SetExpectedArrayValues(currrentArray);
                ManipulateArrays(associatedArray, previousArray, ballNumber, prevBallIndex);

                return CheckCorrectBallPosition(associatedArray, ballNumber, poolBallHolder);
            }
            if (!associatedArray.GetEmpty() && associatedArray.GetExpectedArrayValues() == currrentArray&&CheckCorrectBallPosition(associatedArray,ballNumber,poolBallHolder)==true)
            {
                ManipulateArrays(associatedArray, previousArray, ballNumber, prevBallIndex);
                CheckIfUserLevelShouldChange();
                return true;
            }
            else
            {
                dynamicUI.ChangeWrongPathText("When merging, the arrays become sorted",3f);
                return false;
            }
        }
        dynamicUI.ChangeWrongPathText("The ball doesn't belong to this array",1.5f);
        return false;
    }

    private bool CheckCorrectBallPosition(ArrayInformation associatedArray, int ballNumber,PoolBallHolder poolBallHolder)
    {
        return (associatedArray.GetExpectedArrayValues().IndexOf(ballNumber) == poolBallHolder.GetIndex());
    }

    private List<int> CreateAscendingIndices(int listLen)
    {
        List<int> ascendingIndices = new List<int>();
        for(int i = 0; i < listLen; i++)
        {
            ascendingIndices.Add(i);
        }
        return ascendingIndices;
    }
    
    public bool CheckMoveIsCorrect(int ballNumber, ArrayInformation associatedArray,ArrayInformation previousArray,bool belongsToArray,PoolBallHolder poolBallHolder)
    {
        if (!merging)
        {
            return Splitting(ballNumber, associatedArray, previousArray, belongsToArray);
        }
        return Merging(ballNumber, associatedArray, previousArray, belongsToArray,poolBallHolder);
    }

    /// <summary>
    /// Finds the position where the ball used to be
    /// </summary>
    /// <param name="ballNumber">the integer of the array element</param>
    /// <returns> index of previous ball position</returns>
    private int FindPreviousBallPosition(int ballNumber)
    {
        if (!merging)
        {
            return expectedArrays.Find(x => x.Item2.Contains(ballNumber) && x.Item1 == userLevel - 1).Item2.IndexOf(ballNumber);
        }
        return expectedArrays.Find(x => x.Item2.Contains(ballNumber) && x.Item1 == userLevel).Item2.IndexOf(ballNumber);
    }

    /// <summary>
    /// Changes array level if all balls have been 
    /// placed into the arrays at the current level
    /// </summary>
    private void CheckIfUserLevelShouldChange()
    {
        bool change = true;
        List<ArrayInformation> arraysToCheck = new List<ArrayInformation>();
        if (!merging)
        {
            arraysToCheck = allArrays.FindAll(x => x.GetLevel() == userLevel);
        }
        else
        {
            arraysToCheck = allArrays.FindAll(x => x.GetLevel() == userLevel-1);
        }
        foreach(ArrayInformation array in arraysToCheck)
        {
            change = change && array.GetFull();
            if (!change)
            {
                return;
            }
        }
        if (change)
        {
       
           foreach(ArrayInformation array in arraysToCheck)
            {
                array.SetBallsOfExpectedArray(false);
            }
            if (!merging)
            {
                userLevel++;
                if (userLevel == 4)
                {
                    SetupMerging();
                }
            }
            else
            {
                userLevel--;
            }
        }
    }

    /// <summary>
    /// This method sets up the expected pool balls
    /// for each array in the merging step
    /// </summary>
    private void SetupMerging()
    {
        userLevel = 3;
        merging = true;
        List<Tuple<int, List<int>>> arraysBelowLevel3 = expectedArrays.FindAll(x => x.Item1 <3);
        foreach (Tuple<int, List<int>> array in arraysBelowLevel3)
        {
            array.Item2.Sort();
            sortedArrays.Add(array);
        }
        foreach(ArrayInformation array in allArrays)
        {
            array.SetFull(false);
        }
    }
    /// <summary>
    /// This method updates the array the ball is moved to 
    /// with true at the index.
    /// </summary>
    private void ManipulateArrays(ArrayInformation associatedArray, ArrayInformation previousArray, int ballNumber,int prevBallIndex)
    {
        associatedArray.UpdateIsArrayOccupied(ballNumber, true,prevBallIndex);
        previousArray.UpdateIsArrayOccupied(ballNumber, false,prevBallIndex);
        checkExpectedListFull[associatedArray.GetExpectedArrayValues()] = associatedArray.GetFull(); ;
        checkExpectedListFull[previousArray.GetExpectedArrayValues()] = previousArray.GetFull();
    }

    /// <summary>
    /// This method sets up the expected pool balls
    /// for each array in the splitting step
    /// </summary>
    /// /// <param name="ballsNumbers">list of ints of array ekements</param>
    /// /// <param name="level">the level of array the user is currently at</param>
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

    public void SetSolved(bool _solved)
    {
        this.solved = _solved;
    }
}
