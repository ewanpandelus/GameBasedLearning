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
    private string assertRight = "<color=green>That's correct!</color>\n\n";
    private string assertWrong  = "<color=red>That's not the right answer.</color>\n\n";
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
            explanationText = "This is because the recursive splitting\n of the array is O(logn), and at the merging \n " +
                "step will have to compare at most the entire array of n elements.\n\n" +
                "When we combine these we have O(logn)*O(n) giving us " +
                "<color=green>O(nlogn)</color>.\n\n Merge Sort also has a space complexity of O(n), this is " +
                "because in every recursive call you create an array (or 2)" +
                " for merging and they take no greater than O(n) space." +
                " When the merging is done, these arrays are deleted.\n\n" +
                "For further information, visit https://www.geeksforgeeks.org/merge-sort/";


        }
        if(globalDataHolder.GetLevelToAssessComplexity().Contains("Bubble")){
            complexityAnswer = "O(n^2)";
            explanationText = "Bubble Sort works by comparing elements side by side in a list, swapping the elements if the left side is greater than the right.\n\n" +
                " It is known as Bubble Sort, because with every complete " +
                "iteration the largest element in the given array, bubbles up towards the last place. " +
                "\n\nIf we have n elements in a list then we can expect this process to be repeated n-1 times." +
                "These rules of Bubble Sort give us average complexity of (n-1) + (n-2) + (n-3) + ..... + 3 + 2 + 1 = \n(n-1)/2 or <color=orange>O(n<sup>2</sup>)</color>." +
                "\n\nFor futher information, visit https://www.studytonight.com/data-structures/bubble-sort";
            
        }
        if (globalDataHolder.GetLevelToAssessComplexity().Contains("NQueens"))
        {
            complexityAnswer = "O(2^n)";
            explanationText = "The algorithm used in the graphical solving is the backtracking algorithm." +
                " This algorithm starts at one corner of the board and places a queen, it then looks at the possible" +
                " attacked squares of that queen and places a queen avoiding this, repeating until the algorithm can no longer find a safe space for " +
                "a queen or it completes." +
                " \n\nIn the case it cannot find a safe position for another queen, it will go back one step and change the position of that queen.\n\n" +
                "This algorithm is much faster than the brute force route and is therefore of complexity <color=red>O(2<sup>n</sup>)</color>." +
                "\n\n For further information, visit https://www.codesdope.com/blog/article/backtracking-explanation-and-n-queens-problem/";
        }
        if (globalDataHolder.GetLevelToAssessComplexity().Contains("TSP"))
        {
            complexityAnswer = "O(n!)";
            explanationText = "This is because using the naive/brute force algorithm we go " +
                "through half of all the possible permutations of nodes. \n A route" +
                " ABC is the same as CBA so we don't need to " +
                "go through every single combination of nodes.\n\n For example if we have " +
                "a problem size of 4, the number of routes we need to explore " +
                "before determining the shortest route is" +
         "(4*3*2*1)/2 = 12." +
         "Therefore this algorithm has average case complexity of <color=red>O(n!)</color>.\n\n" +
         "For further information, visit  https://en.wikipedia.org/wiki/Travelling_salesman_problem";
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
