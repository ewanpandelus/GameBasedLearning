using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSolve : MonoBehaviour
{
   [SerializeField] private GameObject complexityIllustrationPrefab;
   [SerializeField] private GameObject showGraphButton;
   private GameObject complexityIllustration;
   private RectTransform crossFade;

    void Start()
    {
        crossFade = GameObject.Find("CrossFade").GetComponent<RectTransform>();
       
    }

    public void ShowGraphBeeLevel1()
    {
        CreateGraph();
        BeeAdjustments();
       

    }
    private void CreateGraph()
    {
        complexityIllustration = Instantiate(complexityIllustrationPrefab, new Vector3(200,40f,0f), Quaternion.identity);
        complexityIllustration.transform.localScale /= 2.7f;
        complexityIllustration.transform.SetParent(crossFade, false);
        ComplexityGraph complexityGraph = complexityIllustration.GetComponent<ComplexityGraph>();
        complexityGraph.UpdateGraph();
    }
    private void BeeAdjustments()
    {
        this.gameObject.transform.position += new Vector3(-200f, 20f, 0);
        this.gameObject.transform.localScale /= 1.2f;
    }
    public void ShowGraphBeeLevel2()
    {
        CreateGraph();
        BeeAdjustments();

        

    }
    public void HideGraphBeeLevel1()
    {

        DestroyGraph();
        this.gameObject.transform.localScale *= 1.2f;
        this.gameObject.transform.position -= new Vector3(-200f, 20f, 0);

    }
    private void DestroyGraph()
    {
        complexityIllustration = GameObject.FindGameObjectWithTag("Graph");
        Destroy(complexityIllustration);
    }
    public void HideGraphBeeLevel2()
    {
        DestroyGraph();
        this.gameObject.transform.localScale *= 1.2f;
        this.gameObject.transform.position -= new Vector3(-200f, 20f, 0);
    }

}
