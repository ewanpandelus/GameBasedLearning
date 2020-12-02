using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QueenPiece : EventTrigger
{
    [HideInInspector]
    public Color mColor = Color.clear;
    public bool mIsFirstMove = true;
    protected Cell mOriginalCell = null;
    protected Cell mCurrentCell = null;
    protected RectTransform mRectTransform = null;
    protected Cell mTargetCell = null;
    Vector3Int mMovement = new Vector3Int();
    protected List<Cell> mHighlightedCells = new List<Cell>();
    [SerializeField] Image image;
    private int problemSize;
    
    void Start()
    {

        problemSize = GameObject.Find("ChessBoard").GetComponent<ChessBoard>().GetProblemSize();
    

        mRectTransform = GetComponent<RectTransform>();
    }

    public virtual void Place(Cell newCell)
    {
        // Cell stuff
        mCurrentCell = newCell;
        mOriginalCell = newCell;
        mCurrentCell.mCurrentPiece = this;

        // Object stuff
        transform.position = newCell.transform.position;
        gameObject.SetActive(true);
    }
    private void AllCellPaths()
    {
        ChessBoard board = GameObject.Find("ChessBoard").GetComponent<ChessBoard>();
        foreach(Cell cell in board.GetAllCells())
        {
            if (cell) 
            {
                mHighlightedCells.Add(cell);
            }
           
        }
    }
    private void FindPossibleCells()
    {
        ChessBoard board = GameObject.Find("ChessBoard").GetComponent<ChessBoard>();
        foreach (Cell cell in board.GetAllCells())
        {
            for(int x =0; x < problemSize - 1; x++)
            {
                for(int y=0; y < problemSize - 1; y++)
                {
                    if (cell.GetOccupied() == false)
                    {
                        mHighlightedCells.Add(cell);
                    }
                }
            }
           

        }
    }
    private void CreateCellPath(int xDirection, int yDirection, int movement)
    {
        // Target position
        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        // Check each cell
        for (int i = 1; i <= movement; i++)
        {
            currentX += xDirection;
            currentY += yDirection;

            // Get the state of the target cell
            CellState cellState = CellState.None;
            cellState = mCurrentCell.mBoard.ValidateCell(currentX, currentY, this);


            // If the cell is not free, break
            if (cellState != CellState.Free)
            {
                break;
            }
               
            if (mCurrentCell.mBoard.GetAllCells()[currentX, currentY])
            {
                // Add to list
                mHighlightedCells.Add(mCurrentCell.mBoard.GetAllCells()[currentX, currentY]);
            }
           
        }
    }

    protected virtual void CheckPathing()
    {
        if (mIsFirstMove)
        {
            AllCellPaths();
        }
        else
        {
            FindPossibleCells();
        }
  
    }

    protected void ShowCells()
    {
        foreach (Cell cell in mHighlightedCells)
            cell.mOutlineImage.enabled = true;
    }

    protected void ClearCells()
    {
        foreach (Cell cell in mHighlightedCells)
            cell.mOutlineImage.enabled = false;

        mHighlightedCells.Clear();
    }
    protected virtual void Move()
    {
        // First move switch
        mIsFirstMove = false;



        // Clear current
        if (mCurrentCell)
        {
            mCurrentCell.SetOccupied(false);
        }


        // Switch cells
        mCurrentCell = mTargetCell;
        mCurrentCell.SetOccupied(true);

        // Move on board
        transform.position = mCurrentCell.transform.position;
        mTargetCell = null;
    }


    #region Events
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        // Test for cells
        CheckPathing();

        // Show valid cells
        ShowCells();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        // Follow pointer
        transform.position += (Vector3)eventData.delta;

        // Check for overlapping available squares
        foreach (Cell cell in mHighlightedCells)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.mRectTransform, Input.mousePosition))
            {
                // If the mouse is within a valid cell, get it, and break.
                mTargetCell = cell;
                break;
            }

            // If the mouse is not within any highlighted cell, we don't have a valid move.
            mTargetCell = null;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        // Hide
        ClearCells();

        // Return to original position
        if (!mTargetCell)
        {
            transform.position = mCurrentCell.gameObject.transform.position;
            return;
        }

        // Move to new cell
        Move();

       
    }
    #endregion
}

