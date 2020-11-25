using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplexityGraph : MonoBehaviour

{
    private RectTransform graphContainer;
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite lineSprite;
    private void Awake()
    {
        graphContainer = GameObject.Find("GraphContainer").GetComponent<RectTransform>();

        ShowGraph(CreateLinearList(),Color.red,10f);
        ShowGraph(CreateConstantList(), Color.yellow,10f);
        ShowGraph(new List<float>() { 0, 1, 2, 3, 6, 12, 60,200,600}, Color.blue,25f);
        ShowGraph(CreateONLogNList(), Color.white, 15f);
        ShowGraph(CreateLogNList(), Color.magenta, 15f);
        ShowGraph(CreateNSquaredList(),Color.green,15f);
      
    }
    private List<float> CreateONLogNList()
    {
        List<float> valueList = new List<float>();
        valueList.Add(0);
        for (int i = 1; i < 42; i++)
        {
            valueList.Add(i*(8 * (Mathf.Log10(i))));
        }
        return valueList;
    }
    private List<float> CreateLogNList()
    {
        List<float> valueList = new List<float>();
        valueList.Add(0);
        for (int i = 1; i < 45; i++)
        {
            valueList.Add(40*(Mathf.Log10(i)));
        }
        return valueList;
    }
    private List<float> CreateConstantList()
    {
        List<float> valueList = new List<float>();
        for (int i = 0; i < 67; i++)
        {
            valueList.Add(1);
        }
        return valueList;
    }
    private List<float> CreateNSquaredList()
    {
        List<float> valueList = new List<float>();
        for (int i = 0; i < 25; i++)
        {
            valueList.Add((Mathf.Pow(i,2)));
        }
        return valueList;
    }
    private List<float> CreateLinearList()
    {
        List<float> valueList = new List<float>();
        for(int i = 0; i < 65; i++)
        {
            valueList.Add(i*5);
        }
        return valueList;
   }
    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);
        gameObject.GetComponent<Image>().sprite  = circleSprite;
        gameObject.GetComponent<Image>().color = Color.clear;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(5,5);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }
    private void ShowGraph(List<float> valueList, Color color,float xSize)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMax = 100f;
        
        GameObject prevCircleObj = null;
        for(int i = 0; i < valueList.Count; i++)
        {
            float xPos = 43f+ (i * xSize);
            float yPos = 39f + (valueList[i] / yMax) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPos, yPos));
            

            if (prevCircleObj)
            {
                CreateDotConnection(prevCircleObj.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition,color);
            }
            prevCircleObj = circleGameObject;
        }
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
    }
}
