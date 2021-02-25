using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QueenSpawner : MonoBehaviour
{
    [SerializeField] private GameObject queenPiecePrefab;
    public int cellSize;
    [SerializeField] GameObject spawnPoint;
    private CanvasScaler canvasScaler;
    private Vector2 ScreenScale;
    private GameObject latestQueen;
    public void SpawnQueen() 
    {
       GameObject queen = (GameObject)Instantiate(queenPiecePrefab, spawnPoint.transform.position, transform.rotation);
       GameObject crossFade = GameObject.Find("CrossFade");
       queen.transform.SetParent(crossFade.transform);
       queen.transform.SetSiblingIndex(3);
       QueenScaleAdjustments(queen);
       latestQueen = queen;
    }
    private void QueenScaleAdjustments(GameObject queen)
    {
        RectTransform rectTransform = queen.GetComponent<RectTransform>();
        Vector2 scaleFactor = CalculateScreenScale(queen) / 2f;
        float width = Screen.width;
        float height = Screen.height;
        if (width / height > 1.65f)
        {
            scaleFactor += new Vector2(0f, 0.12f);
        }
        if (width / height > 1.99f)
        {
            scaleFactor += new Vector2(0f, 0.12f);
        }
        rectTransform.sizeDelta *= scaleFactor;
    }
    public void SpawnQueenOnBoard(Vector3 boardPosition)
    {
        GameObject queen = (GameObject)Instantiate(queenPiecePrefab, boardPosition, transform.rotation);
        GameObject chessBoard = GameObject.Find("ChessBoard");
        queen.transform.SetParent(chessBoard.transform);
        QueenScaleAdjustments(queen);
        latestQueen = queen;
    }
    public GameObject GetLatestQueen()
    {
        return this.latestQueen;
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
