using UnityEngine;

public class AudioSceneCollection : MonoBehaviour
{
    public static AudioSceneCollection instance;
    public string sceneMusic;

    public Sound[] music;
    public Sound[] sfx;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        AudioManager.instance.RemoveNonPermanentSounds();
        AudioManager.instance.AddBgmSounds(music);
        AudioManager.instance.AddSounds(sfx);
        if (music.Length == 1)
            AudioManager.instance.updateMusic(music[0].name);
        else if (sceneMusic != "")
            AudioManager.instance.updateMusic(sceneMusic);
    }
}