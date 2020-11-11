using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine.UIElements;

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
        
        nodes = GameObject.FindGameObjectsWithTag("Node");
        edges = GameObject.FindGameObjectsWithTag("Edge").ToList<GameObject>();
        this.playedNodes.Add(nodes[0]);
        foreach (GameObject distance in GameObject.FindGameObjectsWithTag("Distance"))
        {
            distances.Add(Int32.Parse(distance.name));
        }
        SetupTSP();
        
    }
    private int [,] CreateAdjacencyList()
    {
        int[,] graph = {
        { 0, 10, 15, 20 },
        { 10, 0, 35, 25 },
        { 15, 35, 0, 30 },
        { 20, 25, 30, 0 }
        };
        return graph;
    }
    private int SetupTSP()
    {
        int n = nodes.Count();
        bool[] v = new bool[n];
        int [,]graph = CreateAdjacencyList();
        v[0] = true;
        return tsp(graph, v, 0, n, 1, 0, int.MaxValue);
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
            Debug.Log(e); 
        } 

        return null;


    }
    public void SetPlayedEdge()
    {
        if (playedNodes.Count >= 2)
        {
            edge = FindCurrentEdge().GetComponent<Edge>(); //Finds current edge pased
            edge.setColour();
            int index = FindIndex(FindCurrentEdge());
            DisplayDistance(distances[index]);
         }
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
        
    }
    private Boolean TrySolution()
    {
        return totalDistance == mininumDistance;
    }
 
    private int tsp(int[,] graph, bool[] v, int currPos,
        int n, int count, int cost, int ans)
    {
        List<Node> nodeObjects = setNodes();

        // If last node is reached and it has a link 
        // to the starting node i.e the source then 
        // keep the minimum value out of the total cost 
        // of traversal and "ans" 
        // Finally return to check for more possible values 
        if (count == n && graph[currPos, 0] > 0)
        {
            ans = Math.Min(ans, cost + graph[currPos, 0]);
            return ans;
        }

        // BACKTRACKING STEP 
        // Loop to traverse the adjacency list 
        // of currPos node and increasing the count 
        // by 1 and cost by graph[currPos,i] value 
        for (int i = 0; i < n; i++)
        {
            if (v[i] == false && graph[currPos, i] > 0)
            {

                // Mark as visited 
                v[i] = true;
                nodeObjects[i].SetNode();
                
                ans = tsp(graph, v, i, n, count + 1,
                    cost + graph[currPos, i], ans);

                // Mark ith node as unvisited 
                v[i] = false;
            }
        }
        return ans;
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
