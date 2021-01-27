using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBallHolder : MonoBehaviour
{
    [SerializeField] ArrayInformation associatedArray;
    private Ball currentBall = null;
    private Ball initialBall = null;
    private RectTransform rectTransform = null;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public ArrayInformation GetAssociatedArray()
    {
        return this.associatedArray;
    }
    public void SetInitialBall(Ball ball)
    {
        this.currentBall = ball;
    }
    public Ball GetInitialBall()
    {
        return this.initialBall;
    }
    public void SetCurrentBall(Ball ball)
    {
        this.currentBall = ball;
    }
    public Ball GetCurrentBall()
    {
        return this.currentBall;
    }
    public RectTransform GetRectTransform()
    {
        return this.rectTransform;
    }
}