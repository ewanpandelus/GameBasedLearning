using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalDataHolder:MonoBehaviour

{
    [SerializeField] private Slider slider;
    private string destinationLevel = "";
    private float sliderValue;
    private void Awake() 
    {
    DontDestroyOnLoad(transform.gameObject);
      
    }
    public string GetDestinationLevel()
    {
        return this.destinationLevel;
    }
    public void SetSliderValue(float val)
    {
        this.sliderValue = val;
    }
    public void SetDestinationLevel(string destinationLevelName)
    {
        destinationLevel = destinationLevelName;
    }
    public float RetrieveAndSaveSliderValue()
    {
        if (slider)
            {
            this.SetSliderValue(slider.value);
            return this.slider.value;
           }
        else
        {
            return this.sliderValue;
        }
       
    }
}
