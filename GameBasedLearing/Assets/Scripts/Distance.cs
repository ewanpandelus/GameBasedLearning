using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Distance : MonoBehaviour
{
    
    private Color initialColour;
    Color lightGreen = new Color(46f / 255f, 1, 0f / 255f, 1);
    public TextMeshProUGUI tmp;

    void Awake()
    {
        initialColour = tmp.color;
    }


    public void setColour(bool on)
    {
        if (on)
        {
            tmp.color = lightGreen;
        }
        else
        {
            tmp.color = initialColour;
        }

    }
}
    

