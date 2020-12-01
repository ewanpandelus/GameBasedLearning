using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    [HideInInspector]
    public Vector2Int mBoardPosition = Vector2Int.zero;
    [HideInInspector]
    public ChessBoard mBoard = null;
    [HideInInspector]
    public RectTransform mRectTransform = null;

    [HideInInspector]

    public void Setup(Vector2Int newBoardPosition, ChessBoard newBoard)
    {
        mBoardPosition = newBoardPosition;
        mBoard = newBoard;

        mRectTransform = GetComponent<RectTransform>();
    }
}
