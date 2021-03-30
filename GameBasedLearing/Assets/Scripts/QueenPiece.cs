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
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        CheckPathing();
        ShowCells();
    }

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

