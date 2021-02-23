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
    void Start()
    {
        if (bubbleSort)
        {
            bubbleSortInstance = bubbleSort.GetComponent<BubbleSort>();
        }
        crossFade = GameObject.Find("CrossFade").GetComponent<RectTransform>();

    }
    public void ShowGraphQueens()
    {
        if (!created)
        {
            CreateGraph(new Vector3(0f, 110f, 0f));
            QueenAdjustments();
            created = true;
            
        }
    }
    public void ShowGraphBee()
    {
        if (!created)
        {
            CreateGraph(new Vector3(200, 40f, 0f));
            BeeAdjustments();
            created = true;
        }
   }
   public void ShowGraphMergeSort()
    {
        if (!created)
        {
            CreateGraph(new Vector3(200, 40, 0));
            BubbleSortAdjustments();
            created = true;
        }
    }
   public void ShowGraphBubbleSort()
    {
        if (!created)
        {
            bubbleSortInstance.StopAllCoroutines();
            bubbleSortInstance.StopAnimating();
            CreateGraph(new Vector3(200, 40, 0));
            BubbleSortAdjustments();
            created = true;
            bubbleSortInstance.Reset();
            bubbleSortInstance.Solve();
        }
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
    private void BubbleSortAdjustments()
    {
        {
            this.gameObject.transform.position += new Vector3(-250f, 20f, 0);
            this.gameObject.transform.localScale /= 1.7f;
        }
    }
    private void QueenAdjustments()
    {
        this.gameObject.transform.position += new Vector3(0, -200f, 0);
        this.gameObject.transform.localScale /= 2f;
    }
    private void BeeAdjustments()
    {
        this.gameObject.transform.position += new Vector3(-200f, 20f, 0);
        this.gameObject.transform.localScale /= 1.2f;
    }
    public void HideGraphBubbleSort()
    {
        DestroyGraph();
        created = false;
        this.gameObject.transform.position -= new Vector3(-250f, 20f, 0);
        this.gameObject.transform.localScale *= 1.7f;
        bubbleSortInstance.StopAllCoroutines();
        bubbleSortInstance.StopAnimating();
        bubbleSortInstance.Reset();
        bubbleSortInstance.Solve();
    }
    public void HideGraphQueen()
    {
        DestroyGraph();
        created = false;
        this.gameObject.transform.localScale *= 2f;
        this.gameObject.transform.position -= new Vector3(0, -200f, 0);
    }
    public void HideGraphBee()
    {

        DestroyGraph();
        created = false;
        this.gameObject.transform.localScale *= 1.2f;
        this.gameObject.transform.position -= new Vector3(-200f, 20f, 0);

    }
    private void DestroyGraph()
    {
        complexityIllustration = GameObject.FindGameObjectWithTag("Graph");
        Destroy(complexityIllustration);

    }
    public bool GetCreated()
    {
        return this.created;
    }
}
