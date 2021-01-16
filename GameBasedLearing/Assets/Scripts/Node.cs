using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    TravelingSalesman travellingSalesman;
    private Boolean selected = false;
    private Image image;
    private Color initialColour;
    Color lightGreen = new Color(131f/255f, 243f/255f, 127f/255f, 1);
   
  
    void Start()
    {
        image = this.GetComponent<Image>();
        initialColour = image.color;
        travellingSalesman = TravelingSalesman.instance;
    }

    // Update is called once per frame
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!selected)
        {

            SetAllColours(true, -1);

            image.material.color = Color.white;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
        {
            SetAllColours(false, -2);
            image.material.color = initialColour;
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        SetNode();
    }
        private void SetAllColours(bool on, int sortingOrder)
    {
        if (!(this.name == "A" && travellingSalesman.GetPlayedNodes().Count != GameObject.FindGameObjectsWithTag("Node").Length))
        {
            Distance distance = travellingSalesman.GetDistance(FindEdgeAssociatedWithNode());
            Edge edge = FindEdgeAssociatedWithNode();
            edge.setColour(on);
            //edge.SetSortingOrder(sortingOrder);
            image.material.color = Color.white;
            distance.setColour(on);
        }
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
    private void SwitchOnNode()
    {
        SetAllColours(true, -1);
        selected = true;
    }
    public void SetNode()
    {
   
            if (travellingSalesman.GetIsMovePossible(gameObject))
            {
                SwitchOnNode();
                travellingSalesman.SetPlayedNode(gameObject, false);
            }
        }
    public void SetWinningNode()
    {
        SwitchOnNode();
        travellingSalesman.SetPlayedNode(gameObject, true);
        
    }
    public void DeselectNode()
    {
        image.material.color = initialColour;
        selected = false;
    }
    
}
