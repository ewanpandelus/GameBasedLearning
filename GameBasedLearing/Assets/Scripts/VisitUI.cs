using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisitUI : MonoBehaviour
{
    private GlobalDataHolder globalDataHolder;
    [SerializeField] private Image mergeSort, bubbleSort, easyTSP, hardTSP, nQueensLevel1, nQueensLevel2;

    private void Start()
    {
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        bool on = false;
        on = globalDataHolder.GetBubbleSort();
        SetImageColour(bubbleSort,on);
        on = globalDataHolder.GetMergeSort();
        SetImageColour(mergeSort, on);
        on = globalDataHolder.GetEasyTSP();
        SetImageColour(easyTSP, on);
        on = globalDataHolder.GetHardTSP();
        SetImageColour(hardTSP, on);
        on = globalDataHolder.GetNQueensLevel1();
        SetImageColour(nQueensLevel1, on);
        on = globalDataHolder.GetNQueensLevel2();
        SetImageColour(nQueensLevel2, on);
    }

    private  void SetImageColour(Image image, bool on)
    {
        if (on)
        {
            image.color = Color.green;
        }
    }
}
