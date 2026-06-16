using UnityEngine;
using UnityEngine.UI;

public class UIMainPanelButton : MonoBehaviour
{
    #region private 변수

    [SerializeField] private Button btnStart;

    #endregion

    private void Start()
    {
        btnStart.onClick.AddListener(() => GameManager.Instance.GameStart());

        GameManager.Instance.OnGameStart += InActiveMainPanel;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStart -= InActiveMainPanel;
        }
    }

    /// <summary>
    /// 메인 패널 비활성화
    /// </summary>
    private void InActiveMainPanel()
    {
        gameObject.SetActive(false);
    }
}
