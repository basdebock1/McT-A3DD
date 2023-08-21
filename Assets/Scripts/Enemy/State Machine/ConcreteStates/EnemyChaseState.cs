using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
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
        ChasePlayer();
        if (enemy.IsWithinStrikingDistance)
        {
            enemyStateMachine.ChangeState(enemy.AttackState);
        }
        else if(!enemy.IsAggroed) {
            enemyStateMachine.ChangeState(enemy.WanderState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void ChasePlayer()
    {
        enemy.agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
}
