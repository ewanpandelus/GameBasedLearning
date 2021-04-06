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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSolve : MonoBehaviour
{
    [SerializeField] private GameObject complexityIllustrationPrefab;
    [SerializeField] private GameObject showGraphButton;
    private GameObject complexityIllustration;
    private RectTransform crossFade;
    private bool created = false;
    [SerializeField] private GameObject bubbleSort;
    private BubbleSort bubbleSortInstance;
    [SerializeField] private GameObject mergeSort;
    private MergeSort mergeSortInstance;
    private AnimatePoolBalls mergeSortAnimationInstance;

    void Start()
    {
        if (bubbleSort)
        {
            bubbleSortInstance = bubbleSort.GetComponent<BubbleSort>();
        }
        if (mergeSort)
        {
            mergeSortInstance = mergeSort.GetComponent<MergeSort>();
            mergeSortAnimationInstance = mergeSort.gameObject.GetComponent<AnimatePoolBalls>();
        }
        crossFade = GameObject.Find("CrossFade").GetComponent<RectTransform>();
    }

    public void ShowGraphQueens()
    {
        if (!created)
        {
            CreateGraph(new Vector3(0f, 110f, 0f));
            Adjustments(2f, new Vector3(0, -200f, 0));
            created = true;
        }
    }

    public void ShowGraphBee()
    {
        if (!created)
        {
            CreateGraph(new Vector3(200, 40f, 0f));
            Adjustments(1.2f, new Vector3(-220f, 20f, 0));
            created = true;
        }
   }

   public void ShowGraphMergeSort()
    {
        if (!created)
        {
            CreateGraph(new Vector3(200, 40, 0));
            Adjustments(1.6f, new Vector3(-250f, 0, 0));
            mergeSortAnimationInstance.StopAllCoroutines();
            mergeSortAnimationInstance.ClearAnimationObjects();
            created = true;
            mergeSortInstance.Reset();
            mergeSortInstance.Solve();
        }
    }

   public void ShowGraphBubbleSort()
    {
        if (!created)
        {
            bubbleSortInstance.StopAllCoroutines();
            bubbleSortInstance.StopAnimating();
            CreateGraph(new Vector3(200, 40, 0));
            Adjustments(1.6f, new Vector3(-330f, 20f, 0));
            created = true;
            bubbleSortInstance.Reset();
            bubbleSortInstance.Solve();
        }
    }

    private void Adjustments(float scaleFactor, Vector3 moveByVector)
    {
        this.gameObject.transform.position += moveByVector;
        this.gameObject.transform.localScale /= scaleFactor;
    }

    private void CreateGraph(Vector3 position)
    {
        complexityIllustration = Instantiate(complexityIllustrationPrefab,position , Quaternion.identity);
        complexityIllustration.transform.localScale /= 2.9f;
        complexityIllustration.transform.SetParent(crossFade, false);
        ComplexityGraph complexityGraph = complexityIllustration.GetComponent<ComplexityGraph>();
        complexityIllustration.transform.SetSiblingIndex(1);
        complexityGraph.UpdateGraph();
    }

    public void HideGraphMergeSort()
    {
        mergeSortAnimationInstance.StopAllCoroutines();
        mergeSortAnimationInstance.ClearAnimationObjects();
        DestroyGraph();
        Adjustments(1/1.6f, new Vector3(250f, 0f, 0));
        mergeSortInstance.Reset();
        mergeSortInstance.Solve();
    }

    public void HideGraphBubbleSort()
    {
        DestroyGraph();
        Adjustments(1 / 1.6f, new Vector3(330f, -20f, 0));
        bubbleSortInstance.StopAllCoroutines();
        bubbleSortInstance.StopAnimating();
        bubbleSortInstance.Reset();
        bubbleSortInstance.Solve();
    }

    public void HideGraphQueen()
    {
        DestroyGraph();
        Adjustments(1/2f, new Vector3(0, 200f, 0));
    }

    public void HideGraphBee()
    {
        DestroyGraph();
        Adjustments(1/1.2f, new Vector3(220f, -20f, 0));
    }

    private void DestroyGraph()
    {
        complexityIllustration = GameObject.FindGameObjectWithTag("Graph");
        created = false;
        Destroy(complexityIllustration);

    }

    public bool GetCreated()
    {
        return this.created;
    }
}
