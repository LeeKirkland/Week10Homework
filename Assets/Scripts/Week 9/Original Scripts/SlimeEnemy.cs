using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : BaseEnemy
{
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip damageSound;
    public AudioClip attackSound;


    protected override void Start()
    {
        base.Start();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        Debug.Log("HeeHo I'm a slime!");
    }

    protected override void Update()
    {
        base.Update();
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
        PlayDamageSound(); // Plays sound only for melee damage
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayAttackSound();
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
