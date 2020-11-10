using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.Linq;

public class TravellingSalesman : MonoBehaviour, IPuzzle
{
    public static TravellingSalesman instance;
    Permutations Permutate;
    private int moveCount = 0;
    private int totalDistance = 0;
    public GameObject[] nodes;
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
        foreach (GameObject distance in GameObject.FindGameObjectsWithTag("Distance"))
        {
            distances.Add(Int32.Parse(distance.name));
        }
        this.playedNodes.Add(nodes[0]);
        Permutate = Permutations.instance;
        Solve();
      
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
        if (node.name == "A")
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
            edge = FindCurrentEdge().GetComponent<Edge>();
            edge.setColour();
            int index = FindIndex(FindCurrentEdge());
            DisplayDistance(distances[index]);
         }
    }

    private int FindIndex(GameObject edgeName)
    {
        for(int i = 0; i < (edges.Count) - 1; i++)
        {
            if(edges[i] == edgeName)
            {
                return i;
            }
        }
        return 0;
       
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
    private void Solve()
    {
        List<List<char>> intermediatePermutations = CalculatePermutationsForNodes();
        List<List<char>> finalPermuations = RemoveUneccesaryPermutations(intermediatePermutations);
    }
    private List<List<char>> RemoveUneccesaryPermutations(List<List<char>> permList)
    {
        Dictionary<char, int> intermediateLetterDict = new Dictionary<char, int>();    //(d,b,c) == (c,b,d) so removing those permutations as unnecessary
        char[] nodeChars = SetNodesToPermute();
        List<List<char>> finPermutations = new List<List<char>>();
        foreach (char c in nodeChars)
        {
            intermediateLetterDict.Add(c, 0);
        }
        foreach (IList<char> permutation in permList)
        {
            if (intermediateLetterDict[permutation[0]] < 1)
            {
                intermediateLetterDict[permutation[0]]++;
                finPermutations.Add((List<char>)permutation);
            }
            else
            {
                continue;
            }
           
        }
        return finPermutations;
    }
    private char[] SetNodesToPermute()
    {
        char[] nodeChars = { 'A', 'A', 'A' };
        for (int i = 1; i < (nodes.Length); i++)
        {
            nodeChars.SetValue(Convert.ToChar(nodes[i].name), i - 1); //Setting up intermediate list of nodes to be permuted, !(n-1) 
        }
        return nodeChars;
    }
    private List<List<char>> CalculatePermutationsForNodes()
    {
        char[] nodeChars = SetNodesToPermute();
        return Permutate.Permute(nodeChars);
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
