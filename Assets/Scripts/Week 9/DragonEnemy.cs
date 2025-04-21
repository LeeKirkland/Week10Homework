using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonEnemy : BaseEnemy
{
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip damageSound;
    public AudioClip attackSound;

    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    private NavMeshAgent agent;

    protected override void Start()
    {
        base.Start();

        agent = GetComponent<NavMeshAgent>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (agent != null)
        {
            agent.isStopped = false;
            agent.updateRotation = false;
        }

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }

        Debug.Log("RAHHHH I'M A DRAGON!");
    }

    protected override void Update()
    {
        base.Update();
        HandlePatrolling();
    }

    private void HandlePatrolling()
    {
        if (agent == null || patrolPoints == null || patrolPoints.Length == 0)
            return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex++;
            if (currentPatrolIndex >= patrolPoints.Length)
            {
                currentPatrolIndex = 0;
            }

            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    protected override void Attack()
    {
        base.Attack();
        Debug.Log(this.gameObject.name + " deals " + attackDamage + " damage to you!");
        PlayAttackSound();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Debug.Log($"Dragon took {damage} damage!");
        PlayDamageSound();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            PlayHitSound();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayAttackSound();
        }
    }

    private void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
            Debug.Log("Dragon hit sound played");
        }
    }

    private void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
            Debug.Log("Dragon damage sound played");
        }
    }

    private void PlayAttackSound()
    {
        if (audioSource != null && attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
            Debug.Log("Dragon attack sound played");
        }
    }
}
