using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outline : MonoBehaviour
    
{
    private Image outLineImage;
    private void Start()
    {
        outLineImage = this.GetComponent<Image>();
        outLineImage.enabled = false;
    }
    public void SetOutlineImage(bool val )
    {
        outLineImage.enabled = val;
    }

}
