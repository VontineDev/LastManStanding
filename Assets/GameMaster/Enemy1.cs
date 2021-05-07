using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum MonsterState { idle, roaming, trace, attack, die }
public class Enemy1 : MonoBehaviour
{
    public MonsterState monsterState;

    //variables of monster Tr, player Tr, navMeshAgent, monsterAnimator
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    [SerializeField]
    public float traceDist = 10f;   // distance of tracing
    [SerializeField]
    public float attackDist; // distance of attack
    [SerializeField]
    float behaviorTime;


    private bool isDie = false; // whether monster is die or not

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "PUNCH")
        {
            Die();
            //DelegateManager.Instance.GetDamageOperation();
            // GetDamage(50f);
        }
    }
    void Die()
    {
        isDie = true;
        animator.SetTrigger("IsDie");
    }
    // Start is called before the first frame update
    void Start()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();  //assgin monster Tr
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>(); //assgin player Tr, find with tag
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();  //assgin navMeshAgent
        nvAgent.speed = 2f;
        animator = GetComponent<Animator>(); //assign monster animator

        //this is disabled because should check distance first
        //nvAgent.destination = playerTr.position;

        StartCoroutine(this.CheckMonsterState());
        StartCoroutine(this.MonsterAction());
    }
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.isStopped = true;
                    print($"{this.gameObject.name} is idle");

                    animator.SetBool("IsTrace", false);
                    break;

                case MonsterState.roaming:
                    nvAgent.isStopped = true;
                    var rand1 = Random.Range(-10, 20);
                    var rand2 = Random.Range(-10, 20);
                    var dest = new Vector3(rand1, 0, rand2);
                    nvAgent.destination = dest;
                    nvAgent.isStopped = false;
                    print($"{this.gameObject.name} is roaming");

                    animator.SetBool("IsTrace", true);
                    animator.SetBool("IsAttack", false);
                    yield return new WaitForSeconds(behaviorTime);

                    break;
                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    this.transform.LookAt(playerTr.position);
                    print($"{this.gameObject.name} is trace");
                    nvAgent.isStopped = false;
                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", true);
                    break;
            }
            yield return null;
        }
    }

    IEnumerator CheckMonsterState()
    {
        int rand;

        while (!isDie)
        {
            //wait for 0.2sec
            yield return new WaitForSeconds(0.2f);
            //check distance
            float dist = Vector3.Distance(monsterTr.position, playerTr.position);
            rand = Random.Range(0, 10);
            if (dist <= traceDist && rand < 5)
            {
                monsterState = MonsterState.trace;
            }
            else if (rand < 8)
            {
                monsterState = MonsterState.roaming;
                behaviorTime = Random.Range(4, 7);

                yield return new WaitForSeconds(behaviorTime);
            }
            else
            {
                monsterState = MonsterState.idle;
                yield return new WaitForSeconds(3f);
            }
        }
    }

}
