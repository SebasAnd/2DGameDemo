using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button MenuButton;

    [SerializeField] private Button playButton;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;

    [SerializeField] private TMPro.TMP_Dropdown quality;
    [SerializeField] private Slider resolution;
    [SerializeField] private TMPro.TMP_Text resolutionText;

    [SerializeField] private Slider volume;
    [SerializeField] private TMPro.TMP_Text volumeText;
    [SerializeField] private AudioSource music;


    [SerializeField] private bool useScape = true;

    private string[] resolutionOptions;


    void Start()
    {
        resolutionOptions = new string[] { "1920*1080", "800*600", "640*480" };
        
        AddListeners();
        if (PlayerPrefs.GetFloat("Resolution") != 0)
        {
            resolution.value = PlayerPrefs.GetFloat("Resolution");
        }
        quality.value = PlayerPrefs.GetInt("Quality");
        if (PlayerPrefs.GetFloat("Volume") == 0)
        {
            volume.value = 10;
        }
        else {
            volume.value = PlayerPrefs.GetFloat("Volume");
        }

        
        
    }

    void AddListeners()
    {
        playButton.onClick.AddListener(Resume);
        MenuButton.onClick.AddListener(Pause);
        quitButton.onClick.AddListener(Quit);

        quality.onValueChanged.AddListener(delegate {
            ValueChange(quality);
        });
        resolution.onValueChanged.AddListener(delegate {
            SliderResolucionChange(resolution);
        });
        volume.onValueChanged.AddListener(delegate {
            SliderVolumeChange(volume);
        });
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
        menuPanel.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
        menuPanel.SetActive(true);
    }
    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
                Application.OpenURL(webplayerQuitURL);
        #else
                Application.Quit();
        #endif
    }
    void SliderResolucionChange(Slider slider)
    {
        
        if (slider.value == 1)
        {
            Screen.SetResolution(640, 480, true);
            resolutionText.text = "640*480";
        }
        if (slider.value == 2)
        {
            Screen.SetResolution(800, 600, true);
            resolutionText.text = "800*600";
        }
        if (slider.value == 3)
        {
            Screen.SetResolution(1920, 1080, true);
            resolutionText.text = "1920*1080";
        }
        PlayerPrefs.SetFloat("Resolution", slider.value);
    }
    void SliderVolumeChange(Slider slider)
    {
        AudioListener.volume = slider.value/10;
        volumeText.text = ""+slider.value;
        PlayerPrefs.SetFloat("Volume", slider.value);
    }
    void ValueChange(TMPro.TMP_Dropdown dropdown)
    {

        if (dropdown.value == 0)
        {
            QualitySettings.SetQualityLevel(5, true);
        }
        if (dropdown.value == 1)
        {
            QualitySettings.SetQualityLevel(3, true);
        }
        if (dropdown.value == 2)
        {
            QualitySettings.SetQualityLevel(2, true);
        }
        if (dropdown.value == 3)
        {
            QualitySettings.SetQualityLevel(1, true);
        }
        PlayerPrefs.SetInt("Quality", dropdown.value);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && useScape)
        {
            if (Time.timeScale == 0)
            {
                Resume();
            }
            else {
                Pause();
            }
        }
    }
}
