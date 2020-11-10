using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private SpriteRenderer rend;
    private Color initialColour;
    Color lightGreen = new Color(131f / 255f, 243f / 255f, 127f / 255f, 1);
    Color lightGren = new Color(46f/255f, 1,  0f/ 255f, 1);

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        initialColour = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setColour()
    {
        rend.material.color = lightGren;
    }
}
