using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemy1Pref;
    [SerializeField]
    GameObject enemy2Pref;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnEnemy()
    {
        int count = 0;

        while (count < 30)
        {
            int rand = Random.Range(0, 2);
            int randx = Random.Range(-71, -31);
            int randz = Random.Range(-120, -70);
            var pos = new Vector3(randx, 0.3f, randz);

            if (rand == 0)
            {
                var enemy = Instantiate(enemy1Pref);
                enemy.transform.position = pos;

            }
            else
            {
                var enemy = Instantiate(enemy2Pref);
                enemy.transform.position = pos;
            }
            count++;
            yield return null;

        }
    }
}
