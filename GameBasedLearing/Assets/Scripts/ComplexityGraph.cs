using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComplexityGraph : MonoBehaviour

{

    private string sceneName;
    private RectTransform graphContainer;
    private GameObject title;
    [SerializeField] private Sprite circleSprite;
    private int n = 6;
    [SerializeField] private Sprite lineSprite;
    [SerializeField] private bool complexityIllustration;
    [SerializeField] private GameObject xScaleText;
    private List<GameObject> pointList = new List<GameObject>();
    private List<GameObject> lineList = new List<GameObject>();
    private List<GameObject> scaleTextList = new List<GameObject>();
    private Vector3 down = new Vector3(0, -20, 0);
    private Vector3 left = new Vector3(-90, 0, 0);
    ComplexityDataHolder complexityData; 
    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;

        if (complexityIllustration)
        {
            complexityData = GameObject.FindGameObjectWithTag("GameController").GetComponent<ComplexityDataHolder>();
        }
      
       
     
      
        graphContainer = GameObject.Find("GraphContainer").GetComponent<RectTransform>();
       

      
    }

    public void IncrementN()
    {
        n++;
    }
    public void DecrementN()
    {
        n--;
    }
    private void Start()
    {
        if (!complexityIllustration)
        {
            ShowDifferentComplexities();
        }
        else
        {
            UpdateN();
        }
        
    }
    public void UpdateN()
    {
    
        title = GameObject.Find("XText");
        title.GetComponent<TextMeshProUGUI>().text = "N = " + n.ToString();
    }
    public void UpdateGraph()
    {
        ClearGraph();
        List<float> dataList = new List<float>();
        if(sceneName == "HardTSP"||sceneName == "EasyTSP")
        {
            dataList = complexityData.GetTBPValueList(n);
        }
        ShowChangableGraph(dataList, Color.yellow + new Color(0, 0, 0, -0.5f),"Traveling Bee Algorithm");

    }
    private void ShowDifferentComplexities()
    {
       ShowGraph(CreateLinearList(65), Color.red, 10f,0,1);
       ShowGraph(CreateConstantList(67), Color.yellow, 10f,0,1);
       ShowGraph(new List<float>() { 0, 1, 2, 3, 6, 12, 60, 200, 600 }, Color.blue, 25f,0,1);
       ShowGraph(CreateONLogNList(42), Color.white, 15f,0,1);
       ShowGraph(CreateLogNList(45), Color.magenta, 15f, 0,1);
       ShowGraph(CreateNSquaredList(25), Color.green, 15f,0,1);
    }
    private List<float> CreateFactorialList(int problemSize)
    {
        List<float> valueList = new List<float>();
        valueList.Add(0);
        for (int i = 1 ;i<=problemSize; i++)
        {
            valueList.Add(Factorial(i));
        }
        return valueList;
    }
    private int Factorial(int n)
    {
        if (n == 0)
            return 1;
        else
            return n * Factorial(n - 1);
    }
    private List<float> CreateONLogNList(int problemSize)
    {
        List<float> valueList = new List<float>();
        valueList.Add(0);
        for (int i = 1; i < problemSize; i++)
        {
            valueList.Add(i*(8 * (Mathf.Log10(i))));
        }
        return valueList;
    }
    private List<float> CreateLogNList(int problemSize)
    {
        List<float> valueList = new List<float>();
        valueList.Add(0);
        for (int i = 1; i < problemSize; i++)
        {
            valueList.Add(40*(Mathf.Log10(i)));
        }
        return valueList;
    }
    private List<float> CreateConstantList(int problemSize)
    {
        List<float> valueList = new List<float>();
        for (int i = 0; i < problemSize; i++)
        {
            valueList.Add(1);
        }
        return valueList;
    }
    private List<float> CreateNSquaredList(int problemSize)
    {
        List<float> valueList = new List<float>();
        for (int i = 0; i < problemSize; i++)
        {
            valueList.Add((Mathf.Pow(i,2)));
        }
        return valueList;
    }
    private List<float> CreateLinearList(int problemSize )
    {
        List<float> valueList = new List<float>();
        for(int i = 0; i < problemSize; i++)
        {
            valueList.Add(i*5);
        }
        return valueList;
   }
    private GameObject CreateCircle(Vector2 anchoredPosition,Color color,int size)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);
        gameObject.GetComponent<Image>().sprite  = circleSprite;
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(size,size);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }
    private void ShowGraph(List<float> valueList, Color color, float xSize, float waitTime, int scalingfactor)
    {
        Debug.Log(graphContainer.sizeDelta);
        float graphHeight = graphContainer.sizeDelta.y;
        float yMax = 100f;

        GameObject prevCircleObj = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPos = (i * xSize);
            float yPos = ((valueList[i] / yMax) * graphHeight) / scalingfactor;
            GameObject circleGameObject = CreateCircle(new Vector2(xPos, yPos), color + new Color(0, 0, 0, 0.5f),5);


            if (prevCircleObj)
            {
                CreateDotConnection(prevCircleObj.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, color);
            }
            prevCircleObj = circleGameObject;
        }
    }
    private void ShowChangableGraph(List<float> valueList, Color color,string name)
    {
        title = GameObject.Find("Title");
        title.GetComponent<TextMeshProUGUI>().text = name;
        float startY = 0;
        float startX = 0;
        float graphHeight = graphContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        float graphWidth = graphContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.x + 57;
        float yMax = valueList[valueList.Count - 1];
        float xSize = graphWidth / valueList.Count;
        GameObject prevCircleObj = null;
        for(int i = 0; i < valueList.Count; i++)
        {
            float xPos =  (i * xSize);
            float yPos = ((graphHeight/yMax) * valueList[i]);
           
            GameObject circleGameObject = CreateCircle(new Vector2(xPos, yPos), color + new Color(0, 0, 0, 0.5f),10);
            pointList.Add(circleGameObject);
            if (i == 0)
            {
                startX = circleGameObject.transform.position.x;
                startY = circleGameObject.transform.position.y;
            }
            Vector3 xAxis = new Vector3(circleGameObject.transform.position.x, startY, 0);
            CreateScaleText( i, xAxis+down);


            if (prevCircleObj)
            {
                CreateDotConnection(prevCircleObj.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, color);
            }
            prevCircleObj = circleGameObject;
            
        }
        ShowTotalSteps(valueList[valueList.Count - 1]);
      // Vector3 yAxis = new Vector3(startX, prevCircleObj.transform.position.y, 0);
      //  CreateScaleText(valueList[valueList.Count-1], yAxis+left);
    }
    private void ShowTotalSteps(float value)
    {
        GameObject totalSteps = GameObject.Find("TotalSteps");
        totalSteps.GetComponent<TextMeshProUGUI>().text ="Total Steps = " + value.ToString();
    }
    private void CreateScaleText(float value, Vector3 position)
    {
        if (n > 12)
        {
            if (value % 2 == 0)
            {
                InstantiateText(value, position);
            }
        }
        else
        {
            InstantiateText(value, position);
        }
    
    }
   
    private void InstantiateText(float value, Vector3 position)
    {
        GameObject textInstance = Instantiate(xScaleText, position, Quaternion.identity);
        textInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value.ToString();
        textInstance.transform.SetParent(graphContainer, true);
        scaleTextList.Add(textInstance);
    }
    private void CreateDotConnection(Vector2 dotPosition1, Vector2 dotPosition2,Color color)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);
        gameObject.GetComponent<Image>().color = color;
        gameObject.GetComponent<Image>().sprite = lineSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPosition2 - dotPosition1).normalized;
        float distance = Vector2.Distance(dotPosition1, dotPosition2);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPosition1 + dir *(distance/2);
        rectTransform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        lineList.Add(gameObject);
    }
    public void ClearGraph()
    {
        foreach(GameObject line in lineList)
        {
            Destroy(line);
          
        }
        foreach(GameObject point in pointList)
        {
            Destroy(point);
        
        }
        foreach(GameObject textInstance in scaleTextList)
        {
            Destroy(textInstance);
            
        }
        lineList.Clear();
        pointList.Clear();
        scaleTextList.Clear();
    }
}
