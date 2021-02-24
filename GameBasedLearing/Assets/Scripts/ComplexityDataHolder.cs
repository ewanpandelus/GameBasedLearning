using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexityDataHolder: MonoBehaviour
{



    private List<float> TBPValueList = new List<float> { 0, 1, 1, 3, 12, 60, 360, 2520, 20160, 181440, 1814400,
            19958400, 239500800, 3113510400, 43589145600, 653837184000, 10461394944000, 177843714048000,
            3201186852864000, 60822550204416000, 1216451004088320000 };
    private List<float> NQueens = new List<float> { -1,-1,-1,-1,9,-1, 32, -1, 114, -1, 103,-1,262,-1,1900,-1, 10053, -1, 41300,-1, 199636 };
    private List<float> bubbleSort = new List<float> { 0,0, 4, 9, 16,25,36,49,64,81,100,121,144,169,196,225,256,289,324,361,400, };
    private List<float> mergeSort = new List<float> { 0, 0,2,5,8,12,16,20,24,29,33,38,43,48,53,59,64,69,75,81,86 };


    public List<float> GetTBPValueList(int n)
    {
        return this.TBPValueList.GetRange(0, n+1);
    }

    public List<float> GetNQueensValueList(int n)
    {
        return this.NQueens.GetRange(0, n + 1);
    }
    public List<float> GetBubbleSortValueList(int n)
    {
        return this.bubbleSort.GetRange(0, n + 1);
    }
    public List<float> GetMergeSortValueList(int n)
    {
        return this.mergeSort.GetRange(0, n + 1);
    }
}
