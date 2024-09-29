using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePoint : MonoBehaviour
{
    public static BasePoint Instance;

    private void Awake()
    {
        Instance = this;
    }
}
