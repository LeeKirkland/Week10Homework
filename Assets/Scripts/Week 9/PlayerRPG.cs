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
    public float projectileSpawnOffset = 1f;

 

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
        if (projectilePrefab != null)
        {
            Vector3 spawnPosition = transform.position + transform.forward * projectileSpawnOffset + Vector3.up * 1f;
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(transform.forward));
        }
    }
}
