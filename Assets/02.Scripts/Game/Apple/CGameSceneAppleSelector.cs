using System;
using System.Collections.Generic;
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

    private List<CGameSceneApple> _listSelectedApple = new List<CGameSceneApple>(GameRule.GameSceneAppleCount);

    #endregion

    private void Start()
    {
        arrApple = appleCreator.GetApples();

        dragController.OnDrag += Select;
        dragController.OnDragEnd += CheckScore;

        GameManager.Instance.OnGameStart += OnEnableAppleSelector;

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        dragController.OnDrag -= Select;
        dragController.OnDragEnd -= CheckScore;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStart -= OnEnableAppleSelector;
        }
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
        // 선택된 사과 리스트 Clear
        _listSelectedApple.Clear();

        int sum = 0;

        // 영역 안 사과들의 총합을 구함
        for (int i = 0; i < arrApple.Length; i++)
        {
            // 영역 안에 있는지 판단
            if (dragArea.Contains(arrApple[i].transform.localPosition))
            {
                // 활성화된 사과인지 판단
                if (arrApple[i].gameObject.activeSelf)
                {
                    sum += arrApple[i].Number;
                    _listSelectedApple.Add(arrApple[i]);
                }
            }
        }

        // 목표 숫자라면 비활성화
        if (sum == GameRule.TargetNumber)
        {
            for (int i = 0; i < _listSelectedApple.Count; i++)
            {
                _listSelectedApple[i].gameObject.SetActive(false);
            }

            // TODO : 나중에 Score에 점수 추가 로직 구현
        }

        // Material을 일반으로 되돌리기
        for (int i = 0; i < _listSelectedApple.Count; i++)
        {
            _listSelectedApple[i].SetMaterial(matDefault);
        }
    }

    /// <summary>
    /// AppleSelector를 활성화한다.
    /// </summary>
    private void OnEnableAppleSelector()
    {
        gameObject.SetActive(true);
    }
}