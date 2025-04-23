using System.Collections.Generic;
using UnityEngine;

public class AudioSceneCollection : MonoBehaviour
{
    public static AudioSceneCollection instance;
    public string sceneMusic;

    public Sound[] music;
    public Sound[] sfx;

    void Start()
    {
        instance = this;

        RemoveDuplicateAudioSources();

        AudioManager.instance.RemoveNonPermanentSounds();
        AudioManager.instance.AddBgmSounds(music);
        AudioManager.instance.AddSounds(sfx);

        if (music.Length == 1)
            AudioManager.instance.updateMusic(music[0].name);
        else if (!string.IsNullOrEmpty(sceneMusic))
            AudioManager.instance.updateMusic(sceneMusic);
    }

    void RemoveDuplicateAudioSources()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        var seen = new HashSet<string>();

        // Recorre en reversa para conservar la última instancia
        for (int i = allAudioSources.Length - 1; i >= 0; i--)
        {
            AudioSource source = allAudioSources[i];
            if (source.clip == null) continue;

            string key = $"{source.gameObject.GetInstanceID()}_{source.clip.name}";

            if (seen.Contains(key))
            {
                Destroy(source);
            }
            else
            {
                seen.Add(key);
            }
        }
    }
}
