using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public AudioClip audioClip;

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Enemy2")
        {
            collider.GetComponent<AudioClip>();
        }    
    }

}
