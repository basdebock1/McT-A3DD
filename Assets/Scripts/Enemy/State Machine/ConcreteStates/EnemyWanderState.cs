using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderState : EnemyState
{
    public EnemyWanderState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    private void Patroling()
    {
        if (!enemy.WalkPointSet) SearchWalkPoint();

        if (enemy.WalkPointSet)
            enemy.agent.SetDestination(enemy.walkPoint);

        Vector3 distanceToWalkPoint = enemy.transform.position - enemy.walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            enemy.WalkPointSet = false;
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.animator.Play("Base Layer.Idle");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Patroling();

        if (enemy.IsAggroed)
        {
            enemyStateMachine.ChangeState(enemy.ChaseState);
        }
        else if (enemy.IsWithinStrikingDistance)
        {
            enemyStateMachine.ChangeState(enemy.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-enemy.walkPointRange, enemy.walkPointRange);
        float randomX = Random.Range(-enemy.walkPointRange, enemy.walkPointRange);

        enemy.walkPoint = new Vector3(enemy.transform.position.x + randomX, enemy.transform.position.y, enemy.transform.position.z + randomZ);

        if (Physics.Raycast(enemy.walkPoint, -enemy.transform.up, 2f, enemy.whatIsGround))
        {
            enemy.WalkPointSet = true;
        }
    }
}
