using UnityEngine;

public class CDragController : MonoBehaviour
{
    #region private 변수

    // 드래그 위치
    private Vector2 _v2StartPosition;
    private Vector2 _v2EndPosition;

    // 메인 카메라
    private Camera _camMain;

    #endregion

    private void Awake()
    {
        _camMain = Camera.main;
    }

    private void Update()
    {
        // 드래그 시작
        if (Input.GetMouseButtonDown(0))
        {
            DragStart();
        }
        // 드래그
        else if (Input.GetMouseButton(0))
        {
            Drag();
        }
        // 드래그 종료
        else if (Input.GetMouseButtonUp(0))
        {
            DragEnd();
        }
    }

    /// <summary>
    /// 드래그 시작,
    /// 첫 터치 위치를 저장
    /// </summary>
    private void DragStart()
    {
        _v2StartPosition = GetWorldPosition();
        _v2EndPosition = GetWorldPosition();
    }

    /// <summary>
    /// 드래그
    /// </summary>
    private void Drag()
    {
        _v2EndPosition = GetWorldPosition();
    }

    /// <summary>
    /// 드래그 종료,
    /// 마지막 터치 위치를 저장 및 처리
    /// </summary>
    private void DragEnd()
    {
        _v2EndPosition = GetWorldPosition();
    }

    /// <summary>
    /// 마우스 좌표를 월드 좌표로 변환
    /// </summary>
    /// <returns></returns>
    private Vector2 GetWorldPosition()
    {
        return _camMain.ScreenToWorldPoint(Input.mousePosition);
    }
}
