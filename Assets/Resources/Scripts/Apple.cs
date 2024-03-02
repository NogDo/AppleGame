using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private int nAppleNumber;
    private Vector2 vector2_ApplePos;

    public void SetAppleNumber(int number)
    {
        nAppleNumber = number;
    }

    public int GetAppleNumber()
    {
        return nAppleNumber;
    }

    public void SetApplePos()
    {
        vector2_ApplePos = gameObject.transform.position;
    }

    public Vector2 GetApplePos()
    {
        return vector2_ApplePos;
    }
}
