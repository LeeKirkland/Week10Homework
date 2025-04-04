using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoostPowerUp : PowerUpClass
{
    public override void ApplyPowerUp(GameObject player)
    {
        PlayerRPG playerRPG = player.GetComponent<PlayerRPG>();

        if (playerRPG != null)
        {
            playerRPG.attackDamage += value;
            Debug.Log("Attack Boosted: " + value);
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
