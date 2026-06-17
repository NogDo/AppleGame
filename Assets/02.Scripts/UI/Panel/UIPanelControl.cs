using UnityEngine;

public class UIPanelControl : MonoBehaviour
{
    #region private 변수

    [SerializeField] private GameObject oMainPanel;
    [SerializeField] private GameObject oGamePanel;

    #endregion

    private void Start()
    {
        GameManager.Instance.OnGameStart += OnGameStart;
        GameManager.Instance.OnGameEnd += OnGameEnd;
    }


    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStart -= OnGameStart;
            GameManager.Instance.OnGameEnd -= OnGameEnd;
        }
    }

    /// <summary>
    /// 게임이 시작됐을 때 패널 컨트롤
    /// </summary>
    private void OnGameStart()
    {
        oMainPanel.SetActive(false);
        oGamePanel.SetActive(true);
    }

    /// <summary>
    /// 게임이 끝났을 때 패널 컨트롤
    /// </summary>
    private void OnGameEnd()
    {
        
    }
}
