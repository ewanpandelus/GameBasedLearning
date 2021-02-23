using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComplexityQuestion : MonoBehaviour
{
    private string complexityAnswer = "";
    private GlobalDataHolder globalDataHolder;
    [SerializeField] private GameObject answerObject;
    private string explanationText = "";
    [SerializeField] private GameObject graph;
    private string assertRight = "That's correct!\n\n";
    private string assertWrong  = "That's not the right answer\n\n";
    private void Start()
    {
       
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        AssessComplexity();
        
    }
    private void AssessComplexity()
    {
        if (globalDataHolder.GetLevelToAssessComplexity().Contains("Merge"))
        {
            complexityAnswer = "O(nlogn)";
            explanationText = "This is because the recursive splitting\n of the array is O(logn), and the merging \n " +"step will have to compare at most the entire array of n elements.\n" +
                "when we combine these two we have O(logn)*O(n) giving us \n" +
                "O(nlogn)";
        }
        if(globalDataHolder.GetLevelToAssessComplexity().Contains("Bubble")){
            complexityAnswer = "O(n^2)";
            explanationText = "";
        }
        if (globalDataHolder.GetLevelToAssessComplexity().Contains("NQueens"))
        {
            complexityAnswer = "O(2^n)";
            explanationText = "";
        }
        if (globalDataHolder.GetLevelToAssessComplexity().Contains("TSP"))
        {
            complexityAnswer = "O(n!)";
            explanationText = "This is because using the naive algorithm we go through half of all the possible permutations of nodes. \n\n A route ABC is the same as CBA so we don't need to " +
                "go through every single combination of nodes.\n\n For example if we have a problem size of 4. Then the number of routes we need to explore before determining the shortetst route is\n" +
         "(4*3*2*1)/2 = 12.\n\n" +
         "Therefore this algorithm has average case complexity of O(n!)";
        }
    }
    public void AnswerQuestion()
    {
        if(this.name == complexityAnswer)
        {
            explanationText = assertRight + explanationText;
            answerObject.SetActive(true);
            answerObject.GetComponentInChildren<TextMeshProUGUI>().text = explanationText;
            graph.SetActive(false);
          
        }
        else
        {
            explanationText = assertWrong + explanationText;
            answerObject.SetActive(true);
            answerObject.GetComponentInChildren<TextMeshProUGUI>().text = explanationText;
            graph.SetActive(false);
      
        }
    }

  public void DisableAnswerText()
    {
        answerObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
        answerObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
        answerObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
    }
}
