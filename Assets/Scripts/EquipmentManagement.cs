using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManagement : MonoBehaviour
{
    GameManager gameManager;
    private bool avaliable = true;
    private void Start()
    {
        gameManager = GameManager.instance;        
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InventoryItem"  && gameManager.inventoryInterface.activeSelf && avaliable )
        {
            InventoryItem item = collision.gameObject.GetComponent<InventoryItem>();
            gameManager.UpdatePlayerStats(item.Stats.maxHP, item.Stats.damage);
            avaliable = false;

        } 

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InventoryItem" && gameManager.inventoryInterface.activeSelf )
        {
            InventoryItem item = collision.gameObject.GetComponent<InventoryItem>();
            gameManager.UpdatePlayerStats(-item.Stats.maxHP, -item.Stats.damage);
            avaliable = true;
        }
    }
}
