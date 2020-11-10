using System;
using System.Collections.Generic;
using UnityEngine;

public class Permutations: MonoBehaviour
{

    public static Permutations instance;
    // Start is called before the first frame update
    void Start()
    {
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

}


