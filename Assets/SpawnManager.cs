using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject Enemy1Prefab;
    [SerializeField]
    GameObject Enemy2Prefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());

    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count < 30)
        {
            var rand = Random.Range(0, 3);

            var randx = Random.Range(0, 30);
            var randz = Random.Range(0, 30);
            var pos = new Vector3(randx, 0.3f, randz);


            if (rand < 1)
            {
                var enemy = Instantiate(Enemy1Prefab);
                enemy.transform.position = pos;
            }
            else
            {
                var enemy = Instantiate(Enemy2Prefab);
                enemy.transform.position = pos;
            }
            count++;
            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
