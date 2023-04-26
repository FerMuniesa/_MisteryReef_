using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ENEMY_STATE : int
{
    MOVING, ATTACKING, PATROLLING, IDLE
}

public class EnemySenses : MonoBehaviour
{
    [SerializeField]
    private bool patrol;

    [SerializeField]
    private PatrolPoints patrolPoints;

    [SerializeField]
    private EnemyMovement enemyMovement;

    [SerializeField]
    private EnemyAttack enemyAttack;

    [SerializeField]
    private VisionRange visionRange;

    [SerializeField]
    private EnemyStats myStats;

    [SerializeField]
    private Vector2 pauseTime;

    [SerializeField]
    private Animator myAnim;

    private GameObject myEnemy;

    private bool canAttack = true;

    private float nextAttack = 0.0f;

    private bool enemyDeath = false;

    private ENEMY_STATE sTATE;

    private Vector3 destinationPosition;


    private void OnEnable()
    {
        EnemyActions.OnDeath += EnemyDeath;
    }


    private void OnDisable()
    {
        EnemyActions.OnDeath -= EnemyDeath;
    }

    private void EnemyDeath(GameObject go)
    {
        if (go.Equals(gameObject)) 
        {
            enemyDeath = true;
        }
    }

    private void Start()
    {
        if (patrol)
        {
            InitPatrol();
        }

        if (visionRange) visionRange.SetUp(myStats.rangeVision);
    }

    private void InitPause()
    {
        sTATE = ENEMY_STATE.IDLE;
        float time = UnityEngine.Random.Range(pauseTime.x, pauseTime.y);
        Invoke(nameof(InitPatrol), time);

    }

    private void InitPatrol()
    {
        sTATE = ENEMY_STATE.PATROLLING;
        var tr = patrolPoints.DamePuntoPatrulla();
        enemyMovement.SetDestination(tr, 0);
        destinationPosition = tr.position;
    }

    public void ISeeEnemy(GameObject gameObject)
    {
        if (!enemyDeath)
        {
            myEnemy = gameObject;
            MoveToEnemy(gameObject);
        }
    }

    private void MoveToEnemy(GameObject target)
    {
        if (!enemyDeath)
        {
            sTATE = ENEMY_STATE.MOVING;
            var haveDestination = enemyMovement.SetDestination(target.transform, myStats.attackRange);
            if (haveDestination)
            {
                destinationPosition = target.transform.position;
            }
        }
    }

    public void Attack()
    {
        if (!enemyDeath)
        {
            if (OnRange())
            {
                PlayerStats playerStats = myEnemy.GetComponent<PlayerStats>();
                playerStats.TakeDamage(myStats.dmgAttack);
            }
            Invoke(nameof(BackAttack), myStats.cd);
        }
    }

    private bool OnRange()
    {
        float distance = Vector3.Distance(transform.position, myEnemy.transform.position);
        return distance < myStats.attackRange;
    }

    /// <summary>
    /// Gestiona el estado de ataque del enemigo
    /// </summary>
    private void ManageAttackState()
    {
        if (myEnemy != null && canAttack && !enemyDeath && OnRange())
        {
            InitAttack();
        }
        else
        {
            InitMovement();
        }
    }

    /// <summary>
    /// Gestiona el movimiento del enemigo
    /// </summary>
    private void ManageMoving()
    {
        float distance = Vector3.Distance(transform.position, myEnemy.transform.position);
        if (distance < myStats.attackRange) 
        {
            sTATE = ENEMY_STATE.ATTACKING;
        }
        else
        {
            InitMovement();
        }
    }

    private void ManagePatroling()
    {
        if (enemyMovement.GetState().Equals(MOVEMENT_STATE.WAITING))
        {
            InitPause();
        }
    }

    /// <summary>
    /// Inicializa un movimiento hacia el enemigo
    /// </summary>
    private void InitMovement()
    {
        if (myEnemy != null && canAttack && destinationPosition != myEnemy.transform.position)
        {
            sTATE = ENEMY_STATE.MOVING;
            MoveToEnemy(myEnemy);
        }
    }

    /// <summary>
    /// Inicializa el ataque y lanza la animación de ataque
    /// </summary>
    private void InitAttack()
    {
        canAttack = false;
        enemyMovement.Stop();
        myAnim.SetTrigger("Attack");
        transform.LookAt(myEnemy.transform);
    }


    private void BackAttack()
    {
        canAttack = true;
    }

    private void Update()
    {
        switch (sTATE)
        {
            case ENEMY_STATE.MOVING:
                ManageMoving();
                break;
            case ENEMY_STATE.ATTACKING:
                ManageAttackState();
                break;
            case ENEMY_STATE.PATROLLING:
                ManagePatroling();
                break;
            case ENEMY_STATE.IDLE:
                break;
        }
    }
}
