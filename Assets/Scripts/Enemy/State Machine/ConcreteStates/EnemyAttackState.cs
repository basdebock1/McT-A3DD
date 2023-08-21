using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Transform _playerTransform;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.Attack();

        if (!enemy.IsWithinStrikingDistance)
        {
            enemyStateMachine.ChangeState(enemy.WanderState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
