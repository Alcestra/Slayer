using UnityEngine.AI;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask GroundLayer, PlayerLayer;

    //patrole

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;


    // stats

    public int health;

    public float sightRange, AttackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>(); 
    }

    private void Update()
    {
        // check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerLayer);



        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if(playerInSightRange && !playerInAttackRange) Chasing();
        if(playerInSightRange && playerInAttackRange) Attacking();

    }

    private void Patrolling()
    {
        if (!walkPointSet) searchWalkPoint();
        
        if(walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWAlkPoint = transform.position - walkPoint;

        //walkpoint reached
        if(distanceToWAlkPoint.magnitude <=1f)
            walkPointSet = false;
    }
    
    void searchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,0f,transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, transform.up, 2f, GroundLayer))
            walkPointSet = true;
    }

    private void Attacking()
    {
        //stop enemy from moving
        agent.SetDestination((Vector3)transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {

            /// attack code would go here with the animation



            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
    }


    public void TakeDMG(int dmg)
    {
        health -=  dmg;

        if(health <= 0) Invoke(nameof(DestroyEnemy),0.5f);
    }


    void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    //Debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
