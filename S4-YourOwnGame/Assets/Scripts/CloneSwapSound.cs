using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSwapSound : MonoBehaviour
{
    public static CloneSwapSound instance;

    [SerializeField] AudioSource enterClone;
    [SerializeField] AudioSource stayClone;
    [SerializeField] AudioSource exitClone;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        transform.position = Camera.main.transform.position;
    }

    [ContextMenu("EnterClone")]
    public void Enterclone() => StartCoroutine(EnterCloneRoutine());

    private IEnumerator EnterCloneRoutine()
    {
        enterClone.Play();
        yield return new WaitWhile(() => enterClone.isPlaying);
        if (exitClone.isPlaying) yield break;
        stayClone.Play();
    }

    [ContextMenu("ExitClone")]
    public void ExitClone()
    {
        StartCoroutine(FadeOut(stayClone, 0.1f));
        exitClone.Play();
    }

    private IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}

