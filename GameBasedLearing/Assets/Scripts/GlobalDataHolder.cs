using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalDataHolder:MonoBehaviour

{
    [SerializeField] private Slider slider;
    private GameObject cherryIcon;
    private TextMeshProUGUI cherryText;
    private string destinationLevel = "";
    private float sliderValue;
    private int cherryCount;

    private void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
       
    }
    public void LoadGlobalDataHolder()
    {
        PlayerData data = SaveSystem.LoadPLayer();
        if (data != null)
        {
            this.cherryCount = data.totalCherries;
        }
    }
    public void SetCherryIcon(GameObject _cherryIcon)
    {
        this.cherryIcon = _cherryIcon;
        cherryText = cherryIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void DisplayCherryCount()
    {
        this.cherryText.text = cherryCount.ToString();

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
    public int GetCherries()
    {
        return this.cherryCount;
    }
    public void SetCherries(int count) 
    {
        this.cherryCount = count;
    }
}
