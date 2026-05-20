using UnityEngine;

public class CMainSceneAppleCreator : MonoBehaviour
{
    #region private 변수

    [SerializeField] private IAppleSetting apple;

    private RectTransform _rt;

    #endregion

    private void Awake() 
    {
        _rt = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Rect safeArea = Screen.safeArea;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        Vector2 anchorMin = safeArea.position / screenSize;
        Vector2 anchorMax = (safeArea.position + safeArea.size) / screenSize;

        _rt.anchorMin = anchorMin;
        _rt.anchorMax = anchorMax;
        _rt.offsetMin = Vector2.zero;
        _rt.offsetMax = Vector2.zero;
    }
}
