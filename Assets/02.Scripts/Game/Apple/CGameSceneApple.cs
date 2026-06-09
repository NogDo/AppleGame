using UnityEngine;

public class CGameSceneApple : MonoBehaviour, IAppleSetting
{
    #region private 변수

    

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
        transform.localPosition = position;
        transform.localScale = new Vector3(size.x, size.x, size.x);
    }
}
