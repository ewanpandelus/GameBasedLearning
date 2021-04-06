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
///OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSort : MonoBehaviour
{
    AudioManager AudioManagement;
    [SerializeField] private Slider slider;
    private DynamicUI dynamicUI;
    private int moveCounter = 0;
    private Vector3 down = new Vector3(0f, -150f);
    private Vector3 up = new Vector3(0f, 150f);
    private GameObject[] allCardHoldersGO;
    int[] cards = new int[9];
    int[] finalArray = new int[9];
    private List<int> numbers = new List<int>();
    private List<Card> allCards = new List<Card>();
    private List<Card> listToCompare = new List<Card>();
    private List<Card> inGameCards = new List<Card>();
    List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
    private GameObject cardObj;
    private CardHolder[] allCardHolders = new CardHolder[9];
    private List<CardHolder> allCardHoldersSorted = new List<CardHolder>();
    private bool moving = false;
    private bool solved = false;
    [SerializeField]
    private Card cardPrefab1, cardPrefab2, cardPrefab3, cardPrefab4, cardPrefab5,
        cardPrefab6, cardPrefab7, cardPrefab8, cardPrefab9, cardPrefab10, cardPrefab11, cardPrefab12, cardPrefab13;
    private GlobalDataHolder globalDataHolder;

    void Start()
    {
        dynamicUI = GameObject.Find("GameManager").GetComponent<DynamicUI>();
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        globalDataHolder.SetBubbleSort(true);
        AudioManagement = AudioManager.instance;
        cardObj = GameObject.Find("Cards");
        numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        allCards = new List<Card> { cardPrefab1, cardPrefab2 , cardPrefab3 , cardPrefab4 , cardPrefab5 , cardPrefab6 , cardPrefab7,
        cardPrefab8,cardPrefab9,cardPrefab10,cardPrefab11,cardPrefab12,cardPrefab13};
        allCardHoldersGO = GameObject.FindGameObjectsWithTag("CardHolder");
        int counter = 0;
        foreach (GameObject GO in allCardHoldersGO)
        {
            allCardHolders[counter] = GO.GetComponent<CardHolder>();
            counter++;
        }
        allCardHoldersSorted = SortCardHolders(allCardHolders);
        Shuffle();
        cards = ConvertToIntArray(inGameCards);
        StoreBubbleSortMoves(cards);
    }

    private List<CardHolder> SortCardHolders(CardHolder[] cardHolders)
    {
        List<CardHolder> sortedCardHolders = new List<CardHolder>();
        int[] cardHoldersNumbers = new int[cardHolders.Length];
        int counter = 0;
        foreach (CardHolder cardHolder in cardHolders)
        {
            int cardNumber = int.Parse(Regex.Match(cardHolder.name, @"\d+").Value);
            cardHoldersNumbers[counter] = cardNumber;
            counter++;
        }
        Array.Sort(cardHoldersNumbers);
        foreach (int number in cardHoldersNumbers)
        {
            sortedCardHolders.Add(GameObject.Find(number.ToString()).GetComponent<CardHolder>());
        }
        return sortedCardHolders;
    }

    public void Replay()
    {
        Reset();
        dynamicUI.ReplayGame();
    }

    /// <summary>
    /// This method provides feedback to player if they win the game
    /// </summary>
    private void CorrectExecution()
    {
        dynamicUI.WinGame();
        dynamicUI.ShowCherryAdd(4);
        AudioManagement.Play("WinGame");
    }

    /// <summary>
    /// This method checks if player has solved the puzzle
    /// </summary
    public void TestIfFinished()
    {
        bool finished = true;
        int counter = 0;
        foreach (CardHolder cardHolder in allCardHoldersSorted)
        {
            finished = finished && int.Parse(Regex.Match(cardHolder.GetCurrentCard().name, @"\d+").Value) == finalArray[counter];
            counter++;
        }
        if (finished)
        {
            CorrectExecution();
        }
    }

    public void Solve()
    {
        if (!solved)
        {
            Reset();
            StartCoroutine(BubbleSortAnimateAlgorithm(ConvertToIntArray(inGameCards)));
        }

    }

    public bool CheckMoveIsCorrect(GameObject leftCard, GameObject rightCard)
    {
        if (moveCounter >= moves.Count)
        {
            return false;
        }
        if (moves[moveCounter].Item1 == int.Parse(Regex.Match(leftCard.name, @"\d+").Value) && moves[moveCounter].Item2 == int.Parse(Regex.Match(rightCard.name, @"\d+").Value)
            || moves[moveCounter].Item1 == int.Parse(Regex.Match(rightCard.name, @"\d+").Value) && moves[moveCounter].Item2 == int.Parse(Regex.Match(leftCard.name, @"\d+").Value))
        {
            moveCounter++;
            DisplaySteps();
            return true;
        }
        dynamicUI.SetWrongPathText();
        return false;
    }

    private void DisplaySteps()
    {
        GameObject.Find("StepCount").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Step Count: " + moveCounter.ToString();
    }

    private void RandomiseCards()
    {
        for (int i = 0; i < 9; i++)
        {
            int random = UnityEngine.Random.Range(1, numbers.Count);
            allCardHolders[i].SetCurrentCard(allCards[random]);
            listToCompare.Add(allCards[random]);
            numbers.RemoveAt(random);
            allCards.RemoveAt(random);
        }
    }

    /// <summary> 
    /// Shuffles cards in scene
    /// </summary>
    public void Shuffle()
    {
        RandomiseCards();
        foreach (CardHolder cardHolder in allCardHoldersSorted)
        {
            Card cardGO = Instantiate(cardHolder.GetCurrentCard(), cardHolder.transform);
            cardHolder.SetCurrentCard(cardGO);
            cardHolder.SetInitialCard(cardGO);
            cardGO.SetCurrentCardHolder(cardHolder);
            inGameCards.Add(cardGO);
            cardGO.transform.SetParent(cardObj.transform);
        }
        AudioManagement.Play("CardShuffle");
    }

    public void Reset()
    {
        if (!solved)
        {
            foreach (CardHolder cardHolder in allCardHoldersSorted)
            {
                cardHolder.GetInitialCard().SetCurrentPosition(cardHolder.transform.position);
                cardHolder.SetCurrentCard(cardHolder.GetInitialCard());
                cardHolder.GetCurrentCard().SetInitialPosition(cardHolder.transform.position);
            }
            moveCounter = 0;
            DisplaySteps();
            AudioManagement.Play("CardShuffle");
        }
    }

    public void StopAnimating()
    {
        solved = false;
    }

    private void AnimateMoveLeftAndRight(GameObject leftCard, GameObject rightCard, Vector3 leftInitialPosition, Vector3 rightInitialPosition)
    {
        moving = true;
        StartCoroutine(ChangeCardSpaceLeftAndRight(leftCard, rightCard, (rightInitialPosition + up), (leftInitialPosition + down)));
        moveCounter++;
        DisplaySteps();
    }

    private void AnimateMoveDownAndUp(GameObject leftCard, GameObject rightCard, Vector3 leftInitialPosition, Vector3 rightInitialPosition)
    {
        moving = true;
        StartCoroutine(ChangeCardSpaceUpAndDown(leftCard, rightCard, (leftInitialPosition), (rightInitialPosition)));
    }

    /// <summary> 
    /// Moves one card up and the adjacent swapping card down
    /// </summary>
    /// <param name="leftCard">left card object</param>
    /// <param name="rightCard">right card object </param>
    /// <param name="leftCardMove">where the left card should be moved to</param>
    /// <param name="rightCardMove">where the right card should be moved to</param>
    IEnumerator ChangeCardSpaceUpAndDown(GameObject leftCard, GameObject rightCard, Vector3 leftCardMove, Vector3 rightCardMove)
    {
        Vector2 leftCardDirection = leftCardMove - leftCard.transform.position;
        Vector2 rightCardDirection = rightCardMove - rightCard.transform.position;
        float distanceThisFrame = slider.value * 0.02f;
        while (moving)
        {
            if (leftCard.transform.position.y - leftCardMove.y <= 0.2f)
            {
                moving = false;
                yield break;
            }
            rightCard.transform.Translate(rightCardDirection.normalized * distanceThisFrame, Space.World);
            leftCard.transform.Translate(leftCardDirection.normalized * distanceThisFrame, Space.World);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    /// <summary> 
    /// Moves one card right and the adjacent swapping card left
    /// </summary>
    /// <param name="leftCard">left card object</param>
    /// <param name="rightCard">right card object </param>
    /// <param name="leftCardMove">where the left card should be moved to</param>
    /// <param name="rightCardMove">where the right card should be moved to</param>
    IEnumerator ChangeCardSpaceLeftAndRight(GameObject leftCard, GameObject rightCard, Vector3 leftCardMove, Vector3 rightCardMove)
    {
        float distanceThisFrame = slider.value * 0.02f;
        Vector2 leftCardDirection = leftCardMove - leftCard.transform.position;
        Vector2 rightCardDirection = rightCardMove - rightCard.transform.position;
        while (moving)
        {
            rightCard.transform.Translate(rightCardDirection.normalized * distanceThisFrame, Space.World);
            leftCard.transform.Translate(leftCardDirection.normalized * distanceThisFrame, Space.World);
            if (rightCard.transform.position.x - rightCardMove.x <= 0.1f)
            {
                moving = false;
                yield break;
            }
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    private int[] ConvertToIntArray(List<Card> cards)
    {
        int[] cardsNumbers = new int[cards.Count];
        int counter = 0;
        foreach (Card card in cards)
        {
            int cardNumber = int.Parse(Regex.Match(card.name, @"\d+").Value);
            cardsNumbers[counter] = cardNumber;
            counter++;
        }
        return cardsNumbers;
    }

    public CardHolder[] GetAllCardHolders()
    {
        return this.allCardHolders;
    }

    public List<Card> AllCards()
    {
        return this.inGameCards;
    }

    /// <summary>
    /// Performs bubble sort algorithm and storesd the moves in a list
    /// </summary>
    /// <param name="arr">integer array of the values on the cards in the scene</param>
    private void StoreBubbleSortMoves(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (arr[j] > arr[j + 1])
                {
                    moves.Add(new Tuple<int, int>(arr[j], arr[j + 1]));
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
        finalArray = arr;
    }

    /// <summary>
    /// Graphically solves bubble sort
    /// </summary>
    /// <param name="arr">integer array of the values on the cards in the scene</param>
    IEnumerator BubbleSortAnimateAlgorithm(int[] arr)
    {
        solved = true;
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (arr[j] > arr[j + 1])
                {
                    GameObject leftCard = GameObject.Find(arr[j].ToString() + "(Clone)");
                    GameObject rightCard = GameObject.Find(arr[j + 1].ToString() + "(Clone)");
                    Vector3 leftInitialPosition = leftCard.transform.position;
                    Vector3 rightInitialPosition = rightCard.transform.position;
                    float difference = Mathf.Abs(rightInitialPosition.x - leftInitialPosition.x);
                    Vector3 right = new Vector3(difference, 0f, 0f);
                    AnimateMoveDownAndUp(leftCard, rightCard, leftInitialPosition + up, rightInitialPosition + down);
                    yield return new WaitUntil(() => moving == false);
                    AnimateMoveLeftAndRight(leftCard, rightCard, leftInitialPosition, rightInitialPosition);
                    yield return new WaitUntil(() => moving == false);
                    yield return new WaitForSecondsRealtime(0.2f);
                    AnimateMoveDownAndUp(leftCard, rightCard, leftInitialPosition + right, rightInitialPosition - right);
                    yield return new WaitUntil(() => moving == false);
                    leftCard.transform.position = new Vector3(leftCard.transform.position.x, leftInitialPosition.y, leftCard.transform.position.z);
                    rightCard.transform.position = new Vector3(rightCard.transform.position.x, leftInitialPosition.y, rightCard.transform.position.z);
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
        CorrectExecution();
        solved = false;
    }

    public bool GetSolved()
    {
        return this.solved;
    }
}