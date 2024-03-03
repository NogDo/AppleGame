using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private int             nAppleNumber;

    public int nApplNumber
    {
        set => nAppleNumber = value;
        get => nAppleNumber;
    }
}
