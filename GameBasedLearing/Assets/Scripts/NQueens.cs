using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NQueens: MonoBehaviour,IPuzzle
{
    [SerializeField]
    private ChessBoard board;

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
        throw new System.NotImplementedException();
    }

    void Start()
    {
        board.Create();
    }

    
    
}
