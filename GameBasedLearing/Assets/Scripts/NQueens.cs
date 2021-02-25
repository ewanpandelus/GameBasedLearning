using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NQueens : MonoBehaviour, IPuzzle
{
    [SerializeField] private ChessBoard board;
    [SerializeField] private Slider slider;
    GameObject[] queens;
    [SerializeField] private GameObject queenPiecePrefab;

    private bool safe = true;
    private GameObject parentCanvas;
    private int problemSize;
    private bool solved = false;
    private List<Tuple<Tuple<int, int>, char>> moves = new List<Tuple<Tuple<int, int>, char>>();
    [SerializeField] private DynamicUI dynamicUI;
    AudioManager AudioManagement;
    private Cell randomCell;
    void Start()
    {
        AudioManagement = AudioManager.instance;
        parentCanvas = GameObject.Find("ParentCanvas");
        board.Create();
        problemSize = board.GetProblemSize();
        randomCell = GameObject.Find("Cell(Clone)").GetComponent<Cell>();
    }
    public bool CheckMoveIsPossible(GameObject GO)
    {
        throw new System.NotImplementedException();
    }

    void IPuzzle.ComputerSolve()
    {
        solved = true;
        ClearBoard();
        moves.Clear();
        int[,] board = new int[problemSize, problemSize];
        GameObject[,] queenPlacement = new GameObject[problemSize, problemSize];
        solveNQUtil(board, 0, problemSize);
        StartCoroutine(IterateThroughMoves(queenPlacement));
  
    }

     void IPuzzle.DisplaySteps()
    {
        throw new System.NotImplementedException();
    }

     void IPuzzle.MakeMove(GameObject playedObject)
    {
        throw new System.NotImplementedException();
    }

    public void TrySolution()
    {
        ((IPuzzle)this).TrySolution();
    }
    void IPuzzle.TrySolution()
    {
        queens = GameObject.FindGameObjectsWithTag("Queen");
        
        foreach (GameObject queen in queens)
        {
            safe = safe && queen.GetComponent<QueenPiece>().GetSafe();

        }
        if (safe && queens.Length == board.GetProblemSize())
        {
            dynamicUI.SetWinningPathText();
            dynamicUI.SetButtonsActive();
            AudioManagement.Play("WinGame");
        }
        else
        {
            dynamicUI.SetWrongPathText();
        }
    }


    public void ClearBoard()
    {
        queens = GameObject.FindGameObjectsWithTag("Queen");
        board.ClearCells();


        foreach (GameObject queen in queens)
        {
            Destroy(queen);
        }
    }
    bool isSafe(int[,] board, int row, int col, int n)
    {
        int i, j;

        /* Check this row on left side */
        for (i = 0; i < col; i++)
            if (board[row, i] == 1)
                return false;

        /* Check upper diagonal on left side */
        for (i = row, j = col; i >= 0 &&
             j >= 0; i--, j--)
            if (board[i, j] == 1)
                return false;

        /* Check lower diagonal on left side */
        for (i = row, j = col; j >= 0 &&
                      i < n; i++, j--)
            if (board[i, j] == 1)
                return false;

        return true;
    }

    public void Replay()
    {
        ClearBoard();
        dynamicUI.SetButtonsUnactive();
        dynamicUI.FadeOutWinningPathtext();
    }
    void AddToMoves(int i, int col, char indicator)
    {
        moves.Add(new Tuple<Tuple<int, int>, char>(new Tuple<int, int>(i, col), indicator));
    }
    /* A recursive utility function to solve N 
    Queen problem */
    bool solveNQUtil(int[,] chessBoard, int col, int n)
    {
     

        /* base case: If all queens are placed 
        then return true */
        if (col >= n)
        {
            return true;
        }

        /* Consider this column and try placing 
        this queen in all rows one by one */
        for (int i = 0; i < n; i++)
        {
     
            /* Check if the queen can be placed on 
            board[i,col] */
            if (isSafe(chessBoard, i, col, n))
            {
          

                chessBoard[i, col] = 1;
                AddToMoves(i, col, 'I');


                /* recur to place rest of the queens */
                if (solveNQUtil(chessBoard, col + 1, n) == true)
                    return true;

                /* If placing queen in board[i,col] 
                doesn't lead to a solution then 
                remove queen from board[i,col] */
                chessBoard[i, col] = 0; // BACKTRACK 
                AddToMoves(i, col, 'D');

            }
        }
        return false;


    }

    public void Solve()
    {
        if (!solved)
        {
            ((IPuzzle)this).ComputerSolve();
        }
      
    }
    public bool GetSafe()
    {
        return this.safe;
    }
  
    IEnumerator IterateThroughMoves(GameObject[,] queenPlacement)
    {
        {
     
            foreach (Tuple<Tuple<int, int>, char> move in moves)
            {
                yield return new WaitForSeconds(1/slider.value);
                if (move.Item2 == 'I')
                {
                    GameObject chessBoard = GameObject.Find("ChessBoard");
                    Transform cellTransform = board.GetCellAtXY(move.Item1.Item1, move.Item1.Item2).transform;
                    GameObject gameObject = (GameObject)Instantiate(queenPiecePrefab, cellTransform.position, cellTransform.rotation);
                    gameObject.transform.SetParent(chessBoard.transform);
                    RectTransform mRectTransform = gameObject.GetComponent<RectTransform>();
                    mRectTransform.sizeDelta = (randomCell.gameObject.GetComponent<RectTransform>().sizeDelta) / 1.5f;
                    queenPlacement[move.Item1.Item1, move.Item1.Item2] = gameObject;
                }
                else
                {
                    Destroy(queenPlacement[move.Item1.Item1, move.Item1.Item2]);
                }
            }
        }
        dynamicUI.SetWinningPathText();
        dynamicUI.SetButtonsActive();
        AudioManagement.Play("WinGame");
        solved = false;
    }
}