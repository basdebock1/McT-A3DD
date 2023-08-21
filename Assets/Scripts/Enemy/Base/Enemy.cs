using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITrigger
{
    //3d nav
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    public bool WalkPointSet { get; set; }
    public float walkPointRange;

    public Animator animator;

    //tut
    [field: SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public Rigidbody RB { get; set; }
    public bool isFacingRight { get; set; }

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyWanderState WanderState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        WanderState = new EnemyWanderState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);

        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        RB = GetComponent<Rigidbody>();

        StateMachine.Initialize(WanderState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    public void MoveEnemy(Vector3 destination)
    {
        agent.SetDestination(destination);
        agent.Move(destination);
    }

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        if(CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        GameObject.Destroy(this);
    }
    public enum AnimationTriggerType
    {
        EnemyIdle,
        EnemyAttacking,
        EnemyDamaged,
        PlayFootstepSound
    }
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public void SetAggroedStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }
    public void SetStrikingDistance(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }

    public virtual void Attack()
    {
        Debug.Log("Attack from agent!");
    }
}
