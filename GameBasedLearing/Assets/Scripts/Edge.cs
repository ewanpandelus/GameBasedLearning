using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Edge : MonoBehaviour
{
    TravellingSalesman travellingSalesman;
    private SpriteRenderer rend;
    private Color initialColour;
    Color lightGreen = new Color(46f/255f, 1,  0f/ 255f, 1);
    private bool selected;

    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        initialColour = rend.material.color;
        selected = false;
        
    }
    public void SetSelected(bool _selected)
    {
         selected = _selected;
    }
    private void Start()
    {
        travellingSalesman = TravellingSalesman.instance;
    }

    public void SetSortingOrder(int _sortingOrder)
    {
        this.rend.sortingOrder = _sortingOrder;
    }
    public void setColour(bool on)
    {
        if (on) 
        {
            rend.sortingOrder = -1;
            rend.material.color = lightGreen;
        }
        else
        {
           rend.sortingOrder = -2;
           rend.material.color = initialColour;
        }
            
        
        
    }
}
