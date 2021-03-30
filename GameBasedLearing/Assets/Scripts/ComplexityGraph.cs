using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private int n = 8;
    [SerializeField] private Sprite lineSprite;
    [SerializeField] private bool complexityIllustration;
    [SerializeField] private GameObject xScaleText;
    private List<GameObject> pointList = new List<GameObject>();
    private List<GameObject> lineList = new List<GameObject>();
    private List<GameObject> scaleTextList = new List<GameObject>();
    private Vector3 down = new Vector3(0, -20, 0);
    private Vector3 left = new Vector3(-90, 0, 0);
    private GlobalDataHolder globalDataHolder;
    ComplexityDataHolder complexityData; 


    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        if (complexityIllustration)
        {
            complexityData = GameObject.FindGameObjectWithTag("GameController").GetComponent<ComplexityDataHolder>();
        }
        graphContainer = GameObject.Find("GraphContainer").GetComponent<RectTransform>();
    }

    public void IncrementN()
    {
        if (n != 20)
        {
            n += 2;
        }
    }

    public void DecrementN()
    {
        if (n != 2)
        {
            n -= 2;
        }
     

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
            ShowChangableGraph(dataList, Color.yellow + new Color(0, 0, 0, -0.5f), "Travelling Bee Algorithm");
        }
        if(sceneName =="NQueensLevel1"||sceneName == "NQueensLevel2")
        {
            dataList = complexityData.GetNQueensValueList(n);
            ShowChangableGraph(dataList, new Color(168f/255f,37f/255f,224f/255f,1)+ new Color(0, 0, 0, -0.5f), "NQueens Algorithm");
        }
        if(sceneName == "BubbleSort")
        {
            dataList = complexityData.GetBubbleSortValueList(n);
            ShowChangableGraph(dataList, Color.red + new Color(0, 0, 0, -0.5f), "          Bubblesort");
        }
        if(sceneName =="MergeSort")
        {
            dataList = complexityData.GetMergeSortValueList(n);
            ShowChangableGraph(dataList, Color.green + new Color(0, 0, 0, -0.5f), "          MergeSort");
        }
    }

    private void ShowDifferentComplexities()
    {
       ShowGraph(CreateLinearList(65), Color.red, 10f,0,1);
       ShowGraph(CreateConstantList(67), Color.yellow, 10f,0,1);
       ShowGraph(new List<float>() { 0, 1, 2, 3, 6, 12, 60, 200, 600 }, Color.blue, 25f,0,1);
       ShowGraph(CreateONLogNList(42), Color.white, 15f,0,1);
       ShowGraph(CreateLogNList(45), Color.magenta, 15f, 0,1);
       ShowGraph(CreateNSquaredList(25), Color.green, 15f,0,1);
       ShowGraph(CreateExponentialList(9), Color.cyan, 31f, 0, 1);
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

    private List<float> CreateExponentialList(int problemSize)
    {
        List<float> valueList = new List<float>();
        valueList.Add(0);
        for (int i = 1; i <= problemSize; i++)
        {
            valueList.Add(Exponential(i)*1.15f);
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

    private int Exponential(int n) 
    {
        return (int)Math.Pow(2, n);
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

    private void ShowChangableGraph(List<float> valueList, Color color, string name)
    {
        title = GameObject.Find("Title");
        title.GetComponent<TextMeshProUGUI>().text = name;
        float startY;
        float graphHeight = graphContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        float graphWidth = graphContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.x + 57;
        float yMax = valueList.Max();
        float xSize = graphWidth / valueList.Count;
        GameObject prevCircleObj = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            if (valueList[i] == -1)
            {
                continue;
            }
            float xPos = (i * xSize);
            float yPos = ((graphHeight / yMax) * valueList[i]);
            startY = GameObject.Find("Origin").transform.position.y;
            GameObject circleGameObject = CreateCircle(new Vector2(xPos, yPos), color + new Color(0, 0, 0, 0.5f), 10);
            pointList.Add(circleGameObject);
            if (i == 0)
            {
                startY = circleGameObject.transform.position.y;
            }
            Vector3 xAxis = new Vector3(circleGameObject.transform.position.x, startY, 0);
            CreateScaleText(i, xAxis + down, valueList[i]);
            if (prevCircleObj)
            {
                CreateDotConnection(prevCircleObj.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, color);
            }
            prevCircleObj = circleGameObject;
        }
    }

    private void ShowValueUnderGraph(float value, Vector3 position, float i)
    {
        InstantiateText(Round(value), position, 12, i);
    }

    private bool IfViableToShow(float i)
    {
        if (n > 9)
        {
            if (i % 2 == 0)
            {

                return true;

            }
            else return false;
        }
        return true;
    }
    private void CreateScaleText(float loop, Vector3 position, float value)
    {
        if (IfViableToShow(loop))
        {
            InstantiateText(loop, position, 32, loop);
            ShowValueUnderGraph(value, position + (2.5f * down), loop);
        }
    }

    public static float Round(float value)
    {
        if (value > 10000)
        {
            string s = value.ToString();
            float rounded = Mathf.Round(value / (1 * (Mathf.Pow(10, s.Length - 4))));
            return rounded * (Mathf.Pow(10, s.Length - 4));
        }
        else return value;
    }

    private void InstantiateText(float value, Vector3 position, int size, float i)
    {
        GameObject textInstance = Instantiate(xScaleText, position, Quaternion.identity);
        if (value > 10000 && i % 2 == 0)
        {
            var exponent = (Math.Floor(Math.Log10(Math.Abs(value))));
            var mantissa = (value / Math.Pow(10, exponent));
            textInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = mantissa.ToString("F2") + "e" + exponent;
            textInstance.transform.SetParent(graphContainer, true);
            textInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = size;
        }
        if (value > 10000 && i % 2 != 0)
        {
            return;
        }
        if (value < 10000 &&i%2 == 0)
        {
            textInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value.ToString();
            textInstance.transform.SetParent(graphContainer, true);
            textInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = size;
        }
        scaleTextList.Add(textInstance);
    }

    private void CreateDotConnection(Vector2 dotPosition1, Vector2 dotPosition2, Color color)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = color;
        gameObject.GetComponent<Image>().sprite = lineSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPosition2 - dotPosition1).normalized;
        float distance = Vector2.Distance(dotPosition1, dotPosition2);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPosition1 + dir * (distance / 2);
        rectTransform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        lineList.Add(gameObject);
    }

    public void ClearGraph()
    {
        foreach (GameObject line in lineList)
        {
            Destroy(line);

        }
        foreach (GameObject point in pointList)
        {
            Destroy(point);

        }
        foreach (GameObject textInstance in scaleTextList)
        {
            Destroy(textInstance);

        }
        lineList.Clear();
        pointList.Clear();
        scaleTextList.Clear();
    }
}