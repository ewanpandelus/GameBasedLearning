using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;

public class Permutations: MonoBehaviour
{
    private GameObject[] nodes;
    public static Permutations instance;
    // Start is called before the first frame update
    void Awake()
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
    public List<List<char>> GetFinalPermutations()
    {
        return SolveFinalPermutations();
    }
    private List<List<char>> SolveFinalPermutations()
    {
        List<List<char>> intermediatePermutations = CalculateAllPermutationsForNodes();
        List<List<char>> finalPermuations = RemoveUneccesaryPermutations(intermediatePermutations);
        return finalPermuations;
    }
    private List<List<char>> CalculateAllPermutationsForNodes()
    {
        char[] nodeChars = SetNodesToPermute();
        return Permute(nodeChars);
    }
    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    private List<List<char>> RemoveUneccesaryPermutations(List<List<char>> permList)
    {
        List<List<char>> finPermutations = new List<List<char>>();
        List<string> paths = new List<string>();
        foreach (IList<char> permutation in permList)
        {
            string s = new string(permutation.ToArray());
            if (paths.Contains(Reverse(s)))
            {
                continue;
            }
            else
            {
                paths.Add(s);
                finPermutations.Add((List<char>)permutation);
            }
        }

        return finPermutations;
    }
    private char[] SetNodesToPermute()
    {
        char[] nodeChars = new char[nodes.Length-1];
        for (int i = 1; i < (nodes.Length); i++)
        {
            nodeChars.SetValue(Convert.ToChar(nodes[i].name), i - 1); //Setting up intermediate list of nodes to be permuted, !(n-1) 
        }
        return nodeChars;
    }

}


