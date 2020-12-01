using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoard : MonoBehaviour
{
    [HideInInspector] public GameObject mCellPrefab;

    
   
    [SerializeField] int problemSize;
    public Cell[,] mAllCells = new Cell[8, 8];
    private int compensation;

    // We create the board here, no surprise
    public void Create()
    {
        #region Create
        if (problemSize == 4)
        {
            CreateSmallBoard();
            compensation = 1;
        }
        else
        {
            CreateLargeBoard();
            compensation = 0;
        }
        #region Colour
        for (int x = compensation; x < problemSize+compensation; x += 2)
        {
            for (int y = compensation; y < problemSize+compensation; y++)
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
       for (int y = 1; y < problemSize+1; y++)
       {
           for (int x = 1; x < problemSize+1; x++)
           {
             CreateCell(125, x, y);
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
        rectTransform.anchoredPosition = new Vector2((x * rectTransform.sizeDelta.x) + rectTransform.sizeDelta.x / 2, (y * rectTransform.sizeDelta.y) + rectTransform.sizeDelta.y / 2);

        // Setup
        mAllCells[x, y] = newCell.GetComponent<Cell>();
        mAllCells[x, y].Setup(new Vector2Int(x, y), this);
    }
        #endregion

       
    
}
