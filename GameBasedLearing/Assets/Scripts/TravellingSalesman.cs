using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TravellingSalesman : MonoBehaviour, IPuzzle
{
    public static TravellingSalesman instance;
    private int moveCount = 0;
    private Boolean isMovePossible;
    private GameObject[] nodes;
    private GameObject[] edges;
    private GameObject[] playedEdges;
    private GameObject playedNodes;
    private GameObject currentNode;


    void Start()
    {
        if(!instance)
        {
            instance = this;
        }
        isMovePossible = true;
        nodes = GameObject.FindGameObjectsWithTag("Node");
        edges = GameObject.FindGameObjectsWithTag("Edge");
        currentNode = nodes[0];
        moveCount++;
    }
    void Update()
    {

    }
    public Boolean getIsMovePossible()
    {
        return isMovePossible;
    }
    public GameObject getCurrentNode()
    {
        return this.currentNode;
    }

  
    void IPuzzle.ComputerSolve()
    {
        throw new System.NotImplementedException();
    }

    void IPuzzle.DisplaySteps(int moveCount)
    {
        throw new System.NotImplementedException();
    }

    void IPuzzle.MakeMove()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
 

    void IPuzzle.StartGame()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
   

    void IPuzzle.WinGame(bool winCondition)
    {
        throw new System.NotImplementedException();
    }

     Boolean IPuzzle.CheckMoveIsPossible()
    {
        return isMovePossible = true ;    
    }
}
