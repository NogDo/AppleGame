using TMPro;
using UnityEngine;

public class CGameSceneApple : MonoBehaviour, IAppleSetting
{
    #region private 변수

    private TextMeshPro _tmpNumber;
    private SpriteRenderer _spriteRenderer;

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

        _tmpNumber.text = Number.ToString();
    }

    public void SetLayout(Vector2 position, Vector2 size)
    {
        transform.localPosition = position;
        transform.localScale = new Vector3(size.x, size.x, size.x);
    }

    /// <summary>
    /// 초기 설정
    /// </summary>
    public void Init()
    {
        _tmpNumber = GetComponentInChildren<TextMeshPro>(true);
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Material을 적용한다.
    /// </summary>
    /// <param name="mat">적용할 Material</param>
    public void SetMaterial(Material mat)
    {
        if (_spriteRenderer.sharedMaterial == mat)
        {
            return;
        }

        _spriteRenderer.sharedMaterial = mat;
    }
}
