using TMPro;
using UnityEngine;

public class UITimerTextControl : MonoBehaviour
{
    #region private 변수

    [SerializeField] private CTimerController timerController;

    private TextMeshProUGUI _tmpTime;

    #endregion

    private void Awake()
    {
        _tmpTime = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        timerController.OnValueChanged += ChangeText;
    }

    private void OnDestroy()
    {
        timerController.OnValueChanged -= ChangeText;
    }

    /// <summary>
    /// 현재 시간에 따라 텍스트를 변경한다.
    /// </summary>
    /// <param name="time">현재 시간</param>
    private void ChangeText(float time)
    {
        int ceilTime = Mathf.CeilToInt(time);

        _tmpTime.text = ceilTime.ToString();
    }
}
