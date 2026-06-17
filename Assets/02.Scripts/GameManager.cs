using System;
using UnityEngine;

/// <summary>
/// 게임 전반을 관리하는 매니저 클래스
/// </summary>
public class GameManager : BaseSingleton<GameManager>
{
    #region public 변수

    public event Action OnGameStart;
    public event Action OnGameEnd;
    public event Action OnReturnMain;
    public event Action OnRetry;

    #endregion

    #region  private 변수



    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    /// <summary>
    /// 사과 게임 시작
    /// </summary>
    public void GameStart()
    {
        OnGameStart?.Invoke();
    }

    /// <summary>
    /// 사과 게임 종료
    /// </summary>
    public void GameEnd()
    {
        OnGameEnd?.Invoke();
    }

    /// <summary>
    /// 메인 패널로 복귀
    /// </summary>
    public void ReturnMain()
    {
        OnReturnMain?.Invoke();
    }

    /// <summary>
    /// 게임 다시시작
    /// </summary>
    public void Retry()
    {
        OnRetry?.Invoke();
    }
}