///BSD 3 - Clause License

 /// Copyright(c) 2021, ewanpandelus
 ///All rights reserved.

///Redistribution and use in source and binary forms, with or without
///modification, are permitted provided that the following conditions are met:

///1.Redistributions of source code must retain the above copyright notice, this
///list of conditions and the following disclaimer.

///2. Redistributions in binary form must reproduce the above copyright notice,
///this list of conditions and the following disclaimer in the documentation
///and/or other materials provided with the distribution.

///3. Neither the name of the copyright holder nor the names of its
///contributors may be used to endorse or promote products derived from
///this software without specific prior written permission.

///THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
///AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
///IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
///DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
///FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
///DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
///SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
///CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
///OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
///OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

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

public class TravelingSalesman : MonoBehaviour, IPuzzle
{
    public static TravelingSalesman instance;
    [SerializeField] private int winningPathLength;
    [SerializeField] private DynamicUI dynamicUI;
    [SerializeField] private TravellingBeeInfo travellingBeeInfo;
    Permutations Permutate;
    private int moveCount = 0;
    private int totalDistance = 0;
    private bool solved = false;
    private List<GameObject> nodes = new List<GameObject>();
    List<GameObject> edges = new List<GameObject>();
    List<GameObject> playedNodes = new List<GameObject>();
    List<GameObject> distanceObjects = new List<GameObject>();
    List<int> distances = new List<int>();
    [SerializeField] private UnityEngine.UI.Slider slider;
    private Edge edge;
    private int counter = 0;
    AudioManager AudioManagement;
    private GlobalDataHolder globalDataHolder;
    private Scene scene;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    void Start()
    {
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        scene = SceneManager.GetActiveScene();
        if (scene.name.Contains("Easy")){
            globalDataHolder.SetEasyTSP(true);
        }
        else
        {
            globalDataHolder.SetHardTSP(true);
        }
        AudioManagement = AudioManager.instance;
        Permutate = Permutations.instance;
        nodes = travellingBeeInfo.GetNodes();
        edges = travellingBeeInfo.GetEdges();
        distanceObjects = travellingBeeInfo.GetDistances();
        this.playedNodes.Add(nodes[0]);
        foreach (GameObject distance in distanceObjects)
        {
            distances.Add(Int32.Parse(distance.name));
        }
        AudioManagement.Play("Bee");
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

    public void Reset()
    {
        if (!solved)
        {
            ClearBoard(0);
        }
       
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

    /// <summary>
    /// Coroutine which solves the problem graphically
    /// </summary>
    /// <param name="winningPath">Winning path to be displayed to players</param>
    /// <param name="minDistance">Current minimum distance of path</param>
    /// <param name="nodePermutations">All permutations for nodes (paths) to be
    /// iterated over</param>
    IEnumerator IterateThroughPermutations(List<char> winningPath, int minDistance, List<List<char>> nodePermutations)
    {
       
        foreach (List<char> nodeList in nodePermutations)
        {
            counter++;
            nodeList.Add('A');
            yield return new WaitForSecondsRealtime(1 / slider.value);   // Shows graphic display with speed relative to the problem size
            foreach (char c in nodeList)
            {
                GameObject.Find(c.ToString()).GetComponent<Node>().SetNode();
                yield return new WaitForSecondsRealtime(1 / slider.value);
            }

            if (this.totalDistance < minDistance)
            {
                minDistance = totalDistance;
                winningPath = nodeList;
            }
            yield return new WaitForSecondsRealtime(1 / slider.value);
            SetDistance(0);
            ClearBoard(0);
        }

        foreach (char c in winningPath)
        {
            yield return new WaitForSecondsRealtime(1f);
            GameObject.Find(c.ToString()).GetComponent<Node>().SetWinningNode();
        }
        TrySolution();
        solved = false;
    }

    public Boolean GetIsMovePossible(GameObject node)
    {
        return ((IPuzzle)this).CheckMoveIsPossible(node);
    }

    /// <summary>
    /// Finds the edge between the most recently played nodes
    /// </summary>
    /// <returns>Edge between most recently played nodes</returns>
    private GameObject FindCurrentEdge()
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

    /// <summary>
    /// Plays an edge, changing its colour and adding up the 
    /// current paths total distance
    /// </summary>
    /// <param name="played">Boolean which checks if played</param>
    public void SetPlayedEdge(bool played)
    {
        if (playedNodes.Count >= 2)
        {
            edge = FindCurrentEdge().GetComponent<Edge>(); 
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
        dynamicUI.ReplayGame();
    }

    /// <summary>
    /// Method which checks if player found shortest path
    /// and feedsback to player if they did, or didn't.
    /// </summary>
    void IPuzzle.TrySolution()
    {
        if (this.totalDistance == winningPathLength &&playedNodes.Count-1  == nodes.Count)
        {
                dynamicUI.WinGame();
                dynamicUI.ShowCherryAdd(nodes.Count);
                AudioManagement.Play("WinGame");
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
        if (playedNodes.Count < nodes.Count)
        {
            return (!playedNodes.Contains(node));
        }
        if (node.name == "A" && playedNodes.Count < nodes.Count + 1)
        {
            return true;
        }
        else return false;
    }

    public bool GetSolved()
    {
        return this.solved;
    }
}
