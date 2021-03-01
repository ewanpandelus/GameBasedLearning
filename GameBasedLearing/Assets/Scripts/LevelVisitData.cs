using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelVisitData 
{
    public bool nQueensLevel1, nQueensLevel2, mergeSort, bubbleSort, easyTSP, hardTSP;

    public LevelVisitData(GlobalDataHolder globalDataHolder)
    {
        this.nQueensLevel1= globalDataHolder.GetNQueensLevel1();
        this.nQueensLevel2 = globalDataHolder.GetNQueensLevel2();
        this.mergeSort = globalDataHolder.GetMergeSort();
        this.bubbleSort = globalDataHolder.GetBubbleSort();
        this.easyTSP = globalDataHolder.GetEasyTSP();
        this.hardTSP = globalDataHolder.GetHardTSP();

    }
}
