using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerRPG : MonoBehaviour
{
    [Header("Stats")]
    public float health = 100f;
    public float attackDamage = 5f;
    public float attackInterval = 1f;
    public float damageReductionFactor = 1f;

    [Header("UI")]
    public Image attackReadyImage;
    public TMP_Text healthText;

    [Header("Projectile Attack")]
    public GameObject projectilePrefab;
    public float projectileSpawnOffset = 2f;

    [Header("Melee Attack")]
    public float meleeRange = 2f;
    public float meleeDamage = 10f;

    void Start()
    {
        UpdateHealthUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FireProjectile();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformMeleeAttack();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage / damageReductionFactor;
        health = Mathf.Clamp(health, 0, 100f);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString("0");
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab is not assigned!");
            return;
        }

        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found!");
            return;
        }

        Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * projectileSpawnOffset;
        Quaternion spawnRotation = Quaternion.LookRotation(mainCamera.transform.forward);

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, spawnRotation);
        Debug.Log("Projectile instantiated at: " + spawnPosition);
    }

    void PerformMeleeAttack()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * 1f;
        Vector3 direction = Camera.main.transform.forward;

        Debug.DrawRay(origin, direction * meleeRange, Color.red, 1f);

        if (Physics.Raycast(origin, direction, out hit, meleeRange))
        {
            Debug.Log("Melee attack hit: " + hit.collider.name);
            BaseEnemy enemy = hit.collider.GetComponentInParent<BaseEnemy>();
            if (enemy != null)
            {
                Debug.Log($"Dealing {meleeDamage} damage to {enemy.name}");
                enemy.TakeDamage(meleeDamage);
            }
        }
        else
        {
            Debug.Log("Melee attack missed.");
        }
    }
}
