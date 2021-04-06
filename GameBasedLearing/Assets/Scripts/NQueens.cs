using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NQueens : MonoBehaviour, IPuzzle
{
    [SerializeField] private ChessBoard board;
    [SerializeField] private Slider slider;
    private GameObject[] queens;
    [SerializeField] private GameObject queenPiecePrefab;
    private bool safe = true;
    private GameObject parentCanvas;
    private int problemSize;
    private bool solved = false;
    private List<Tuple<Tuple<int, int>, char>> moves = new List<Tuple<Tuple<int, int>, char>>();
    [SerializeField] private DynamicUI dynamicUI;
    private AudioManager audioManager;
    private QueenSpawner queenSpawner;
    private GlobalDataHolder globalDataHolder;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        queenSpawner = GameObject.Find("GameManager").GetComponent<QueenSpawner>();
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        parentCanvas = GameObject.Find("ParentCanvas");
        board.Create();
        problemSize = board.GetProblemSize();
        if(problemSize == 4)
        {
            globalDataHolder.SetNQueensLevel1(true);
        }
        else
        {
            globalDataHolder.SetNQueensLevel2(true);
        }
    }

    public bool CheckMoveIsPossible(GameObject GO)
    {
        throw new System.NotImplementedException();
    }

    ///<summary>Solves puzzle graphically </summary>
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

    /// <summary>Tests if player's solution is correct</summary>
    void IPuzzle.TrySolution()
    {
        safe = true;
        queens = GameObject.FindGameObjectsWithTag("Queen");
        
        foreach (GameObject queen in queens)
        {
            safe = safe && queen.GetComponent<QueenPiece>().GetSafe();

        }
        if (safe && queens.Length == board.GetProblemSize())
        {
            WinBehaviour();
            
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

    /// <summary>
    /// Evaluates if a queen is fafe
    /// </summary>
    /// <param name="board"> int list representing thechessboard</param>
    /// <param name="row">Row in chessboard to check</param>
    /// <param name="col">Column in chessboard to check</param>
    /// <param name="n">Problem size</param>
    /// <returns>True if queen is safe, false otherwise</returns>
    bool isSafe(int[,] board, int row, int col, int n)
    {
        int i, j;
        for (i = 0; i < col; i++)
            if (board[row, i] == 1)
                return false;
        for (i = row, j = col; i >= 0 &&
             j >= 0; i--, j--)
            if (board[i, j] == 1)
                return false;
        for (i = row, j = col; j >= 0 &&
                      i < n; i++, j--)
            if (board[i, j] == 1)
                return false;
        return true;
    }

    public void Replay()
    {
        ClearBoard();
        dynamicUI.ReplayGame();
    }
    void AddToMoves(int i, int col, char indicator)
    {
        moves.Add(new Tuple<Tuple<int, int>, char>(new Tuple<int, int>(i, col), indicator));
    }
  
   /// <summary>
   /// Solves NQueens puzzle(backtracking algorithm) and adds moves to list
   /// </summary>
   /// <param name="chessBoard">Two dimensional int list representing the board</param>
   /// <param name="col">Current column</param>
   /// <param name="n">Problem size</param>
   /// <returns>Recursive, returns true if board is currently safe and false otherwise</returns>
    bool solveNQUtil(int[,] chessBoard, int col, int n)
    {
        if (col >= n)
        {
            return true;
        }
        for (int i = 0; i < n; i++)
        {
            if (isSafe(chessBoard, i, col, n))
            {
                chessBoard[i, col] = 1;
                AddToMoves(i, col, 'I');
                if (solveNQUtil(chessBoard, col + 1, n) == true)
                    return true;
                chessBoard[i, col] = 0; 
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
    private void WinBehaviour()
    {
        dynamicUI.WinGame();
        audioManager.Play("WinGame");
    }

    /// <summary>
    /// This Coroutine solves NQueens graphically, spwaning and destorying 
    /// queens(uses backtracking algorithm)
    /// </summary>
    /// <param name="queenPlacement">List of queens on the board, and their positions</param>
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
                    queenSpawner.SpawnQueenOnBoard(cellTransform.position);
                    GameObject gameObject = queenSpawner.GetLatestQueen();
                    queenPlacement[move.Item1.Item1, move.Item1.Item2] = gameObject;
                    
                }
                else
                {
                    Destroy(queenPlacement[move.Item1.Item1, move.Item1.Item2]);
                }
            }
        }
        WinBehaviour();
        dynamicUI.ShowCherryAdd(problemSize);
        solved = false;
    }
}