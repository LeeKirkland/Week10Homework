using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReducePowerUp : PowerUpClass
{
    public override void ApplyPowerUp(GameObject player)
    {
        PlayerRPG playerRPG = player.GetComponent<PlayerRPG>();

        if (playerRPG != null)
        {
            playerRPG.damageReductionFactor = Mathf.Clamp(playerRPG.damageReductionFactor * value, 0.1f, 1f);
            Debug.Log("Damage Reduction Applied: " + value);
        }
        else
        {
            Debug.Log("component not found on player");
        }
    }

// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
