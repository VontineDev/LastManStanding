using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print($"Triggerd {other.name}");

        if (other.tag == "PUNCH")
        {
            print("gotHit");

        }

    }
    private void Start()
    {
        print("Cube Start");

    }
}
