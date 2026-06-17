using System;
using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class CDragController : MonoBehaviour
{
    #region public 변수

    public event Action<Rect> OnDrag;
    public event Action<Rect> OnDragEnd;

    #endregion

    #region private 변수

    [SerializeField] private SpriteRenderer sprDragArea;

    // 드래그 관련
    private Vector2 _v2StartPosition;
    private Vector2 _v2EndPosition;
    private Rect _rectDragArea;
    private int _nActiveFingerId = -1;

    // 메인 카메라
    private Camera _camMain;

    #endregion

    private void Awake()
    {
        _camMain = Camera.main;
    }

    private void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouch.EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        // 터치 가져오기
        var touches = EnhancedTouch.Touch.activeTouches;

        // 첫번재 터치만 유효하도록
        foreach (var touch in touches)
        {
            // 첫 터치만 등록
            if (_nActiveFingerId == -1 && touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                _nActiveFingerId = touch.finger.index;
                DragStart();
            }
            // 첫 터치만 드래그 처리
            else if (touch.finger.index == _nActiveFingerId)
            {
                // 드래그
                if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
                {
                    Drag();
                }
                // 드래그 종료
                else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
                {
                    DragEnd();

                    _nActiveFingerId = -1;
                }
            }
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

        sprDragArea.gameObject.SetActive(true);
    }

    /// <summary>
    /// 드래그
    /// </summary>
    private void Drag()
    {
        _v2EndPosition = GetWorldPosition();

        Vector2 min = Vector2.Min(_v2StartPosition, _v2EndPosition);
        Vector2 max = Vector2.Max(_v2StartPosition, _v2EndPosition);
        _rectDragArea = new Rect(min, max - min);

        UpdateDragAreaVisual(_rectDragArea);

        OnDrag?.Invoke(_rectDragArea);
    }

    /// <summary>
    /// 드래그 종료,
    /// 마지막 터치 위치를 저장 및 처리
    /// </summary>
    private void DragEnd()
    {
        _v2EndPosition = GetWorldPosition();

        Vector2 min = Vector2.Min(_v2StartPosition, _v2EndPosition);
        Vector2 max = Vector2.Max(_v2StartPosition, _v2EndPosition);
        _rectDragArea = new Rect(min, max - min);

        sprDragArea.gameObject.SetActive(false);
        sprDragArea.size = Vector2.zero;

        OnDragEnd?.Invoke(_rectDragArea);
    }

    /// <summary>
    /// 드래그 영역 SpriteRenderer를 Rect에 맞게 업데이트
    /// </summary>
    /// <param name="rect">Drag 영역 Rect</param>
    private void UpdateDragAreaVisual(Rect rect)
    {
        sprDragArea.transform.position = rect.center;
        sprDragArea.size = new Vector2(rect.width, rect.height);
    }

    /// <summary>
    /// 포인터 좌표를 월드 좌표로 변환
    /// </summary>
    private Vector2 GetWorldPosition()
    {
        return _camMain.ScreenToWorldPoint(Pointer.current.position.ReadValue());
    }
}
