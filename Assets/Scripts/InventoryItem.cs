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

    public GameObject DropRefference;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = Stats.sprite;
        initialParent = transform.parent.transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InventorySlot" )
        {
            currentParent = collision.gameObject.transform;
        }
    }
    public void OnMouseDrag()
    {
        transform.position = Input.mousePosition;
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
        
    }



}
