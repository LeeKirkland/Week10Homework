using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnemy : BaseEnemy
{
    protected override void Start()
    {
        base.Start();

        Debug.Log("RAHHHH I'M A DRAGON!");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        base.Attack();
        Debug.Log(this.gameObject.name + " deals " + attackDamage + " damage to you!");
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}
