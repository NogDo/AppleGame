using System;
using UnityEngine;

public class CGameSceneManager : MonoBehaviour
{
    #region public 변수

    public event Action<int> OnScoreChanged;

    #endregion

    #region private 변수

    // 게임씬 오브젝트
    [SerializeField] private GameObject[] oArrGameSceneObject;

    // 점수 관련
    private int _nCurrentScore;

    #endregion

    private void Start()
    {
        GameManager.Instance.OnGameStart += OnGameStart;
        GameManager.Instance.OnReturnMain += ReturnMain;

        Init();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStart -= OnGameStart;
            GameManager.Instance.OnReturnMain -= ReturnMain;
        }
    }

    /// <summary>
    /// 어플리케이션 처음 시작시 초기화
    /// </summary>
    private void Init()
    {
        for (int i = 0; i < oArrGameSceneObject.Length; i++)
        {
            oArrGameSceneObject[i].SetActive(false);
        }
    }

    /// <summary>
    /// 게임이 시작됐을 때 게임씬 오브젝트들을 활성화
    /// </summary>
    private void OnGameStart()
    {
        for (int i = 0; i < oArrGameSceneObject.Length; i++)
        {
            oArrGameSceneObject[i].SetActive(true);
        }

        _nCurrentScore = 0;
    }

    /// <summary>
    /// 게임 종료 후 메인 패널로 돌아갈때 게임씬 오브젝트들 비활성화
    /// </summary>
    private void ReturnMain()
    {
        for (int i = 0; i < oArrGameSceneObject.Length; i++)
        {
            oArrGameSceneObject[i].SetActive(false);
        }
    }

    /// <summary>
    /// 점수를 더함
    /// 점수 변경 이벤트를 실행
    /// </summary>
    /// <param name="score">얻은 점수</param>
    public void AddScore(int score)
    {
        _nCurrentScore += score;

        OnScoreChanged?.Invoke(_nCurrentScore);
    }
}
