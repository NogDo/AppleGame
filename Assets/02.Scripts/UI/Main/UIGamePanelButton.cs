using UnityEngine;
using UnityEngine.UI;

public class UIGamePanelButton : MonoBehaviour
{
    #region private 변수

    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnRetry;

    #endregion

    private void Start()
    {
        btnHome.onClick.AddListener(() =>
        {
            GameManager.Instance.ReturnMain();
        });

        btnRetry.onClick.AddListener(() =>
        {
            GameManager.Instance.Retry();
        });
    }
}
