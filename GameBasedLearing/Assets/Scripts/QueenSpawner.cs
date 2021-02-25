using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QueenSpawner : MonoBehaviour
{
    [SerializeField] private GameObject queenPiecePrefab;
    public int cellSize;
    private Cell randomCell;
    [SerializeField] GameObject spawnPoint;
    private CanvasScaler canvasScaler;
    private Vector2 ScreenScale;
    public void SpawnQueen()
    {
        randomCell = GameObject.Find("Cell(Clone)").GetComponent<Cell>();
        GameObject queen = (GameObject)Instantiate(queenPiecePrefab, spawnPoint.transform.position, transform.rotation);
        GameObject chessBoard = GameObject.Find("CrossFade");
        queen.transform.SetParent(chessBoard.transform);
        GameObject queenSpawnButton = GameObject.Find("SpawnQueenButton");
        RectTransform rectTransform = queen.GetComponent<RectTransform>();
        Vector2 scaleFactor = CalculateScreenScale(queen) / 2f;
        float width = Screen.width;
        float height = Screen.height;
        if (width/height> 1.65f)
        {
            scaleFactor += new Vector2(0f, 0.12f);
        }
        if (width / height > 1.99f)
        {
            scaleFactor += new Vector2(0f, 0.12f);
        }
        rectTransform.sizeDelta *= scaleFactor;
    }

    private Vector2 CalculateScreenScale(GameObject queen)
    {
        if(canvasScaler == null)
             {
            canvasScaler = queen.transform.parent.gameObject.GetComponentInParent<CanvasScaler>();
        }

        if (canvasScaler)
        {
            return new Vector2( Screen.width/canvasScaler.referenceResolution.x ,  Screen.height/canvasScaler.referenceResolution.y );

        }
        else
        {
            return Vector2.one;
        }
    }
}
