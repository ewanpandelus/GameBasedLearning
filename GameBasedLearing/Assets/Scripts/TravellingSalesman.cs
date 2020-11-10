using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TravellingSalesman : MonoBehaviour, IPuzzle
{
    public static TravellingSalesman instance;
    private int moveCount = 0;
    private GameObject[] nodes;
    private GameObject[] edges;
    List<GameObject> playedEdges = new List<GameObject>();
    List<GameObject> playedNodes = new List<GameObject>();
    private GameObject currentNode;
    private Edge edge;


    void Start()
    {
        if(!instance)
        {
            instance = this;
        }
        nodes = GameObject.FindGameObjectsWithTag("Node");
        edges = GameObject.FindGameObjectsWithTag("Edge");
        currentNode = nodes[0];
        this.playedNodes.Add(nodes[0]);
    }
    void Update()
    {

    }
    public Boolean getIsMovePossible(GameObject node)
    {
        if(playedNodes.Count < 4)
        {
            return (!playedNodes.Contains(node));
        }
        if (node.name == "A")
        {
            return true;
        }
        else return false;
          
    }
    public void setPlayedEdge()
    {
        if (playedNodes.Count >= 2)
        {
            try
            {
                edge = GameObject.Find("(" + playedNodes[playedNodes.Count - 2].name + "," + playedNodes[playedNodes.Count - 1].name + ")").GetComponent<Edge>();//Finds edge associated with recently played move
            }

            catch (NullReferenceException e)
          
            {
                edge = GameObject.Find("(" + playedNodes[playedNodes.Count - 1].name + "," + playedNodes[playedNodes.Count - 2].name + ")").GetComponent<Edge>();
            }
            edge.setColour();
         
            
        }
    }
    public GameObject getCurrentNode()
    {
        return this.currentNode;
    }

    public void setPlayedNode(GameObject playedNode)
    {
        this.playedNodes.Add(playedNode);
        this.moveCount++;
        GameObject.Find("MoveCount").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Move Count: " + moveCount.ToString(); //Increment move count and display
        
    }
  
    void IPuzzle.ComputerSolve()
    {
        throw new System.NotImplementedException();
    }

    void IPuzzle.DisplaySteps(int moveCount)
    {
        throw new System.NotImplementedException();
    }

    void IPuzzle.MakeMove(GameObject playedNode)
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
        return true;
    }
}
