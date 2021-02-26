using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellingBeeInfo : MonoBehaviour
{

    [SerializeField] private GameObject edgesParent;
    private List<GameObject> edges = new List<GameObject>();
    [SerializeField] GameObject distancesParent;
    private List<GameObject> distances = new List<GameObject>();
    [SerializeField] private GameObject nodesParent;
    private List<GameObject> nodes = new List<GameObject>();
    private void Awake()
    {
        for (int i = 0; i < nodesParent.transform.childCount; i++)
        {
            nodes.Add(nodesParent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < edgesParent.transform.childCount; i++)
        {
            edges.Add(edgesParent.transform.GetChild(i).gameObject);
          
        }
        for (int i = 0; i < distancesParent.transform.childCount; i++)
        {
            distances.Add(distancesParent.transform.GetChild(i).gameObject);
        }
    
    }
    public List<GameObject> GetNodes()
    {
        return this.nodes;
    }
    public List<GameObject> GetEdges()
    {
        return this.edges;
    }
    public List<GameObject> GetDistances()
    {
        return this.distances;
    }
}
