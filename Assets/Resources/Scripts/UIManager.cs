using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Screen")]
    public GameObject           gameObject_MainScreen;
    public GameObject           gameObject_GameScreen;
    public GameObject           gameObject_ResultScreen;

    [Header("Time")]
    public Slider               slider_Time;
    public TextMeshProUGUI      textMeshPro_Time;

    [Header("Score")]
    public TextMeshProUGUI      textMeshPro_Score;
    public TextMeshProUGUI      textMeshPro_ResultScore;

    public void InitUIManager(int nTotalGameTime)
    {
        // UI 시간 및 텍스트 초기화
        slider_Time.value = nTotalGameTime;
        textMeshPro_Score.text = "Score : 0";
        textMeshPro_ResultScore.text = "0";
    }

    public void OnPressStartButton()
    {
        gameObject_MainScreen.SetActive(false);
        gameObject_GameScreen.SetActive(true);
    }

    public void OnPressHomeButton()
    {
        gameObject_MainScreen.SetActive(true);
        gameObject_GameScreen.SetActive(false);
    }

    public void OnPress_ResultScreen_RetryButton()
    {
        gameObject_ResultScreen.SetActive(false);
    }

    public void OnPree_ResultScreen_HomeButton()
    {
        gameObject_ResultScreen.SetActive(false);
        gameObject_GameScreen.SetActive(false);
        gameObject_MainScreen.SetActive(true);
    }

    public void ChageTime()
    {
        int nTime = (int)slider_Time.value;
        textMeshPro_Time.text = "Time : " + nTime.ToString();
    }

    public void ChangeScore(int nScore)
    {
        textMeshPro_Score.text = "Score : " + nScore.ToString();
    }

    public void DisplayResultWindow()
    {
        gameObject_ResultScreen.SetActive(true);
    }

    public void SetResultScore(int score)
    {
        textMeshPro_ResultScore.text = score.ToString();
    }
}
