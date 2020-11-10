﻿using System;
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
    private GameObject Edge;
    // Start is called before the first frame update
    void Start()
    {
        travellingSalesman = TravellingSalesman.instance;
        rend = GetComponent<Renderer>();
        initialColour = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter()
    {

        if (!selected) 
        {
            rend.material.color = lightGreen;
        }
    }
    private void OnMouseExit()
    {
        if (!selected)
        {
            rend.material.color = initialColour;
        }
    }

    private void OnMouseDown()
    {
        if (travellingSalesman.getIsMovePossible())
        {

            selected = true;
        }
       
    }
}
