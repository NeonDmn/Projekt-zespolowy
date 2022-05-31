using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTask : UnitTask
{
    private ObjectHealth objectHealth;
    //private AudioManager audioManager;
    private float attackTimer;

    public AttackTask(Unit parent, ObjectHealth objectHealth) : base(parent)
    {
        this.objectHealth = objectHealth;
        AudioManager audioManager = owner.GetComponent<AudioManager>();
    }

    public override void OnTaskStart()
    {
        objectHealth.onObjectDie += EndTask;
        attackTimer = owner.unitStats.attackSpeed;
        owner.SetStoppingDistance(owner.unitStats.attackRange);
    }

    public override void OnTaskEnd()
    {
        owner.SetStoppingDistance(0);
        owner.Goto(owner.transform.position);
    }


    public override void Tick()
    {
        Collider2D collider2D = objectHealth.GetComponent<Collider2D>();
        attackTimer += Time.deltaTime;
        Vector3 targetPos = collider2D.ClosestPoint(owner.transform.position);
        owner.Goto(targetPos);
        var distance = (owner.transform.position - targetPos).magnitude;
        if (distance <= owner.unitStats.attackRange)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (attackTimer >= owner.unitStats.attackSpeed)
        {
            Debug.Log("Attack");
            objectHealth.DealDamage(owner,owner.unitStats.attackValue);
            owner.audioManager.attack.Play();
            attackTimer = 0;
        }
    }

    public void EndTask(ObjectHealth oh)
    {
        Debug.LogWarning("Task attack ended");
        owner.SwitchTask(new IdleTask(owner));
    }
}
