using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMainSceneAppleCreator : MonoBehaviour
{
    #region private 변수

    [SerializeField] private Canvas _canvas;
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
        SetPoolAndArrange();
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
        Vector2 screenSize = _canvas.pixelRect.size;

        Vector2 anchorMin = safeArea.position / screenSize;
        Vector2 anchorMax = (safeArea.position + safeArea.size) / screenSize;

        _rt.anchorMin = anchorMin;
        _rt.anchorMax = anchorMax;
        _rt.offsetMin = Vector2.zero;
        _rt.offsetMax = Vector2.zero;
    }

    /// <summary>
    /// Pool에 사과를 생성하고 배치한다.
    /// </summary>
    private void SetPoolAndArrange()
    {
        _arrApplePool = new IAppleSetting[_nPoolInitSize];
        
        for (int i = 0; i < _nPoolInitSize; i++)
        {
            CMainSceneApple apple = Instantiate(applePrefab, rtApplePool);
            apple.Init();
            apple.gameObject.SetActive(false);

            _arrApplePool[i] = apple;
        }
    }
}
