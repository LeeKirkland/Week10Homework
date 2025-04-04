using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRPG : MonoBehaviour
{
    public float health = 100f;
    public float attackDamage = 5f;
    public float attackInterval = 1f;
    public float damageReductionFactor = 1f;

    public int maxAmmo = 10;  
    public int currentAmmo;    
    public float projectileDamage = 20f;  
    public float projectileSpeed = 10f;  

    private float timer;
    private bool isAttackReady = true;

    public Image attackReadyImage;

    public TextMeshProUGUI healthText;

    public GameObject projectilePrefab; 
    public Transform projectileSpawnPoint;  

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo; //starting ammo amount
    }

    // Update is called once per frame
    void Update()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString("F0"); // Display health
        }

        if (isAttackReady == false)
        {
            timer += Time.deltaTime;

            if (timer >= attackInterval)
            {
                isAttackReady = true;
                attackReadyImage.gameObject.SetActive(isAttackReady);
                timer = 0f;
            }
        }

        if (Input.GetMouseButtonDown(0) && isAttackReady)
        {
            if (currentAmmo > 0)  // Checkint if there is ammo
            {
                ShootProjectile();
                currentAmmo--;
                isAttackReady = false;
                attackReadyImage.gameObject.SetActive(isAttackReady);
            }
        }
    }
    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);        //putting projectile at spawn point

        Projectile projScript = projectile.GetComponent<Projectile>();
        projScript.damage = projectileDamage;
        projScript.speed = projectileSpeed;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();    //making it move
        rb.velocity = transform.forward * projectileSpeed;
    }

    public void ReloadAmmo()
    {
        currentAmmo = maxAmmo; 
        Debug.Log("Ammo reloaded!");
    }

    public void Attack(BaseEnemy enemy)
    {
        enemy.TakeDamage(attackDamage);
        isAttackReady = false;
        attackReadyImage.gameObject.SetActive(isAttackReady);
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
