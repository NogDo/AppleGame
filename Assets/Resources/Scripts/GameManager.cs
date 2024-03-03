using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("AppleNumber")]
    public GameObject       gameObject_Panel_AppleNumbers;
    public GameObject[]     gameObject_AppleNumbers;
    public Sprite[]         sprites_AppleNumber;

    [Header("Manager")]
    public UIManager        UIManager;
    public DragHandler      dragHandler;

    // Time관련 변수들
    private int             nTotalGameTime;
    private bool            isTimeOver;
    private float           fDeltaTime;

    // Score관련 변수들
    private int nScore;

    private void Update()
    {
        if (!isTimeOver)
        {
            if (UIManager.slider_Time.value > 0)
            {
                UIManager.slider_Time.value -= Time.deltaTime;

                fDeltaTime += Time.deltaTime;
                if (fDeltaTime >= 1.0)
                {
                    fDeltaTime = 0;
                    UIManager.ChageTime();
                }
            }
            else
            {
                TimeOver();
            }
        }
    }

    public void InitGameManager()
    {
        // Time관련 변수들 초기화
        nTotalGameTime = 10;
        isTimeOver = false;
        fDeltaTime = 1.0f;

        // Score 초기화
        nScore = 0;

        // Manager 초기화 & 사과가 랜덤으로 배치
        dragHandler.InitDragHandler();
        UIManager.InitUIManager(nTotalGameTime);
        ArrangeAppleRandom();
    }

    public void ArrangeAppleRandom()
    {
        int[] intArr_AppleNumberCount = { 18, 18, 18, 18, 18, 18, 18, 18, 18 };

        for (int i = 0; i < gameObject_AppleNumbers.Count(); i++)
        {
            if (gameObject_Panel_AppleNumbers.transform.GetChild(i).gameObject.activeSelf == false)
            {
                gameObject_Panel_AppleNumbers.transform.GetChild(i).gameObject.SetActive(true);
            }

            gameObject_AppleNumbers[i] = gameObject_Panel_AppleNumbers.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < gameObject_AppleNumbers.Count(); i++)
        {
            while (true)
            {
                int number = Random.Range(0, 9);

                if (intArr_AppleNumberCount[number] == 0)
                {
                    continue;
                }

                gameObject_AppleNumbers[i].GetComponent<Image>().sprite = sprites_AppleNumber[number];
                gameObject_AppleNumbers[i].GetComponent<Apple>().nApplNumber = number + 1;
                intArr_AppleNumberCount[number]--;
                break;
            }
        }
    }

    public void SumApples(List<GameObject> list_GameObject_Apples)
    {
        int sum = 0;

        foreach (GameObject appleNumber in list_GameObject_Apples)
        {
            sum += appleNumber.GetComponent<Apple>().nApplNumber;
        }

        if (sum == 10)
        {
            int nListCount = list_GameObject_Apples.Count();
            for (int i = 0; i < nListCount; i++)
            {
                list_GameObject_Apples[0].SetActive(false);
            }

            nScore += nListCount;
            UIManager.ChangeScore(nScore);
        }
    }

    public void TimeOver()
    {
        isTimeOver = true;
        dragHandler.SetIsPlaying(false);
        UIManager.SetResultScore(nScore);
        UIManager.DisplayResultWindow();
    }
}
