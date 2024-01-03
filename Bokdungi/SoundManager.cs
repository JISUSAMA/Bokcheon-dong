using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public float fadeSpeed = 80f;
    public AudioMixer audioMixer;

    public AudioSource bgm;
    public AudioSource sfx;
    public AudioSource dialog;

    public AudioClip[] bgm_clip;
    public AudioClip[] sfx_clip;

    [Header("Character Clip")] 
    public AudioClip[] bokdungi_clip;
    public AudioClip[] suro_clip;
    public AudioClip[] bihwa_clip;
    public AudioClip[] hwangok_clip;
    public AudioClip[] daero_clip;
    public AudioClip[] goro_clip;
    public AudioClip[] malro_clip;
    
    public Dictionary<string, AudioClip> lookupTable = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> dialogTable = new Dictionary<string, AudioClip>();

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else Instance = this;

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        foreach (AudioClip clip in bgm_clip)
        {
            lookupTable.Add(clip.name, clip);
        }
        foreach (AudioClip clip in sfx_clip)
        {
            lookupTable.Add(clip.name, clip);
        }
       //캐릭터 나레이션 클립
       foreach (AudioClip clip in bokdungi_clip)
       {
           dialogTable.Add(clip.name, clip);
       }
       foreach (AudioClip clip in suro_clip)
       {
           dialogTable.Add(clip.name, clip);
       }
       foreach (AudioClip clip in bihwa_clip)
       {
           dialogTable.Add(clip.name, clip);
       }
       foreach (AudioClip clip in hwangok_clip)
       {
           dialogTable.Add(clip.name, clip);
       }
       foreach (AudioClip clip in daero_clip)
       {
           dialogTable.Add(clip.name, clip);
       }
       foreach (AudioClip clip in goro_clip)
       {
           dialogTable.Add(clip.name, clip);
       }
       foreach (AudioClip clip in malro_clip)
       {
           dialogTable.Add(clip.name, clip);
       }
      
    }

    public void PlayBGM(string clipName, float volume = 0.1f)
    {
        if (bgm.isPlaying && bgm.clip.name.Equals(clipName))
            return;
        else
        {
            AudioClip bgm_clop = GetClipByName(clipName);
            bgm.volume = volume;
            bgm.clip = bgm_clop;
            bgm.Play();
        }

    }
    public void StopBGM()
    {
        bgm.Stop();
    }
    public void PlaySFX(string clipName, float volume = 0.5f)
    {
        sfx.PlayOneShot(GetClipByName(clipName), volume);
    }
    public AudioClip GetClipByName(string clipName)
    {
        return lookupTable[clipName];
    }
    public void PlayCharacterDialog(string clipName,float dialogSpeed = 3f ,float volume = 0.8f)
    {
        //소리가 나오는 중이면
        if (dialog.isPlaying)
        {
            dialog.Stop();
            DialogManager.instance.Typing_speed = dialogSpeed;
            dialog.PlayOneShot(character_GetClipByName(clipName), volume);
        }
        else
        {
            DialogManager.instance.Typing_speed = dialogSpeed;
            dialog.PlayOneShot(character_GetClipByName(clipName), volume);
        }
        Debug.Log("dialogSpeed"+dialogSpeed);
    }
    public AudioClip character_GetClipByName(string clipName)
    {
        return dialogTable[clipName];
    }

}
