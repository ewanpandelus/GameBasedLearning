using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Image mOutlineImage;
    [HideInInspector]
    public Vector2Int mBoardPosition = Vector2Int.zero;
    [HideInInspector]
    public ChessBoard mBoard = null;
    [HideInInspector]
    public QueenPiece mCurrentPiece = null;
    [HideInInspector]
    public RectTransform mRectTransform = null;

    [HideInInspector]

    public void Setup(Vector2Int newBoardPosition, ChessBoard newBoard)
    {
        mOutlineImage.enabled = false;
        mBoardPosition = newBoardPosition;
        mBoard = newBoard;

        mRectTransform = GetComponent<RectTransform>();
    }
}
