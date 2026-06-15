using UnityEngine;

public class CGameSceneAppleSelector : MonoBehaviour
{
    #region private 변수

    [Header ("드래그 관련")]
    [SerializeField] private CGameSceneAppleCreator appleCreator;
    [SerializeField] private CDragController dragController;

    [Header ("Material")]
    [SerializeField] private Material matDefault;
    [SerializeField] private Material matOutline;

    private CGameSceneApple[] arrApple;

    #endregion

    private void Start()
    {
        arrApple = appleCreator.GetApples();

        dragController.OnDrag += Select;
        dragController.OnDragEnd += CheckScore;
    }

    /// <summary>
    /// 드래그 도중 영역안에 들어온 사과들을 선택한다.
    /// </summary>
    /// <param name="dragArea">드래그 영역</param>
    private void Select(Rect dragArea)
    {
        for (int i = 0; i < arrApple.Length; i++)
        {
            if (dragArea.Contains(arrApple[i].transform.localPosition))
            {
                arrApple[i].SetMaterial(matOutline);
            }
        }
    }

    /// <summary>
    /// 드래그를 마쳤을 때 영역안에 있는 사과들의 점수를 합쳐 비교한다.
    /// </summary>
    /// <param name="dragArea">드래그 영역</param>
    private void CheckScore(Rect dragArea)
    {
        for (int i = 0; i < arrApple.Length; i++)
        {
            if (dragArea.Contains(arrApple[i].transform.localPosition))
            {
                arrApple[i].gameObject.SetActive(false);
                arrApple[i].SetMaterial(matDefault);
            }
        }
    }
}
