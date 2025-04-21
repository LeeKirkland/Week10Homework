using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRPG : MonoBehaviour
{
    // Power-ups
    public float health = 100f;
    public float attackDamage = 10f;
    public float attackInterval = 1f;
    public float damageReductionFactor = 1f;

    // Projectile
    public int maxAmmo = 10;
    public int currentAmmo;
    public float projectileDamage = 20f;
    public float projectileSpeed = 10f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private float timer;
    private bool isAttackReady = true;

    public Image attackReadyImage;
    public TextMeshProUGUI healthText;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString("F0");
        }

        if (!isAttackReady)
        {
            timer += Time.deltaTime;

            if (timer >= attackInterval)
            {
                isAttackReady = true;
                attackReadyImage.gameObject.SetActive(true);
                timer = 0f;
            }
        }

        // Melee Attack
        if (Input.GetMouseButtonDown(0) && isAttackReady)
        {
            PerformMeleeAttack();
            isAttackReady = false;
            attackReadyImage.gameObject.SetActive(false);
        }

        // Projectile Attack
        if (Input.GetKeyDown(KeyCode.Q) && isAttackReady && currentAmmo > 0)
        {
            ShootProjectile();
            currentAmmo--;
            isAttackReady = false;
            attackReadyImage.gameObject.SetActive(false);
        }
    }

    void PerformMeleeAttack()
    {
        Debug.Log("Player performed a melee attack");

        RaycastHit hit;
        float meleeRange = 2f;
        if (Physics.Raycast(transform.position, transform.forward, out hit, meleeRange))
        {
            BaseEnemy enemy = hit.collider.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                Debug.Log($"Hit {enemy.name} with melee attack");
                enemy.TakeDamage(attackDamage);
            }
            else
            {
                Debug.Log("Melee hit something, but it wasn't an enemy");
            }
        }
        else
        {
            Debug.Log("No melee hit");
        }
    }

    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        Projectile projScript = projectile.GetComponent<Projectile>();
        projScript.damage = projectileDamage;
        projScript.speed = projectileSpeed;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
    }

    public void ReloadAmmo()
    {
        currentAmmo = maxAmmo;
        Debug.Log("Ammo reloaded!");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("YOU DIED");
        }
    }
}
