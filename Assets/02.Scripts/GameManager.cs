using UnityEngine;

/// <summary>
/// 게임 전반을 관리하는 매니저 클래스
/// </summary>
public class GameManager : BaseSingleton<GameManager>
{
    #region 유니티 메시지
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
    #endregion
}
