using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartInterfaceMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
