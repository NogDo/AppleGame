using System;
using UnityEngine;

public class CTimerController : MonoBehaviour
{
    #region public 변수

    public event Action<float> OnValueChanged;

    #endregion

    #region private 변수

    // 시간 관련
    private float _fCurrentTime = 0;
    
    // 타이머 진행 관련
    private bool _isRunning = false;

    #endregion

    private void OnEnable()
    {
        StartTimer();
    }

    private void Update()
    {
        // 게임을 진행중이지 않으면 업데이트 X
        if (_isRunning == false)
        {
            return;
        }

        UpdateTime();
        EndCheck();
    }

    /// <summary>
    /// 게임 타이머를 시작한다.
    /// </summary>
    private void StartTimer()
    {
        _fCurrentTime = GameRule.GameTime;

        _isRunning = true;
    }

    /// <summary>
    /// 남은 시간 업데이트
    /// </summary>
    private void UpdateTime()
    {
        _fCurrentTime -= Time.deltaTime;
        // 음수값 처리
        _fCurrentTime = Mathf.Max(0, _fCurrentTime);

        OnValueChanged?.Invoke(_fCurrentTime);
    }

    /// <summary>
    /// 타이머가 종료됐는지 체크
    /// </summary>
    private void EndCheck()
    {   
        // 남은 시간이 0초 보다 많다면 return
        if (_fCurrentTime > 0.0f)
        {
            return;
        }

        _isRunning = false;
        GameManager.Instance.GameEnd();
    }
}
