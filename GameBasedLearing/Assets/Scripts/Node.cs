using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    TravellingSalesman travellingSalesman;
    private Boolean selected = false;
    private Renderer rend;
    private Color initialColour;
    Color lightGreen = new Color(131f/255f, 243f/255f, 127f/255f, 1);
   
  
    void Start()
    {
        rend = GetComponent<Renderer>();
        initialColour = rend.material.color;
        travellingSalesman = TravellingSalesman.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetAllColours(bool on, int sortingOrder)
    {
        if (!(this.name == "A" && travellingSalesman.GetPlayedNodes().Count != GameObject.FindGameObjectsWithTag("Node").Length))
        {
            Distance distance = travellingSalesman.GetDistance(FindEdgeAssociatedWithNode());
            Edge edge = FindEdgeAssociatedWithNode();
            edge.setColour(on);
            edge.SetSortingOrder(sortingOrder);
            rend.material.color = lightGreen;
            distance.setColour(on);
        }
    }
    void OnMouseEnter()
    {

        if (!selected) 
        {

            SetAllColours(true, -1);
          
            rend.material.color = lightGreen;
        }
    }
    private void OnMouseExit()
    {
        if (!selected)
        {
            SetAllColours(false,-2);
            rend.material.color = initialColour;
        }
    }

    private void OnMouseDown()
    {
        SetNode();
    }
    
    private Edge FindEdgeAssociatedWithNode()
    {
        try
        {
            if (GameObject.Find("(" + travellingSalesman.GetLastPlayedNode().name + "," + this.name + ")") == null)
            {
                return   (GameObject.Find("(" + this.name  + "," + travellingSalesman.GetLastPlayedNode().name + ")")).GetComponent<Edge>() ;
            }
            else
            {
                return GameObject.Find("(" + travellingSalesman.GetLastPlayedNode().name + "," + this.name + ")").GetComponent<Edge>();
            }
        }
        catch (NullReferenceException e)
        {
            UnityEngine.Debug.Log(e);
        }

        return null;

    }
    public void SetNode()
    {
   
            if (travellingSalesman.GetIsMovePossible(gameObject))
            {
            SetAllColours(true, -1);
                travellingSalesman.SetPlayedNode(gameObject, false);
                selected = true;
                
            }
        }
    public void SetWinningNode()
    {
        SetAllColours(true, -1);
        travellingSalesman.SetPlayedNode(gameObject, true);
    }
    public void DeselectNode()
    {
        rend.material.color = initialColour;
        selected = false;
    }
    
}
