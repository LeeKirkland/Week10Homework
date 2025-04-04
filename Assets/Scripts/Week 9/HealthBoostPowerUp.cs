using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoostPowerUp : PowerUpClass
{
    public override void ApplyPowerUp(GameObject player)
    {
        PlayerRPG playerRPG = player.GetComponent<PlayerRPG>();     //getting component from player 

        if (playerRPG != null)
        {
            playerRPG.health += value;
            Debug.Log("Health Boosted: " + value);
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
