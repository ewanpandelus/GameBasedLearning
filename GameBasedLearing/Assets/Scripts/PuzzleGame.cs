using System;
using System.Xml.Serialization;
using UnityEngine;
public interface IPuzzle
{
    void MakeMove();
    void StartGame();

    Boolean CheckMoveIsPossible();
    
    void ComputerSolve();

    void WinGame(Boolean winCondition);

    void DisplaySteps(int counter);
}