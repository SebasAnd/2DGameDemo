using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InventoryItem" && transform.childCount == 0 )
        {
            InventoryItem item = collision.gameObject.GetComponent<InventoryItem>();
            item.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //collision.transform.parent = null;
    }
}
