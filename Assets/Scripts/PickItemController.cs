using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItemController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject inventoryRefference;
    [SerializeField] private GameObject parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager gameManager = GameManager.instance;
            InventoryItem inventoryItem = inventoryRefference.GetComponent<InventoryItem>();
            inventoryItem.DropRefference = this.gameObject;
            gameManager.AddItemToInventory(inventoryRefference);
            parent.SetActive(false);
            gameManager.ShowItemNotication(inventoryItem.Stats.name);

        }
    }
}
