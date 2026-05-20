using UnityEngine;
using UnityEngine.UI;

public class CMainSceneApple : MonoBehaviour, IAppleSetting
{
    #region private 변수

    private RectTransform _rt;
    private Image _img;

    #endregion

    #region public 변수

    /// <summary>
    /// 사과 숫자
    /// </summary>
    /// <value></value>
    public int Number { get; private set; }

    #endregion

    public void Numbering(int number)
    {
        Number = number;
    }

    public void SetLayout(Vector2 position, Vector2 size)
    {
        

        _rt.localPosition = position;
        _rt.sizeDelta = size;
    }

    /// <summary>
    /// 초기 설정
    /// </summary>
    public void Init()
    {
        _img = GetComponent<Image>();
        _rt = GetComponent<RectTransform>();
    }
}