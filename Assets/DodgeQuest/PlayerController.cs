using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public float speed = 8f;

    //[SerializeField]
    private float hp = 100;
    // Start is called before the first frame update
    void Start()
    {
        //playerRigidbody = GetComponent<Rigidbody>();


        hp = 150f;


        DelegateManager.Instance.GetDamageOperate += GetDamage50;
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log($"Player OnTrigger {other.gameObject.name}");
        Debug.Log($"this is Trigger On PlayerController");

        if (other.gameObject.tag == "PUNCH")
        {
            GetDamage(10f);
        }
    }
    public void GetDamage50()
    {
        hp -= 50;

        if (hp < 0 || hp == 0)
        {
            Die();
        }
    }

    public float GetHp()
    {
        return hp;
    }

    public void GetDamage(float damage)
    {
        hp -= damage;

        if (hp < 0 || hp == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);

        FindObjectOfType<GameManager>().EndGame();
    }
}
