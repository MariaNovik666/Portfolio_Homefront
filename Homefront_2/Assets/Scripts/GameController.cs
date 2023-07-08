using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GameController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        audioSource.volume = volumeSlider.value;

    }

    public void SetVolume()
    {
        audioSource.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void QuitGame()
    {
        Application.Quit();

        EditorApplication.isPlaying = false;
    }
}
