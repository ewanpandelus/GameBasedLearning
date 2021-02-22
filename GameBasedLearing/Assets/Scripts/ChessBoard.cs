using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum CellState
{
    None,
    Occupied,
    Free,
    OutOfBounds
}
public class ChessBoard : MonoBehaviour
{  [SerializeField]
    private GameObject mCellPrefab;



    [SerializeField] int problemSize;
    private Cell[,] mAllCells;
    private int compensation;


   
     void Awake()
    {
        mAllCells = new Cell[problemSize, problemSize];
    }
    public void Create()
    {
        #region Create
        if (problemSize == 4)
        { 

            compensation = 140;
            CreateSmallBoard();
            
        }
        else
        {
            CreateLargeBoard();
            compensation = 0;
        }
        #region Colour
        for (int x = 0; x < problemSize; x += 2)
        {
            for (int y = 0; y < problemSize; y++)
            {
                // Offset for every other line
                int offset = (y % 2 != 0) ? 0 : 1;
                int finalX = x + offset;

                // Color
                mAllCells[finalX, y].GetComponent<Image>().color = Color.white;
            }
        }
        #endregion
    }
    private void CreateSmallBoard()
    {
       for (int y = 0; y < problemSize; y++)
       {
           for (int x = 0; x < problemSize; x++)
           {
             CreateCell(130, x, y);
           }
       }
    }
    private void CreateLargeBoard()
    {
        for (int y = 0; y < problemSize; y++)
        {
            for (int x = 0; x < problemSize; x++)
            {
                CreateCell(100, x, y);
            }
        }
    }

    private void CreateCell(int sizeDelta,int x, int y)
    {
        GameObject newCell = Instantiate(mCellPrefab, transform);

        // Position
        RectTransform rectTransform = newCell.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(sizeDelta, sizeDelta);
        rectTransform.anchoredPosition = new Vector2(((x * rectTransform.sizeDelta.x)+compensation) + rectTransform.sizeDelta.x / 2, ((y * rectTransform.sizeDelta.y)+compensation) + rectTransform.sizeDelta.y / 2);

        // Setup
        mAllCells[x, y] = newCell.GetComponent<Cell>();
        mAllCells[x, y].Setup(new Vector2Int(x, y), this);
    }

    #endregion
    public CellState ValidateCell(int targetX, int targetY, QueenPiece checkingPiece)
    {
        // Bounds check
        if (targetX < 0 || targetX > problemSize-1)
            return CellState.OutOfBounds;

        if (targetY < 0 || targetY > problemSize-1)
            return CellState.OutOfBounds;

        // Get cell
        Cell targetCell = mAllCells[targetX, targetY];
        try
        {
            if (targetCell.mCurrentPiece != null)
            {
                return CellState.Occupied;
            }
        }
        catch
        {
            return CellState.Free;
        }
        // If the cell has a piece
        return CellState.Free;


    }
    public void SetProblemSize(int _problemSize)
    {
        this.problemSize = _problemSize;
    }
    public int GetProblemSize()
    {
        return this.problemSize;
    }
    public Cell GetCellAtXY(int x, int y)
    {
        return this.mAllCells[x, y];
    }
    public Cell[,]GetAllCells()
    {
        return this.mAllCells;
    }
    public void ClearCells()
    {
        foreach(Cell cell in mAllCells)
        {
            cell.SetOccupied(false);
        }
    }

}
