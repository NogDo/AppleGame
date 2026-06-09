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

    /// <summary>
    /// Apple Grid 정렬할 공간 위치값 설정
    /// </summary>
    private void SetAnchor()
    {
        Rect safeArea = Screen.safeArea;
        Camera mainCamera = Camera.main;

        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(safeArea.xMin, safeArea.yMin, -mainCamera.transform.position.z));
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(safeArea.xMax, safeArea.yMax, -mainCamera.transform.position.z));

        _v3GridLeftTop     = Vector3.Lerp(bottomLeft, topRight, 0.1f);
        _v3GridRightBottom = Vector3.Lerp(bottomLeft, topRight, 0.9f);
    }

    /// <summary>
    /// Apple 프리팹 및 번호 배열 생성
    /// </summary>
    private void SetPool()
    {
        // 사과 풀 생성
        _arrApplePool = new IAppleSetting[GameRule.MainSceneAppleCount];

        for (int i = 0; i < GameRule.MainSceneAppleCount; i++)
        {
            CGameSceneApple apple = Instantiate(applePrefab, transform);
            apple.Init();

            _arrApplePool[i] = apple;
        }

        // 사과 번호 배열 생성
        _nArrNumber = new int[GameRule.MainSceneAppleCount];
    }

    /// <summary>
    /// 사과 배치
    /// </summary>
    private void ArrangeInGrid()
    {
        
    }

    /// <summary>
    /// 사과 숫자 세팅
    /// </summary>
    private void NumberSetting()
    {
        // 숫자 셔플
        Shuffle();
    }

    /// <summary>
    /// 번호 셔플
    /// </summary>
    private void Shuffle()
    {
        
    }
}
