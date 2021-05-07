using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    public MonsterState monsterState;

    //variables of monster Tr, player Tr, navMeshAgent, monsterAnimator
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    [SerializeField]
    float traceDist = 10f;   // distance of tracing
    [SerializeField]
    float attackDist; // distance of attack
    [SerializeField]
    float behaviorTime;


    private bool isDie = false; // whether monster is die or not
    private bool hasTarget = false;
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
        StartCoroutine(this.SelectTarget());
    }
    IEnumerator SelectTarget()
    {
        int rand;
        while (!isDie)
        {
            rand = Random.Range(0, 2);

            yield return new WaitForSeconds(0.2f);
            if (!hasTarget)
            {
                if (rand == 0)
                    playerTr ??= GameObject.FindWithTag("Player").GetComponent<Transform>();
                else
                    playerTr ??= GameObject.FindWithTag("Enemy1").GetComponent<Transform>();

                hasTarget = true;
            }
        }
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

                case MonsterState.attack:

                    nvAgent.isStopped = true;
                    print($"{this.gameObject.name} is attack");
                    animator.SetBool("IsAttack", true);
                    yield return new WaitForSeconds(1f);
                    animator.SetTrigger("IsIdle");
                    break;
                default:
                    animator.SetTrigger("IsIdle");
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
            float dist = Vector3.Distance(monsterTr.position, playerTr.position - new Vector3(0, playerTr.position.y, 0));

            rand = Random.Range(0, 10);
            if (dist <= attackDist && rand < 8)
            {
                hasTarget = true;
                monsterState = MonsterState.attack;
                yield return new WaitForSeconds(1f);

            }
            else if (dist <= traceDist && rand < 7)
            {
                hasTarget = true;
                monsterState = MonsterState.trace;
            }
            else if (rand < 8)
            {
                hasTarget = false;
                monsterState = MonsterState.roaming;
                behaviorTime = Random.Range(4, 7);

                yield return new WaitForSeconds(behaviorTime);

            }
            else
            {
                hasTarget = false;
                monsterState = MonsterState.idle;
                yield return new WaitForSeconds(3f);
            }
        }
    }


}
