///BSD 3 - Clause License

/// Copyright(c) 2021, ewanpandelus
///All rights reserved.

///Redistribution and use in source and binary forms, with or without
///modification, are permitted provided that the following conditions are met:

///1.Redistributions of source code must retain the above copyright notice, this
///list of conditions and the following disclaimer.

///2. Redistributions in binary form must reproduce the above copyright notice,
///this list of conditions and the following disclaimer in the documentation
///and/or other materials provided with the distribution.

///3. Neither the name of the copyright holder nor the names of its
///contributors may be used to endorse or promote products derived from
///this software without specific prior written permission.

///THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
///AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
///IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
///DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
///FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
///DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
///SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
///CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
///OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
///OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE

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

    /// <summary>
    /// This method sets up the arrays, the top array
    /// is full is array occupied is set as true, the others aren't so set 
    /// as false 
    /// </summary>
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
        if (level == 0)
        {
            if (full == true)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    /// <summary>This method updates the indexes of the array added to 
    /// and the array from which the ball was taken from </summary>
    /// <param name="ballToUpdate">the number of the ball in question</param>
    /// <param name="adding">whether the ball is being added or removed</param>
    /// <param name="previousArray">the index of the ball in the ball's previous array</param>
    public void UpdateIsArrayOccupied(int ballToUpdate, bool adding, int prevBallIndex)
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
        foreach (int ballNumber in expectedArrayValues)
        {
            Ball ball = GameObject.Find(ballNumber.ToString() + "(Clone)").GetComponent<Ball>();
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
