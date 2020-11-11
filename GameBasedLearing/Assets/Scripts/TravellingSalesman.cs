using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.UIElements;
using System.Diagnostics.Tracing;

public class TravellingSalesman : MonoBehaviour, IPuzzle
{
    public static TravellingSalesman instance;
    Permutations Permutate;
    private int moveCount = 0;
    private int totalDistance = 0;
    private GameObject[] nodes;
    List<GameObject> edges = new List<GameObject>();
    List<GameObject> playedNodes = new List<GameObject>();
    List<int> distances = new List<int>();
    private Edge edge;
    private int mininumDistance = 0;
  

    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
        Permutate = Permutations.instance;
        nodes = GameObject.FindGameObjectsWithTag("Node");
        edges = GameObject.FindGameObjectsWithTag("Edge").ToList<GameObject>();
        this.playedNodes.Add(nodes[0]);
        foreach (GameObject distance in GameObject.FindGameObjectsWithTag("Distance"))
        {
            distances.Add(Int32.Parse(distance.name));
        }
        
    }
    private void clearBoard() 
    { 
        foreach(GameObject edge in edges)
        {
            edge.GetComponent<Edge>().setColour(true);
        }
        foreach(GameObject node in nodes)
        {
            node.GetComponent<Node>().DeselectNode();
        }
        this.playedNodes.Clear();
        this.playedNodes.Add(nodes[0]);
        DisplayDistance(0);
    }

    public void Solve()
    {
        clearBoard();
        DisplayDistance(0);
        List<char> winningPath = new List<char>();
        int minDistance = int.MaxValue;
        List<List<char>> nodePermutations = Permutate.GetFinalPermutations();
        StartCoroutine(IterateThroughPermutations(winningPath, minDistance, nodePermutations));
        }

    IEnumerator IterateThroughPermutations(List<char> winningPath, int minDistance, List<List<char>> nodePermutations)
    {
        foreach (List<char> nodeList in nodePermutations)
        {
            nodeList.Add('A');
            foreach (char c in nodeList)
            {
                yield return new WaitForSecondsRealtime(1f);
                GameObject.Find(c.ToString()).GetComponent<Node>().SetNode();
            }

            if (this.totalDistance < minDistance)
            {
                minDistance = totalDistance;
                winningPath = nodeList;
            }
            SetDistance(0);
            clearBoard();
        }

        foreach (char c in winningPath)
        {
            yield return new WaitForSecondsRealtime(1f);
            GameObject.Find(c.ToString()).GetComponent<Node>().SetNode();}
       
    }

private List<Node> setNodes()
    {
        List<Node> nodesScripts = new List<Node>();
        foreach(GameObject node in nodes)
        {
            nodesScripts.Add(node.GetComponent<Node>());
        }
        return nodesScripts;
    }
    void Update()
    {

    }

    public Boolean GetIsMovePossible(GameObject node)
    {
        if (playedNodes.Count < 4)
        {
            return (!playedNodes.Contains(node));
        }
        if (node.name == "A" && playedNodes.Count < 5)
        {
            return true;
        }
        else return false;

    }
    private GameObject FindCurrentEdge()//Finds edge associated with recently played move, sometimes the edge name is backward so the nodes need to be flipped
    {
        try
        {
            if (GameObject.Find("(" + playedNodes[playedNodes.Count - 2].name + "," + playedNodes[playedNodes.Count - 1].name + ")") == null)
            {
                return GameObject.Find("(" + playedNodes[playedNodes.Count - 1].name + "," + playedNodes[playedNodes.Count - 2].name + ")");
            }
            else
            {
                return GameObject.Find("(" + playedNodes[playedNodes.Count - 2].name + "," + playedNodes[playedNodes.Count - 1].name + ")");
            }
        }
        catch (NullReferenceException e) 
        {
            UnityEngine.Debug.Log(e); 
        } 

        return null;


    }
    public void SetPlayedEdge()
    {
        if (playedNodes.Count >= 2)
        {
            edge = FindCurrentEdge().GetComponent<Edge>(); //Finds current edge pased
            edge.setColour(false);
            int index = FindIndex(FindCurrentEdge());
            DisplayDistance(distances[index]);
         }
    }
    public void SetLastEdge()
    {
        edge = FindCurrentEdge().GetComponent<Edge>(); //Finds current edge pased
        edge.setColour(false);
    }

    private int FindIndex(GameObject edgeName)
    {
        for(int i = 0; i < (edges.Count); i++)
        {
            if(edges[i] == edgeName)
            {
                return i;
            }
        }
        return 0;
       
    }
    private int [,] DistanceToNodes()
    {
        int[,] graph = {
        { 0, 10, 15, 20 },
        { 10, 0, 35, 25 },
        { 15, 35, 0, 30 },
        { 20, 25, 30, 0 } };
        return graph;
    }
    public void SetDistance(int distance)
    {
        this.totalDistance = distance;
    }
    private void DisplayDistance(int distanceAdd)
    {
        totalDistance += distanceAdd;
        GameObject.Find("TotalDistance").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Distance: " + totalDistance.ToString();
    }

    public void SetPlayedNode(GameObject playedNode)
    {
        this.playedNodes.Add(playedNode);
        this.moveCount++;
        GameObject.Find("MoveCount").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Move Count: " + moveCount.ToString(); //Increment move count and display
        SetPlayedEdge();
    }
    public void SetLastNode(GameObject playedNode)
    {
        this.playedNodes.Add(playedNode);
        SetLastEdge();
    }
    private Boolean TrySolution()
    {
        return totalDistance == mininumDistance;
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
