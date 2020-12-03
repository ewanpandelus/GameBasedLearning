using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NQueens: MonoBehaviour,IPuzzle
{
    [SerializeField]
    private ChessBoard board;
    GameObject[] queens;
    void Start()
    {
        board.Create();
       
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
        foreach(GameObject queen in queens)
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
        if (safe && count == 8)
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
        
        
        foreach(GameObject queen in queens)
        {
            Destroy(queen);
        }
    }

    
    
}
