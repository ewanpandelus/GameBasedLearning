using System;
using System.Xml.Serialization;
using UnityEngine;
public interface IPuzzle
{
    void MakeMove(GameObject playedObject);
    void TrySolution();    
    void ComputerSolve();
    void DisplaySteps();
    bool CheckMoveIsPossible(GameObject GO );
}