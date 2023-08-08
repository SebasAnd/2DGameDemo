using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour
{
    private Transform currentParent;
    private Transform initialParent;
    public ItemTemplate Stats;
    public Image image;
    public bool isDropped;
    public bool hasChanged;

    public GameObject DropRefference;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = Stats.sprite;
        initialParent = transform.parent.transform;
        isDropped = false;
        hasChanged = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "InventorySlot" )
        {
            currentParent = collision.gameObject.transform;
        }
        if ( collision.gameObject.tag == "EquipmentSlot" && !hasChanged)
        {
            currentParent = collision.gameObject.transform;
            GameManager.instance.UpdatePlayerStats(Stats.maxHP, Stats.damage);
            hasChanged = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EquipmentSlot" && GameManager.instance.inventoryInterface.activeSelf &&hasChanged)
        {
            currentParent = initialParent;
            GameManager.instance.UpdatePlayerStats(-Stats.maxHP, -Stats.damage);
            hasChanged = false;
        }
    }
    public void OnMouseDrag()
    {
        transform.position = Input.mousePosition;
        isDropped = false;
    }
    public void OnMouseDrop()
    {
        if (currentParent)
        {
            transform.position = currentParent.position;
        }
        else {
            transform.position = initialParent.transform.position;
        }
        isDropped = true;

    }



}
