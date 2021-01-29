using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBallHolder : MonoBehaviour
{   
    [SerializeField] private PoolBallHolder destinationPoolBallHolder; 
    [SerializeField] private ArrayInformation associatedArray;
    [SerializeField] private int arrayIndex;
    private Ball currentBall = null;
    private Ball initialBall = null;
    private RectTransform rectTransform = null;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public PoolBallHolder GetDestinationPoolBallHolder()
    {
        return this.destinationPoolBallHolder;
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
    public int GetIndex()
    {
        return this.arrayIndex;
    }
    public RectTransform GetRectTransform()
    {
        return this.rectTransform;
    }
}