using UnityEngine;

public class CGameSceneAppleCreator : MonoBehaviour
{
    #region private 변수

    [SerializeField] private CGameSceneApple applePrefab;

    // 배치 관련
    private Vector3 _v3GridLeftTop;
    private Vector3 _v3GridRightBottom;

    // 사과 Pool
    private IAppleSetting[] _arrApplePool;
    private int[] _nArrNumber;

    #endregion

    private void Awake()
    {
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
        GameManager.Instance.OnGameStart += EnableAppleCreator;

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Apple Grid 정렬할 공간 위치값 설정
    /// </summary>
    private void SetAnchor()
    {
        Rect safeArea = Screen.safeArea;
        Camera mainCamera = Camera.main;

        Vector3 leftTop = mainCamera.ScreenToWorldPoint(new Vector3(safeArea.xMin, safeArea.yMax, -mainCamera.transform.position.z));
        Vector3 rightBottom = mainCamera.ScreenToWorldPoint(new Vector3(safeArea.xMax, safeArea.yMin, -mainCamera.transform.position.z));

        _v3GridLeftTop = Vector3.Lerp(leftTop, rightBottom, 0.1f);
        _v3GridRightBottom = Vector3.Lerp(leftTop, rightBottom, 0.9f);
    }

    /// <summary>
    /// Apple 프리팹 및 번호 배열 생성
    /// </summary>
    private void SetPool()
    {
        // 사과 풀 생성
        _arrApplePool = new IAppleSetting[GameRule.GameSceneAppleCount];

        for (int i = 0; i < GameRule.MainSceneAppleCount; i++)
        {
            CGameSceneApple apple = Instantiate(applePrefab, transform);
            apple.Init();

            _arrApplePool[i] = apple;
        }

        // 사과 번호 배열 생성
        _nArrNumber = new int[GameRule.GameSceneAppleCount];
    }

    /// <summary>
    /// 사과 배치
    /// </summary>
    private void ArrangeInGrid()
    {
        // 사과 Scale값 구하기
        float gridWidth = _v3GridRightBottom.x - _v3GridLeftTop.x;
        float gridHeight = _v3GridLeftTop.y - _v3GridRightBottom.y;

        float cellWidth = gridWidth / GameRule.GameSceneCols;
        float cellHeight = gridHeight / GameRule.GameSceneRows;

        CGameSceneApple apple = _arrApplePool[0] as CGameSceneApple;
        Vector2 nativeSize = apple.GetComponent<SpriteRenderer>().sprite.bounds.size;

        float scale = Mathf.Min(cellWidth / nativeSize.x, cellHeight / nativeSize.y);
        Vector2 appleScale = new Vector3(scale, scale);

        // 배치
        int index = 0;
        for (int row = 0; row < GameRule.GameSceneRows; row++)
        {
            for (int col = 0; col < GameRule.GameSceneCols; col++)
            {
                float x = _v3GridLeftTop.x + cellWidth * col + cellWidth * 0.5f;
                float y = _v3GridLeftTop.y - cellHeight * row - cellHeight * 0.5f;

                Vector2 position = new Vector2(x, y);

                _arrApplePool[index].SetLayout(position, appleScale);
                index++;
            }
        }
    }

    /// <summary>
    /// 사과 숫자 세팅
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
        int numberCount = GameRule.GameSceneAppleCount / 9;
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
    private void EnableAppleCreator()
    {
        gameObject.SetActive(true);
    }
}
