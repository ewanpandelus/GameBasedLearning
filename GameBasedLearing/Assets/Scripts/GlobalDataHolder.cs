using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalDataHolder : MonoBehaviour

{
    [SerializeField] private Slider slider;
    private GameObject cherryIcon;
    private bool nQueensLevel1=false, nQueensLevel2 = false, mergeSort = false, 
        bubbleSort = false, easyTSP = false, hardTSP = false;
    private TextMeshProUGUI cherryText;
    private string destinationLevel = "";
    private float sliderValue;
    private int cherryCount;
    private string levelToAssessComplexity = "";

    private void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
       
    }
    public void LoadGlobalDataHolder()
    {
        TotalCherriesData data = SaveSystem.LoadTotalCherriesData();
        if (data != null)
        {
            this.cherryCount = data.totalCherries;
        }
        LevelVisitData levelVisitData = SaveSystem.LoadLevelVisitData();
        if(levelVisitData!= null)
        {
            this.mergeSort = levelVisitData.mergeSort;
            this.bubbleSort = levelVisitData.bubbleSort;
            this.easyTSP = levelVisitData.easyTSP;
            this.hardTSP = levelVisitData.hardTSP;
            this.nQueensLevel1 = levelVisitData.nQueensLevel1;
            this.nQueensLevel2 = levelVisitData.nQueensLevel2;
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
    public string GetLevelToAssessComplexity()
    {
        return this.levelToAssessComplexity;
    }
    public void SetLevelToAssessComplexity(string levelName)
    {
        this.levelToAssessComplexity = levelName;
    }

    public int GetCherries()
    {
        return this.cherryCount;
    }
    public void SetCherries(int count) 
    {
        this.cherryCount = count;
    }
    public bool GetMergeSort()
    {
        return this.mergeSort;
    }
    public void SetMergeSort(bool value)
    {
        this.mergeSort = value;
    }
    public bool GetBubbleSort()
    {
        return this.bubbleSort;
    }
    public void SetBubbleSort(bool value)
    {
        this.bubbleSort = value;
    }
    public bool GetEasyTSP()
    {
        return this.easyTSP;
    }
    public void SetEasyTSP(bool value)
    {
        this.easyTSP = value;
    }
    public bool GetHardTSP()
    {
        return this.hardTSP;
    }
    public void SetHardTSP(bool value)
    {
        this.hardTSP = value;
    }
    public bool GetNQueensLevel1()
    {
        return this.nQueensLevel1;
    }
    public void SetNQueensLevel1(bool value)
    {
        this.nQueensLevel1 = value;
    }
    public bool GetNQueensLevel2()
    {
        return this.nQueensLevel2;
    }
    public void SetNQueensLevel2(bool value)
    {
        this.nQueensLevel2 = value;
    }
}
