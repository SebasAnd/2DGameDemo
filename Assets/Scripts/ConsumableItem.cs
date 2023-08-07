using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : MonoBehaviour
{
    [SerializeField] private float healthContent;
    [SerializeField] private float damageContent;
    [SerializeField] private GameObject item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (healthContent > 0 && damageContent > 0)
            {
                player.AddHealth(healthContent);
                player.AddDamage(damageContent);
                Destroy(this.gameObject);
                Destroy(item);
            }
            else {
                if (player.ReturnHealth("current") < player.ReturnHealth("max") && healthContent > 0)
                {
                    player.AddHealth(healthContent);
                    Destroy(this.gameObject);
                    Destroy(item);

                }
                else
                {
                    if (healthContent <= 0)
                    {
                        player.AddDamage(damageContent);
                        Destroy(this.gameObject);
                        Destroy(item);
                    }
                }
            }
            
            
        }
    }
}
