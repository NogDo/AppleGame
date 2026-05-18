using UnityEngine;

/// <summary>
/// MonoBehaviour를 상속받는 클래스에서 사용할 수 있는 싱글톤 패턴 기반 클래스
/// </summary>
public class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region 정적 변수
    private static T _instance;
    private static bool _isQuitting = false;
    #endregion

    #region 프로퍼티
    public static T Instance
    {
        get
        {
            if (_isQuitting)
            {
                return null;
            }

            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject oSingletonObject = new GameObject(typeof(T).Name);
                    _instance = oSingletonObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }
    #endregion

    #region 유니티 메시지
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
            _isQuitting = true;
        }
    }
    #endregion
}
