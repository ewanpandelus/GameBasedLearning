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

    public void ShowGraphSolve()
    {
        Vector3 posUpRight = new Vector3(165f, 50f, 0);
        complexityIllustration = Instantiate(complexityIllustrationPrefab, gameObject.transform.position+posUpRight,Quaternion.identity);
        complexityIllustration.transform.SetParent(crossFade,false);
        complexityIllustration.transform.localScale /= 2.7f;
        this.gameObject.transform.localScale /= 1.2f;
        this.gameObject.transform.position += new Vector3(-3.7f,0.6f, 0);
    }
    public void HideGraphSolve()
    {
        complexityIllustration = GameObject.FindGameObjectWithTag("Graph");
        Destroy(complexityIllustration);
        this.gameObject.transform.localScale *= 1.2f;
        this.gameObject.transform.position -= new Vector3(-3.7f, 0.6f, 0);
    }

    void Update()
    {
        
    }
}
