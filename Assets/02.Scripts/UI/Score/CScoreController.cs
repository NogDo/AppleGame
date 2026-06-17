using TMPro;
using UnityEngine;

public class CScoreController : MonoBehaviour
{
    #region private 변수

    [SerializeField] private CGameSceneManager gameSceneManager;
    [SerializeField] private TextMeshProUGUI tmpScore;

    #endregion

    private void Start()
    {
        gameSceneManager.OnScoreChanged += ChangeScoreText;
    }

    /// <summary>
    /// 점수 텍스트를 변경
    /// </summary>
    /// <param name="score">현재 점수</param>
    private void ChangeScoreText(int score)
    {
        tmpScore.text = score.ToString();
    }
}
