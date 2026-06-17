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
    private int[] _nArrNumber;

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
        NumberSetting();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStart += InActiveAppleCreator;
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
        // 사과 풀 생성
        _arrApplePool = new IAppleSetting[GameRule.MainSceneAppleCount];

        for (int i = 0; i < GameRule.MainSceneAppleCount; i++)
        {
            CMainSceneApple apple = Instantiate(applePrefab, rtApplePool);
            apple.Init();

            _arrApplePool[i] = apple;
        }

        // 사과 번호 배열 생성
        _nArrNumber = new int[GameRule.MainSceneAppleCount];
    }

    /// <summary>
    /// 사과를 GridLayout처럼 배치한다.
    /// </summary>
    private void ArrangeInGrid()
    {
        Canvas.ForceUpdateCanvases();

        Vector2 poolSize = rtApplePool.rect.size;

        float appleSize = Mathf.Min(poolSize.x / GameRule.MainSceneCols, poolSize.y / GameRule.MainSceneRows);

        float spacingX = (poolSize.x - GameRule.MainSceneCols * appleSize) / (GameRule.MainSceneCols + 1);
        float spacingY = (poolSize.y - GameRule.MainSceneRows * appleSize) / (GameRule.MainSceneRows + 1);

        Vector2 appleSizeVec = new Vector2(appleSize, appleSize);

        for (int i = 0; i < _nArrNumber.Length; i++)
        {
            int col = i % GameRule.MainSceneCols;
            int row = i / GameRule.MainSceneCols;

            float x = -poolSize.x / 2f + spacingX + col * (appleSize + spacingX) + appleSize / 2f;
            float y =  poolSize.y / 2f - spacingY - row * (appleSize + spacingY) - appleSize / 2f;

            _arrApplePool[i].SetLayout(new Vector2(x, y), appleSizeVec);
        }
    }

    /// <summary>
    /// 사과의 숫자를 세팅한다.
    /// </summary>
    private void NumberSetting()
    {
        // 숫자 셔플
        Shuffle();

        // 사과에 번호 할당
        for (int i = 0; i < _nArrNumber.Length; i++)
        {
            _arrApplePool[i].Numbering(_nArrNumber[i]);
        }
    }

    /// <summary>
    /// 번호 셔플
    /// </summary>
    private void Shuffle()
    {
        // 1 ~ 9까지 번호 할당
        int numberCount = GameRule.MainSceneAppleCount / 9;
        for (int i = 0; i < 9; i++)
        {
            int number = i * numberCount;

            for (int j = 0; j < numberCount; j++)
            {
                _nArrNumber[number + j] = i + 1;
            }
        }

        // 번호할당 후 남은 2개의 공간은 1 ~ 5하나, 10에서 첫번째 숫자를 뺀 수 하나를 넣는다.
        int randNum = Random.Range(1, 6);
        _nArrNumber[_nArrNumber.Length - 2] = randNum;
        _nArrNumber[_nArrNumber.Length - 1] = 10 - randNum;

        // 번호를 섞는다.
        // Fisher-Yates Shuffle 알고리즘
        for (int i = _nArrNumber.Length - 1; i > 0; i--)
        {
            int randIndex = Random.Range(0, i + 1);

            int temp = _nArrNumber[i];
            _nArrNumber[i] = _nArrNumber[randIndex];
            _nArrNumber[randIndex] = temp;
        }
    }

    /// <summary>
    /// AppleCreator를 비활성화한다.
    /// </summary>
    private void InActiveAppleCreator()
    {
        gameObject.SetActive(false);
    }
}
