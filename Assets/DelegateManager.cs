using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//public class DamageEvent : EventArgs
//{
//    public float damage
//}


public class DelegateManager : MonoBehaviour
{
    public static DelegateManager Instance
    {
        get;
        set;
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Action GetDamageOperate;

    public void GetDamageOperation()
    {
        GetDamageOperate?.Invoke();
    }
}
