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
    private ChessBoard board;
    private bool safe = true;
    private Cell randomCell;
    private AudioManager audioManagement;

    void Start()
    {
        dynamicSolve = GameObject.Find("ChessBoard").GetComponent<DynamicSolve>();
        audioManagement = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        board = GameObject.Find("ChessBoard").GetComponent<ChessBoard>();
        randomCell = GameObject.Find("Cell(Clone)").GetComponent<Cell>();
        problemSize = board.GetProblemSize();
        mMovement = new Vector3Int(problemSize - 1, problemSize - 1, problemSize - 1);
        mRectTransform = GetComponent<RectTransform>();
        if (problemSize == 8)
        {
            mRectTransform.sizeDelta /= 1.45f;
        }
        if (dynamicSolve.GetCreated())
        {
            mRectTransform.sizeDelta /= 2;
        }
    }

    /// <summary>
    /// Finds the cells which are occupied by queens and 
    /// sets that they can be highlighted green
    /// </summary>
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

    /// <summary>
    ///  Shows the path from a queen, if a queen is in anothers 
    ///  path they will be added to an occupied list,
    ///  and the cell outline will be red, if no queen is in a cell within
    ///  their sight, then the cell will be outlined green.
    /// </summary>
    /// <param name="xDirection">XDirection of sight</param>
    /// <param name="yDirection">YDirection of sight</param>
    /// <param name="movement">Number of spaces to check</param>
    private void CreateCellPath(int xDirection, int yDirection, int movement)
    {
        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;
      
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

    /// <summary>
    /// Check cells for Queen's full range of sight
    /// </summary>
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

    
            CreateCellPath(0, 1, mMovement.y);
            CreateCellPath(0, -1, mMovement.y);


            CreateCellPath(1, 1, mMovement.z);
            CreateCellPath(-1, 1, mMovement.z);

            CreateCellPath(-1, -1, mMovement.z);
            CreateCellPath(1, -1, mMovement.z);
        }
  
    }

    /// <summary>
    /// Draw the outline for cells
    /// </summary>
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
    
    /// <summary>
    /// Delete outline of cells
    /// </summary>
    protected void ClearCells()
    {
        foreach (Cell cell in highlightedCells)
            cell.mOutlineImage.enabled = false;
        foreach (Cell cell in occupiedCells)
            cell.mOutlineImage.enabled = false;
       
        occupiedCells.Clear();
        highlightedCells.Clear();
    }

    /// <summary>
    /// Move queen to cell if unoccupied
    /// </summary>
    protected virtual void Move()
    {
        if (mCurrentCell)
        {
            mCurrentCell.SetOccupied(false);
        }

        if (mTargetCell)
        {
            mIsFirstMove = false;
            mCurrentCell = mTargetCell;
            mCurrentCell.SetOccupied(true);
            transform.position = mCurrentCell.transform.position;
        }
        audioManagement.Play("PlacePiece");
        mTargetCell = null;
    }

    #region Events
    /// <summary>
    /// Allows queen to be dragged, and shows pathing for queen
    /// </summary>
    /// <param name="eventData">Variable which holds pointer data(mouse)</param>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        CheckPathing();
        ShowCells();
    }

    /// <summary>
    /// Allows queens to have a target cell to be moved to if they are
    /// dragged over a cell.
    /// </summary>
    /// <param name="eventData">Variable which holds pointer data(mouse</param>
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        transform.position += (Vector3)eventData.delta;
        foreach (Cell cell in highlightedCells)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.mRectTransform, Input.mousePosition))
            {
                mTargetCell = cell;
                break;
            }
            mTargetCell = null;
        }
    }

    /// <summary>
    /// Moves queen after releasing mouse 
    /// to a new cell
    /// </summary>
    /// <param name="eventData">Variable which holds pointer data(mouse</param>
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        ClearCells();
        if (!mTargetCell&&mCurrentCell)
        {
            transform.position = mCurrentCell.gameObject.transform.position;
            return;
        }
        Move();
    }
    #endregion

    public bool GetSafe()
    {
        CheckPathing();
        return this.safe;
    }

}

