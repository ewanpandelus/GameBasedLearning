﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Distance : MonoBehaviour
{
    
    private Color initialColour;
    Color lightGreen = new Color(46f / 255f, 1, 0f / 255f, 1);

    void Awake()
    {
        initialColour = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color;
    }


    public void setColour(bool on)
    {
        if (on)
        {
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = lightGreen;
        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = initialColour;
        }

    }
}
    

