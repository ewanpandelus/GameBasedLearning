using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QueenPiece : EventTrigger
{
    [HideInInspector]
    private DynamicSolve dynamicSolve;
    public Color mColor = Color.clear;
    public bool mIsFirstMove = true;
    protected Cell mOriginalCell = null;
    protected Cell mCurrentCell = null;
    protected RectTransform mRectTransform = null;
    protected Cell mTargetCell = null;
    Vector3Int mMovement = new Vector3Int();
    protected List<Cell> highlightedCells = new List<Cell>();
    protected List<Cell> occupiedCells = new List<Cell>();
    [SerializeField] Image image;
    private int problemSize;
    ChessBoard board;
    private bool safe = true;

    private AudioManager audioManagement;
    void Start()
    {
        dynamicSolve = GameObject.Find("ChessBoard").GetComponent<DynamicSolve>();
        audioManagement = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        board = GameObject.Find("ChessBoard").GetComponent<ChessBoard>();
        problemSize = board.GetProblemSize();
        mMovement = new Vector3Int(problemSize - 1, problemSize - 1, problemSize - 1);
        

        mRectTransform = GetComponent<RectTransform>();
        if (problemSize == 4)
        {
            mRectTransform.sizeDelta = new Vector2(100, 100);
        }
        else
        {
            mRectTransform.sizeDelta = new Vector2(75, 75);
        }
        if (dynamicSolve.GetCreated())
        {
            mRectTransform.sizeDelta /= 2;
        }
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
    
        foreach(Cell cell in board.GetAllCells())
        {
            if (cell) 
            {
                highlightedCells.Add(cell);
            }
           
        }
    }
    private void FindPossibleCells()
    {
       
        foreach (Cell cell in board.GetAllCells())
        {
            for(int x =0; x < problemSize - 1; x++)
            {
                for(int y=0; y < problemSize - 1; y++)
                {
                    if (cell.GetOccupied() == false)
                    {
                        highlightedCells.Add(cell);
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
            try
            {
                Cell cell = board.GetAllCells()[currentX, currentY];
                CellState cellState = CellState.None;
                cellState = mCurrentCell.mBoard.ValidateCell(currentX, currentY, this);
                if (cell.GetOccupied() == true)
                {
                    this.safe = false;
                    occupiedCells.Add(cell);
                }

                
                if (cell.GetOccupied() == false && cellState != CellState.OutOfBounds)
                {
                    highlightedCells.Add(mCurrentCell.mBoard.GetAllCells()[currentX, currentY]);
                }
            }
            catch
            {
                continue;
            }
            
          
        }
           
    }

    protected virtual void CheckPathing()
    {
        this.safe = true;
        if (mIsFirstMove)
        {
            FindPossibleCells();
        }
        else
        {
            CreateCellPath(1, 0, mMovement.x);
            CreateCellPath(-1, 0, mMovement.x);

            // Vertical 
            CreateCellPath(0, 1, mMovement.y);
            CreateCellPath(0, -1, mMovement.y);

            // Upper diagonal
            CreateCellPath(1, 1, mMovement.z);
            CreateCellPath(-1, 1, mMovement.z);

            // Lower diagonal
            CreateCellPath(-1, -1, mMovement.z);
            CreateCellPath(1, -1, mMovement.z);
        }
  
    }

    protected void ShowCells()
    {
        foreach (Cell cell in highlightedCells)
        {
            cell.mOutlineImage.enabled = true;
            if (!mIsFirstMove)
            {
                
                cell.SetColour(Color.green+ new Color(0,0,0,-0.5f));
            }
            else
            {
                cell.SetColour(Color.clear);
            }
        }
        foreach(Cell cell in occupiedCells)
        {
            cell.mOutlineImage.enabled = true;
            cell.SetColour(Color.red + new Color(0, 0, 0, -0.3f));
        }
        FindPossibleCells();
           
      
    }
    
    protected void ClearCells()
    {
        foreach (Cell cell in highlightedCells)
            cell.mOutlineImage.enabled = false;
        foreach (Cell cell in occupiedCells)
            cell.mOutlineImage.enabled = false;
       
        occupiedCells.Clear();
        highlightedCells.Clear();
    }
    protected virtual void Move()
    {
        // First move switch
       
        // Clear current
        if (mCurrentCell)
        {
            mCurrentCell.SetOccupied(false);
        }
        // Switch cells
        if (mTargetCell)
        {
            mIsFirstMove = false;
            mCurrentCell = mTargetCell;
            mCurrentCell.SetOccupied(true);
            transform.position = mCurrentCell.transform.position;
        }


        // Move on board
        audioManagement.Play("PlacePiece");
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
        foreach (Cell cell in highlightedCells)
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
        if (!mTargetCell&&mCurrentCell)
        {
            transform.position = mCurrentCell.gameObject.transform.position;
            return;
        }

        // Move to new cell
        Move();

       
    }
    #endregion
    public Cell GetCell()
    {
     return this.mCurrentCell;
  
    }
    public bool GetSafe()
    {
        CheckPathing();
        return this.safe;
    }
    public void SetSafe(bool _safe)
    {
        this.safe = _safe;
    }
}

