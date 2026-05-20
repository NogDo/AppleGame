using UnityEngine;

public interface IAppleSetting
{
    /// <summary>
    /// 사과에게 숫자를 부여한다.
    /// </summary>
    /// <param name="number">사과가 받을 숫자</param>
    public void Numbering(int number);

    /// <summary>
    /// 사과 이미지의 위치와 크기를 지정한다.
    /// </summary>
    /// <param name="position">위치</param>
    /// <param name="size">크기</param>
    public void SetLayout(Vector2 position, Vector2 size);
}