using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexityDataHolder: MonoBehaviour
{



    private List<float> TBPValueList = new List<float> { 0, 1, 1, 3, 12, 60, 360, 2520, 20160, 181440, 1814400,
            19958400, 239500800, 3113510400, 43589145600, 653837184000, 10461394944000, 177843714048000,
            3201186852864000, 60822550204416000, 1216451004088320000 };
   

    public List<float> GetTBPValueList(int n)
    {
        return this.TBPValueList.GetRange(0, n+1);
    }
 
}
