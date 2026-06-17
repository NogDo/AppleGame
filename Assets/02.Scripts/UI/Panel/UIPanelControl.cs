using UnityEngine;

public class UIPanelControl : MonoBehaviour
{
    #region private 변수

    [SerializeField] private GameObject[] oArrMainPanel;
    [SerializeField] private GameObject[] oArrGamePanel;

    #endregion

    private void Start()
    {
        GameManager.Instance.OnGameStart += OnGameStart;
        GameManager.Instance.OnGameEnd += OnGameEnd;
        GameManager.Instance.OnReturnMain += OnReturnMain;

        Init();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStart -= OnGameStart;
            GameManager.Instance.OnGameEnd -= OnGameEnd;
            GameManager.Instance.OnReturnMain -= OnReturnMain;
        }
    }

    /// <summary>
    /// 어플리케이션 처음 실행시 초기화
    /// </summary>
    private void Init()
    {
        for (int i = 0; i < oArrMainPanel.Length;i ++)
        {
            oArrMainPanel[i].SetActive(true);
        }

        for (int i = 0; i < oArrGamePanel.Length; i++)
        {
            oArrGamePanel[i].SetActive(false);
        }
    }

    /// <summary>
    /// 게임이 시작됐을 때 패널 컨트롤
    /// </summary>
    private void OnGameStart()
    {
        for (int i = 0; i < oArrMainPanel.Length;i ++)
        {
            oArrMainPanel[i].SetActive(false);
        }

        for (int i = 0; i < oArrGamePanel.Length; i++)
        {
            oArrGamePanel[i].SetActive(true);
        }
    }

    /// <summary>
    /// 게임이 끝났을 때 패널 컨트롤
    /// </summary>
    private void OnGameEnd()
    {
        
    }

    /// <summary>
    /// 메인으로 돌아갔을 때 패널 컨트롤
    /// </summary>
    private void OnReturnMain()
    {
        for (int i = 0; i < oArrMainPanel.Length;i ++)
        {
            oArrMainPanel[i].SetActive(true);
        }

        for (int i = 0; i < oArrGamePanel.Length; i++)
        {
            oArrGamePanel[i].SetActive(false);
        }
    }
}
