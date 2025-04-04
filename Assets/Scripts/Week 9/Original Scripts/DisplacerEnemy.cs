using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplacerEnemy : BaseEnemy
{
    protected override void Start()
    {
        base.Start();

        Debug.Log("Grrr I'm beast!");
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
