using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject sceneEnemies;
    [SerializeField] private GameObject origin;
    public GameObject consumableItemsContainer;
    public GameObject[] generalItems;



    private void Awake()
    {
        if (instance == null)
            instance = this;
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
}
