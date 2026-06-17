using UnityEngine;

public class UITimerImageControl : MonoBehaviour
{
    #region private 변수
    
    [SerializeField] private CTimerController timerController;

    private RectTransform _rt;

    #endregion

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    private void Start()
    {
        timerController.OnValueChanged += ChangeImageSize;
    }

    private void OnDestroy()
    {
        if (timerController != null)
        {
            timerController.OnValueChanged -= ChangeImageSize;
        }
    }

    /// <summary>
    /// 남은 시간에 따라 이미지 크기를 변경한다
    /// </summary>
    /// <param name="time">남은 시간</param>
    private void ChangeImageSize(float time)
    {
        // 남은 시간 비율을 앵커에 적용
        Vector2 anchor = Vector2.one;
        anchor.x = time / GameRule.GameTime;

        _rt.anchorMax = anchor;

        // 조정한 anchor에 이미지를 딱맞게 하기 위해 offset 조정
        _rt.offsetMax = Vector2.zero;
    }
}
