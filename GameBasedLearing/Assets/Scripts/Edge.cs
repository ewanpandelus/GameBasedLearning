using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Edge : MonoBehaviour
{
    TravelingSalesman travelingSalesman;
    private Image image;
    private Color initialColour;
    Color lightGreen = new Color(46f/255f, 1,  0f/ 255f, 1);
    private bool selected;

    void Awake()
    {
        image = this.GetComponent<Image>();
        initialColour = image.color;
        selected = false;
    }

    private void Start()
    {
        travelingSalesman = TravelingSalesman.instance;
    }

    public void SetSelected(bool _selected)
    {
         selected = _selected;
    }

    public void setColour(bool on)
    {
        if (on) 
        {
            
            image.color = lightGreen;
        }
        else
        {
           image.color = initialColour;
        }
    }
}
