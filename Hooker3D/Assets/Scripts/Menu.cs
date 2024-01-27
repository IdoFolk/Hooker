using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider slider;
    void Start()
    {
        SetVolumeSlider();
    }

   public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SetVolumeSlider()
    {
        if (SoundManager.Instance.AudioMixer.GetFloat("Volume", out var value))
        {
            slider.value = value;
        }
    }
}
