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
    }
}
