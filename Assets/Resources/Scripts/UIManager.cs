using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public GameObject gameObject_MainScreen;
    public GameObject gameObject_GameScreen;
    public GameObject gameObject_Panel_AppleNumbers;
    public Image[] image_AppleNumbers;
    public Sprite[] sprites_AppleNumber;

    public void OnStartButtonPress()
    {
        gameObject_MainScreen.SetActive(false);
        gameObject_GameScreen.SetActive(true);
        ArrangeAppleRandom();
    }

    public void ArrangeAppleRandom()
    {
        int[] intArr_AppleNumberCount = { 18, 18, 18, 18, 18, 18, 18, 18, 18 };

        for(int i = 0; i < image_AppleNumbers.Count(); i++)
        {
            image_AppleNumbers[i] = gameObject_Panel_AppleNumbers.transform.GetChild(i).GetComponent<Image>();
        }

        for (int i = 0; i < image_AppleNumbers.Count(); i++)
        {
            while (true)
            {
                int number = Random.Range(0, 9);

                if (intArr_AppleNumberCount[number] == 0)
                {
                    continue;
                }

                image_AppleNumbers[i].sprite = sprites_AppleNumber[number];
                intArr_AppleNumberCount[number]--;
                break;
            }
        }
    }
}
