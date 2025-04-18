using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpClass : MonoBehaviour
{
    public float value;
    public virtual void ApplyPowerUp(GameObject player)
    {
        Debug.Log("Power-up applied");
    }
    public void OnTriggerEnter(Collider other)     //checking if player triggers it then tells it what to do 
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUp(other.gameObject);
            Destroy(gameObject);
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