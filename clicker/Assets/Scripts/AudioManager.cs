using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<Sound> music = new List<Sound>();
    public List<Sound> sfx = new List<Sound>();
    public static AudioManager instance;
    public static float bgMusicVolume = .5f;
    public static float effectsMusicVolume = .5f;
    Sound actualBGM;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        InitializeSounds(music);
        InitializeSounds(sfx);
    }

    private void InitializeSounds(List<Sound> soundList)
    {
        foreach (Sound s in soundList)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        //PlayMusic("Menu Theme Test");
    }

    public void PlaySfx(string name)
    {
        Sound s = sfx.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("No se encontro el audio!");
            return;
        }
        s.source.Play();
    }

    public void PlayMusic(string name)
    {
        print("Play music: " + name);
        actualBGM = music.Find(bgmSound => bgmSound.name == name);
        if (actualBGM == null)
        {
            Debug.LogError("No se encontro el audio! " + name);
            return;
        }
        actualBGM.source.Play();
    }

    public void updateMusic(string newTheme)
    {
        if (actualBGM != null && actualBGM.name != newTheme)
        {
            actualBGM.source.Stop();
            PlayMusic(newTheme);
            updateMusicVolume(bgMusicVolume);
        }
        else if (actualBGM == null)
        {
            PlayMusic(newTheme);
            updateMusicVolume(bgMusicVolume);
        }
    }

    public void updateMusicVolume(float volume)
    {
        bgMusicVolume = volume;
        foreach (Sound s in music)
        {
            s.source.volume = volume * s.volume;
        }
    }

    public void updateSfxVolume(float volume)
    {
        effectsMusicVolume = volume;
        foreach (Sound s in sfx)
        {
            s.source.volume = volume * s.volume;
        }
    }

    private void Update()
    {

    }

    public void AddBgmSounds(Sound[] newSounds)
    {
        foreach (Sound newSound in newSounds)
        {
            // Verifica si ya existe un sonido con el mismo nombre
            bool exists = music.Exists(s => s.name == newSound.name);
            if (exists) continue;

            newSound.source = gameObject.AddComponent<AudioSource>();
            newSound.source.clip = newSound.clip;
            newSound.source.volume = newSound.volume;
            newSound.source.pitch = newSound.pitch;
            newSound.source.loop = newSound.loop;
            music.Add(newSound);
        }
    }

    public void AddSounds(Sound[] newSounds)
    {
        foreach (Sound newSound in newSounds)
        {
            // Verifica si ya existe un sonido con el mismo nombre
            bool exists = sfx.Exists(s => s.name == newSound.name);
            if (exists) continue;

            newSound.source = gameObject.AddComponent<AudioSource>();
            newSound.source.clip = newSound.clip;
            newSound.source.volume = newSound.volume;
            newSound.source.pitch = newSound.pitch;
            newSound.source.loop = newSound.loop;
            sfx.Add(newSound);
        }
    }

    public void RemoveNonPermanentSounds()
    {
        music.RemoveAll(sound => !sound.permanent);
        sfx.RemoveAll(sound => !sound.permanent);
    }

    public void StopMusic()
    {
        if (actualBGM != null && actualBGM.source.isPlaying)
        {
            actualBGM.source.Stop();
        }
    }
}