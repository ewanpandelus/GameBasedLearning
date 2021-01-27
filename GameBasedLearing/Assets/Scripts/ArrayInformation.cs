using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayInformation : MonoBehaviour
{
     [SerializeField] private int level;
     private List<int> expectedArrayValues = new List<int>();
     private bool empty = true;
  
    public bool GetEmpty()
    {
        return this.empty;
    }
    public void SetEmpty(bool _empty)
    {
        this.empty = _empty;
    }
    public int GetLevel()
    {
        return this.level;
    }
    public void SetExpectedArrayValues(List<int> _expectedArrayValues)
    {
        expectedArrayValues = _expectedArrayValues;
    }
    public  List<int> GetExpectedArrayValues()
    {
        return this.expectedArrayValues;
    }
    public void CreateExpectedArrayValues()
    {

    }
}
