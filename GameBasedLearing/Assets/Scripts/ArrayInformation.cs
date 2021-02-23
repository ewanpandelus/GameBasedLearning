using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ArrayInformation : MonoBehaviour
{
     [SerializeField] private int level;
     [SerializeField] private int size;
     [SerializeField] private List<PoolBallHolder> associatedPoolBallHolders;
     [SerializeField] private List<ArrayInformation> associatedMergingArrays;
     private List<int> expectedArrayValues = new List<int>();
     private bool empty = true;
     private bool full = false;
     private List<bool> isArrayOccupied = new List<bool>(); 



    private void Start()
    {
     InitialiseArrayOccupied();
    }


    private void InitialiseArrayOccupied()
    {
        for (int i = 0; i < size; i++)
        {
            if (size == 8)
            {
                isArrayOccupied.Add(true);
            }
            else
            {
                isArrayOccupied.Add(false);
            }
        }
    }
    public bool CheckFinished()
    {
        if(level == 0)
        {
            if(full == true)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    
    public void UpdateIsArrayOccupied(int ballToUpdate,bool adding,int prevBallIndex)
    {
        if (adding)
        {
            isArrayOccupied[expectedArrayValues.IndexOf(ballToUpdate)] = adding;
            SetBallsOfExpectedArray(adding);
        }
        else
        {
            isArrayOccupied[prevBallIndex] = adding;
        }
    
        if (!isArrayOccupied.Contains(true))
        {
            empty = true;
        }
        else
        {
            empty = false;
        }

        if (!isArrayOccupied.Contains(false))
        {
            empty = false;
            full = true;
        }

    }
    public void SetBallsOfExpectedArray(bool belongsToArray)
    {
        foreach(int ballNumber in expectedArrayValues)
        {
            Ball ball = GameObject.Find(ballNumber.ToString()+"(Clone)").GetComponent<Ball>();
            ball.SetBelongsToArray(belongsToArray);
        }
    }
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
    public void SetAllOccupiedFalse()
    {
        isArrayOccupied.Clear();
        InitialiseArrayOccupied();
    }
    public void SetExpectedArrayValues(List<int> _expectedArrayValues)
    {
        expectedArrayValues = _expectedArrayValues;
    }
    public List<int> GetExpectedArrayValues()
    {
        return this.expectedArrayValues;
    }


    public bool GetFull()
    {
        return this.full;
    }
    public void SetFull(bool value)
    {
        this.full = value;
    }
    public int GetSize()
    {
        return this.size;
    }
    public List<PoolBallHolder> GetPoolBallHolders()
    {
        return this.associatedPoolBallHolders;
    }
    public List<ArrayInformation> GetAssociatedMergingArrays()
    {
        return this.associatedMergingArrays;
    }
}
