using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button mainBack;
    [SerializeField] private Button optionBack;
    [SerializeField] private EventSystem ES;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown ResDropdown;
    Resolution[] rs;
    private void Start()
    {
        rs = Screen.resolutions;
        ResDropdown.ClearOptions();
        List<string> list = new List<string>();
        int resIndex = 0;
        for (int i = 0; i < rs.Length; i++)
        {
            string option = "" + rs[i].width + " x " + rs[i].height;
            list.Add(option);

            if (rs[i].width == Screen.currentResolution.width &&
                rs[i].height == Screen.currentResolution.height)
            {
                resIndex = i;
            }
        }
        ResDropdown.AddOptions(list);   
        ResDropdown.value = resIndex;
        ResDropdown.RefreshShownValue();
    }
    public void startGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void applicationQuit()
    {
        applicationQuit();
    }
    public void openOption()
    {
        SceneManager.LoadScene(0);
    }
    public void closeOption()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
        mainBack.Select();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
    public void fullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }
    public void setReso(int reso)
    {
        Screen.SetResolution(rs[reso].width, rs[reso].height, Screen.fullScreen);
    }

}
