using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject sceneEnemies;
    [SerializeField] private GameObject origin;
    public GameObject consumableItemsContainer;
    [SerializeField] public GameObject inventoryInterface;
    [SerializeField] private Button inventoryCloseButton;
    [SerializeField] private PlayerController player;

    [SerializeField] private TMPro.TMP_Text damageInventoryIndicator;
    [SerializeField] private TMPro.TMP_Text healthInventoryIndicator;

    public GameObject[] generalItems;
    public GameObject[] inventorySlots;
    public GameObject[] equipmentSlots;

    [SerializeField] private GameObject itemNotification;
    [SerializeField] private TMPro.TMP_Text itemNotificationText;

    private float initialMaxHealthValue;
    private float initialMaxDamageValue;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        inventoryCloseButton.onClick.AddListener(ShowOrHideInventory);
        UpdateStats();
        itemNotification.SetActive(false);
        initialMaxDamageValue = player.damage;
        initialMaxHealthValue = player.maxHealth;
    }

    public void ShowOrHideInventory()
    {
        if (inventoryInterface.activeSelf)
        {
            inventoryInterface.SetActive(false);
        }
        else {
            inventoryInterface.SetActive(true);

        }
    }
    public void RefreshEnemies()
    {
        Destroy(sceneEnemies);
        GameObject newEnemies = Instantiate(enemies);
        sceneEnemies = newEnemies;

        for (int i = 0; i < sceneEnemies.transform.childCount; i++)
        {
            sceneEnemies.transform.GetChild(i).transform.GetChild(0).GetComponent<EnemyController>().attackArea.origin = origin;
        }

    }

    public void AddItemToInventory(GameObject gameObject)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].transform.childCount == 0)
            {
                GameObject newItem = Instantiate(gameObject, inventorySlots[i].transform);
                break;
            }
        }
    }

    public void UpdateStats()
    {
        damageInventoryIndicator.text = "+" + player.damage+" DMG";
        healthInventoryIndicator.text = player.currentHealth+"/"+player.maxHealth + " HP";
    }

    public void UpdatePlayerStats(float health,float damage)
    {
        player.maxHealth += health;
        player.AddHealth(0f);
        player.AddDamage(damage);
    }

    public void ShowItemNotication(string name)
    {
        itemNotificationText.text = name;
        itemNotification.SetActive(true);        
        StartCoroutine(ItemNotificationTime());
    }
    IEnumerator ItemNotificationTime()
    {
        yield return new WaitForSeconds(.5f);
        itemNotification.SetActive(false);
    }

    public void Update()
    {
        if(Input.GetKeyDown("i"))
        {
            ShowOrHideInventory();
        }
     
    }
}
