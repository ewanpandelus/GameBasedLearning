﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NQueens : MonoBehaviour, IPuzzle
{
    [SerializeField]
    private ChessBoard board;
    GameObject[] queens;
    private int counter = 0;
    [SerializeField] private GameObject queenPiecePrefab;
    private GameObject parentCanvas;
    private int problemSize;
    private float time;
    private List<Tuple<Tuple<int,int>,char>> moves = new List<Tuple<Tuple<int, int>, char>>();
    void Start()
    {
        parentCanvas = GameObject.Find("ParentCanvas");
        board.Create();
        problemSize = board.GetProblemSize();
        solveNQ(problemSize);

    }
    public bool CheckMoveIsPossible(GameObject GO)
    {
        throw new System.NotImplementedException();
    }

    public void ComputerSolve()
    {
        throw new System.NotImplementedException();
    }

    public void DisplaySteps()
    {
        throw new System.NotImplementedException();
    }

    public void MakeMove(GameObject playedObject)
    {
        throw new System.NotImplementedException();
    }

    public void TrySolution()
    {
        queens = GameObject.FindGameObjectsWithTag("Queen");
        bool safe = true;
        int count = 0;
        foreach (GameObject queen in queens)
        {
            safe = safe && queen.GetComponent<QueenPiece>().GetSafe();
            try
            {
                if (queen.GetComponent<QueenPiece>().GetCell())
                {
                    count++;
                }
            }
            catch
            {
                Debug.Log("Error");
            }


        }
        if (safe && count == board.GetProblemSize())
        {
            Debug.Log("Win!");
        }
        else
        {
            Debug.Log("Lose");
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
    private void WaitTime()
    {
        float  time = 0;
        while (time < 0.2f)
        {
            time += Time.deltaTime;
        }
       
    }
    void AddToMoves(int i , int col, char indicator)
    {
      moves.Add(new Tuple<Tuple<int,int>,char> (new Tuple<int, int>(i, col), indicator));
    }
    /* A recursive utility function to solve N 
    Queen problem */
    bool  solveNQUtil(int[,] chessBoard, int col, int n) 
    { 


        /* base case: If all queens are placed 
        then return true */
        if (col >= n)
        {
      
            Debug.Log(chessBoard);
            return true;
        }
       
        /* Consider this column and try placing 
        this queen in all rows one by one */
        for (int i = 0; i < n; i++)
        {
            counter++;
           



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
        ((IPuzzle)this).ComputerSolve();
    }
    IEnumerator IterateThroughMoves(GameObject[,] queenPlacement)
    {
        {
            foreach (Tuple<Tuple<int, int>, char> move in moves)
            {
                yield return new WaitForSeconds(0.2f);
                if (move.Item2 == 'I')
                {
                    Transform cellTransform = board.GetCellAtXY(move.Item1.Item1, move.Item1.Item2).transform;
                    GameObject gameObject = (GameObject)Instantiate(queenPiecePrefab, cellTransform.position, cellTransform.rotation);
                    gameObject.transform.SetParent(parentCanvas.transform);
                    queenPlacement[move.Item1.Item1, move.Item1.Item2] = gameObject;
                }
                else
                {
                    Destroy(queenPlacement[move.Item1.Item1, move.Item1.Item2]);
                }
            }
        }
    }

    void solveNQ(int problemSize)
    {
        int[,] board = new int[problemSize, problemSize];
        GameObject[,] queenPlacement = new GameObject[problemSize, problemSize];
        solveNQUtil(board, 0, problemSize);
        StartCoroutine(IterateThroughMoves(queenPlacement));

    }
}