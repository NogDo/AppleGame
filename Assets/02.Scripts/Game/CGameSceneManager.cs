using UnityEngine;

public class CGameSceneManager : MonoBehaviour
{
    #region private 변수

    [SerializeField] private GameObject[] oArrGameSceneObject;

    #endregion

    private void Start()
    {
        GameManager.Instance.OnGameStart += OnGameStart;
    }

    /// <summary>
    /// 게임이 시작됐을 때 게임씬 오브젝트들을 활성화
    /// </summary>
    private void OnGameStart()
    {
        for (int i = 0; i < oArrGameSceneObject.Length; i++)
        {
            oArrGameSceneObject[i].SetActive(true);
        }
    }
}
