using System;
using System.Collections.Generic;
using UnityEngine;

public class Permutations: MonoBehaviour
{
    private GameObject[] nodes;
    public static Permutations instance;
    // Start is called before the first frame update
    void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        if (!instance)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<List<char>> Permute(char[] nodes)
    {
        var list = new List<List<char>>();
        return DoPermute(nodes, 0, nodes.Length - 1, list);
    }

    List<List<char>> DoPermute(char[] nodes, int start, int end, List<List<char>> list)
    {
        if (start == end)
        {
            // We have one of our possible n! solutions,
            // add it to the list.
            list.Add(new List<char>(nodes));
        }
        else
        {
            for (var i = start; i <= end; i++)
            {
                Swap(ref nodes[start], ref nodes[i]);
                DoPermute(nodes, start + 1, end, list);
                Swap(ref nodes[start], ref nodes[i]);
            }
        }

        return list;
    }
    void Swap(ref char a, ref char b)
    {
        var temp = a;
        a = b;
        b = temp;
    }

    private void Solve()
    {
        List<List<char>> intermediatePermutations = CalculatePermutationsForNodes();
        List<List<char>> finalPermuations = RemoveUneccesaryPermutations(intermediatePermutations);
    }
    private List<List<char>> CalculatePermutationsForNodes()
    {
        char[] nodeChars = SetNodesToPermute();
        return Permute(nodeChars);
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

}


