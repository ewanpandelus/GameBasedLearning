using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TotalCherriesData
{
    public int totalCherries;

    public TotalCherriesData(GlobalDataHolder globalDataHolder)
    {
        this.totalCherries = globalDataHolder.GetCherries();

    }
}
