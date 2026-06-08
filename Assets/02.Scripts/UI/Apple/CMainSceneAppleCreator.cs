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
    private int _nCols = 20;
    private int _nRows = 10;

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

        float appleSize = Mathf.Min(poolSize.x / _nCols, poolSize.y / _nRows);

        float spacingX = (poolSize.x - _nCols * appleSize) / (_nCols + 1);
        float spacingY = (poolSize.y - _nRows * appleSize) / (_nRows + 1);

        Vector2 appleSizeVec = new Vector2(appleSize, appleSize);

        for (int i = 0; i < _nPoolInitSize; i++)
        {
            int col = i % _nCols;
            int row = i / _nCols;

            float x = -poolSize.x / 2f + spacingX + col * (appleSize + spacingX) + appleSize / 2f;
            float y =  poolSize.y / 2f - spacingY - row * (appleSize + spacingY) - appleSize / 2f;

            _arrApplePool[i].SetLayout(new Vector2(x, y), appleSizeVec);
        }
    }
}
