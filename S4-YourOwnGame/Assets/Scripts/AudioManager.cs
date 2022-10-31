using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // Start is called before the first frame update
    void Start() => instance = this;

    public void PlayAudioClip(string FilePath)
    {
        AudioClip CurrentClip = (AudioClip)Resources.Load(FilePath);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(CurrentClip);
    }
}
