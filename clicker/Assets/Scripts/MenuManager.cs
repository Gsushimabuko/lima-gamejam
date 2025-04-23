using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject credis;
    public bool subMenuActive = false;

    private AudioManager audioManager;
    public GameObject BGMSlider;
    public GameObject SFXSlider;

    void Start()
    {
        audioManager = AudioManager.instance;
        BGMSlider.GetComponent<Slider>().value = AudioManager.bgMusicVolume;
        BGMSlider.GetComponent<Slider>().onValueChanged.AddListener(audioManager.updateMusicVolume);
        updateBGMusic(BGMSlider.GetComponent<Slider>().value);

        SFXSlider.GetComponent<Slider>().value = AudioManager.effectsMusicVolume;
        SFXSlider.GetComponent<Slider>().onValueChanged.AddListener(audioManager.updateSfxVolume);
        updateSFX(SFXSlider.GetComponent<Slider>().value);
    }

    public void updateBGMusic(float n)
    {
        print(n);
        audioManager.updateMusicVolume(n);
    }

    public void updateSFX(float n)
    {
        audioManager.updateSfxVolume(n);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
