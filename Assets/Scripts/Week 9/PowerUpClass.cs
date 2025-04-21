using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpClass : MonoBehaviour
{
    public float value;
    public float respawnTime = 5f; // Time in seconds before the power-up reappears

    private Collider powerUpCollider;
    private Renderer powerUpRenderer;

    private void Awake()
    {
        powerUpCollider = GetComponent<Collider>();
        powerUpRenderer = GetComponent<Renderer>();
    }

    public virtual void ApplyPowerUp(GameObject player)
    {
        Debug.Log("Power-up applied");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUp(other.gameObject);
            StartCoroutine(RespawnPowerUp());
        }
    }

    private IEnumerator RespawnPowerUp()
    {
        // Disable visual and collision
        powerUpRenderer.enabled = false;
        powerUpCollider.enabled = false;

        // Wait for respawn time
        yield return new WaitForSeconds(respawnTime);

        // Re-enable visual and collision
        powerUpRenderer.enabled = true;
        powerUpCollider.enabled = true;
    }
}
