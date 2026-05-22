using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMainSceneAppleCreator : MonoBehaviour
{
    #region private 변수

    [SerializeField] private Canvas canvas;
    [SerializeField] private CMainSceneApple applePrefab;
    [SerializeField] private RectTransform rtApplePool;

    // UI 관련
    private RectTransform _rt;

    // Pool 관련
    private IAppleSetting[] _arrApplePool;
    private int _nPoolInitSize = 200;

    #endregion

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();

        SetAnchor();
        SetPool();
        ArrangeInGrid();
    }

    private void OnEnable()
    {
    }

    /// <summary>
    /// Anchor를 설정한다.
    /// </summary>
    private void SetAnchor()
    {
        Rect safeArea = Screen.safeArea;
        Vector2 screenSize = canvas.pixelRect.size;

        Vector2 anchorMin = safeArea.position / screenSize;
        Vector2 anchorMax = (safeArea.position + safeArea.size) / screenSize;

        _rt.anchorMin = anchorMin;
        _rt.anchorMax = anchorMax;
        _rt.offsetMin = Vector2.zero;
        _rt.offsetMax = Vector2.zero;
    }

    /// <summary>
    /// Pool에 사과를 생성한다.
    /// </summary>
    private void SetPool()
    {
        _arrApplePool = new IAppleSetting[_nPoolInitSize];

        for (int i = 0; i < _nPoolInitSize; i++)
        {
            CMainSceneApple apple = Instantiate(applePrefab, rtApplePool);
            apple.Init();
            // apple.gameObject.SetActive(false);

            _arrApplePool[i] = apple;
        }
    }

    /// <summary>
    /// 사과를 GridLayout처럼 배치한다.
    /// </summary>
    private void ArrangeInGrid()
    {
        Canvas.ForceUpdateCanvases();

        Vector2 poolSize = rtApplePool.rect.size;
        float aspectRatio = poolSize.x / poolSize.y;

        int bestCols = 1, bestRows = _nPoolInitSize;
        float bestDiff = float.MaxValue;

        for (int cols = 1; cols <= _nPoolInitSize; cols++)
        {
            if (_nPoolInitSize % cols != 0) 
            {
                continue;
            }

            int rows = _nPoolInitSize / cols;
            float diff = Mathf.Abs((float)cols / rows - aspectRatio);
            if (diff < bestDiff)
            {
                bestDiff = diff;
                bestCols = cols;
                bestRows = rows;
            }
        }

        float cellSize = Mathf.Min(poolSize.x / bestCols, poolSize.y / bestRows);
        Vector2 appleSize = new Vector2(cellSize, cellSize);

        float startX = -(bestCols * cellSize) / 2f + cellSize / 2f;
        float startY =  (bestRows * cellSize) / 2f - cellSize / 2f;

        for (int i = 0; i < _nPoolInitSize; i++)
        {
            int col = i % bestCols;
            int row = i / bestCols;

            float x = startX + col * cellSize;
            float y = startY - row * cellSize;

            _arrApplePool[i].SetLayout(new Vector2(x, y), appleSize);
        }
    }
}
