using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Screen")]
    public GameObject gameObject_MainScreen;
    public GameObject gameObject_GameScreen;

    [Header("Time")]
    public Slider slider_Time;
    public TextMeshProUGUI textMeshPro_Time;

    [Header("Score")]
    public TextMeshProUGUI textMeshPro_Score;

    public void InitUIManager(int nTotalGameTime)
    {
        slider_Time.value = nTotalGameTime;
    }

    public void OnPressStartButton()
    {
        gameObject_MainScreen.SetActive(false);
        gameObject_GameScreen.SetActive(true);
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
}
