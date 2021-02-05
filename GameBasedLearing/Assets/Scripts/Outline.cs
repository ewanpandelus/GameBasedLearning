using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outline : MonoBehaviour
    
{
    private Image outLineImage;
    private Color initialColour;
    private void Start()
    {
        outLineImage = this.GetComponent<Image>();
        outLineImage.enabled = false;
        initialColour = this.outLineImage.color;
    }
    public void SetOutlineImage(bool val )
    {
        outLineImage.enabled = val;
    }
    public void ResetColour()
    {
        outLineImage.color = initialColour;
    }
    public void SetColour(Color color)
    {
        outLineImage.color = color;
    }

}
