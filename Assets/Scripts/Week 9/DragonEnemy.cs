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
        agent.angularSpeed = 0; // We'll rotate manually

        // Ensure the dragon is standing upright (rotated like a billboard)
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // Offset the dragon so it's not halfway in the ground
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

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
        FaceMovementDirection(); // Rotate to face direction
    }

    private void HandlePatrolling()
    {
        if (agent == null || patrolPoints == null || patrolPoints.Length == 0)
            return;

        if (agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex++;
            if (currentPatrolIndex >= patrolPoints.Length)
            {
                currentPatrolIndex = 0;
            }

            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    private void FaceMovementDirection()
    {
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            Vector3 direction = agent.velocity.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

            // Apply Y-axis rotation only to keep upright
            float yRotation = targetRotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(90f, yRotation, 0f); // Keep X at 90 to stand up
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
        }
    }

    private void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    private void PlayAttackSound()
    {
        if (audioSource != null && attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }
}
