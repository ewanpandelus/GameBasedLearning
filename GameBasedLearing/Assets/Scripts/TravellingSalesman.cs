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
using UnityEngine.SceneManagement;

public class TravellingSalesman : MonoBehaviour, IPuzzle
{
    
    public static TravellingSalesman instance;
    [SerializeField] private int winningPathLength;
    [SerializeField] private DynamicUI dynamicUI;
    Permutations Permutate;
    private int moveCount = 0;
    private int totalDistance = 0;
    private bool solved = false;
    private GameObject[] nodes;
    List<GameObject> edges = new List<GameObject>();
    List<GameObject> playedNodes = new List<GameObject>();
    List<GameObject> distanceObjects = new List<GameObject>();
    List<int> distances = new List<int>();
    private Edge edge;
  
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    void Start()
    {
        Permutate = Permutations.instance;
        nodes = GameObject.FindGameObjectsWithTag("Node");
        edges = GameObject.FindGameObjectsWithTag("Edge").ToList<GameObject>();
        distanceObjects = GameObject.FindGameObjectsWithTag("Distance").ToList<GameObject>(); ;
        this.playedNodes.Add(nodes[0]);
        foreach (GameObject distance in GameObject.FindGameObjectsWithTag("Distance"))
        {
            distances.Add(Int32.Parse(distance.name));
        }
        
    }
    private float Factorial(float n)
    {
        if (n == 0)
            return 1;
        else
            return n * Factorial(n - 1);
    }
    public GameObject GetLastPlayedNode()
    {
        return playedNodes[playedNodes.Count-1];
    }
    private void ClearBoard(int setDistance) 
    {
        SetDistance(setDistance);
        foreach (GameObject distance in distanceObjects)
        { 
            distance.GetComponent<Distance>().setColour(false);
        }
        foreach (GameObject edge in edges)
        {
            edge.GetComponent<Edge>().setColour(false);
        }
        foreach(GameObject node in nodes)
        {
            node.GetComponent<Node>().DeselectNode();
        }
        this.playedNodes.Clear();
        this.playedNodes.Add(nodes[0]);
        DisplayDistance(0);
    }
    public List<GameObject> GetPlayedNodes()
    {
        return this.playedNodes;
    }
    public Distance GetDistance(Edge _edge) 
    {
        int i = 0;
        foreach(GameObject edge in edges) 
        {
            if (_edge == edge.GetComponent<Edge>())
            {
                return distanceObjects[i].GetComponent<Distance>();
            }
            i++;
        }
        return null;
    }

    public void Solve()
    {
        ((IPuzzle)this).ComputerSolve();
    }

    IEnumerator IterateThroughPermutations(List<char> winningPath, int minDistance, List<List<char>> nodePermutations)
    {
        foreach (List<char> nodeList in nodePermutations)
        {
            nodeList.Add('A');
            yield return new WaitForSecondsRealtime(12f / Factorial(nodes.Length));   // Shows graphic display with speed relative to the problem size
            foreach (char c in nodeList)
            {
                
                GameObject.Find(c.ToString()).GetComponent<Node>().SetNode();
                yield return new WaitForSecondsRealtime(12f / Factorial(nodes.Length));
            }

            if (this.totalDistance < minDistance)
            {
                minDistance = totalDistance;
                winningPath = nodeList;
            }
            yield return new WaitForSecondsRealtime(12f/Factorial(nodes.Length));
            SetDistance(0);
            ClearBoard(0);
        }

        foreach (char c in winningPath)
        {
            yield return new WaitForSecondsRealtime(1f);
            GameObject.Find(c.ToString()).GetComponent<Node>().SetWinningNode();
        }
       
    }


    public Boolean GetIsMovePossible(GameObject node)
    {
        return ((IPuzzle)this).CheckMoveIsPossible(node);
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
    public void SetPlayedEdge(bool played)
    {
        if (playedNodes.Count >= 2)
        {
            edge = FindCurrentEdge().GetComponent<Edge>(); //Finds current edge passed
            edge.setColour(true);
            edge.SetSelected(true);
            
            int index = FindIndex(FindCurrentEdge());
            if (played) 
            {
                DisplayDistance(distances[index]);
            }
           
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
    public void SetDistance(int distance)
    {
        this.totalDistance = distance;
    }
    private void DisplayDistance(int distanceAdd)
    {
        totalDistance += distanceAdd;
        GameObject.Find("TotalDistance").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Distance: " + totalDistance.ToString();
    }

    public void SetPlayedNode(GameObject playedNode, Boolean bestPath)
    {
        ((IPuzzle)this).MakeMove(playedNode);
        if (!bestPath)
        {
            this.moveCount++;
            ((IPuzzle)this).DisplaySteps();
        }
    }
    public void TrySolution()
    {
        ((IPuzzle)this).TrySolution();
    }
    public void Replay()
    {
        DisplayDistance(totalDistance - totalDistance);
        ClearBoard(0);
        moveCount = 0;
        ((IPuzzle)this).DisplaySteps();
        dynamicUI.SetButtonsUnactive();
        dynamicUI.FadeOutWinningPathtext();
    }


    void IPuzzle.TrySolution()
    {
        if (this.totalDistance == winningPathLength &&playedNodes.Count-1  == nodes.Length)
        {
            if (!solved)
            {
                dynamicUI.SetWinningPathText();
                dynamicUI.SetButtonsActive();
            }
        }
        else
        {
            dynamicUI.SetWrongPathText();
            ClearBoard(0);
        }
        
    }
 
    
   
   void IPuzzle.ComputerSolve()
    {
        if (!solved) 
        {
            solved = true;
            ClearBoard(0);
            this.moveCount = 0;
            List<char> winningPath = new List<char>();
            int minDistance = int.MaxValue;
            List<List<char>> nodePermutations = Permutate.GetFinalPermutations();
            StartCoroutine(IterateThroughPermutations(winningPath, minDistance, nodePermutations));
        }
        
    }

    void IPuzzle.DisplaySteps()
    {
        GameObject.Find("MoveCount").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Move Count: " + moveCount.ToString(); //Increment move count and display
    }

    void IPuzzle.MakeMove(GameObject playedNode)
    {
        this.playedNodes.Add(playedNode);
        SetPlayedEdge(true);
    }

     Boolean IPuzzle.CheckMoveIsPossible(GameObject node)
    {
        if (playedNodes.Count < nodes.Length)
        {
            return (!playedNodes.Contains(node));
        }
        if (node.name == "A" && playedNodes.Count < nodes.Length + 1)
        {
            return true;
        }
        else return false;
    }

  
}
