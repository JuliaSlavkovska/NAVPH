using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class FadeAudioSource {
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume, bool FadeIn)
    {
        float currentTime = 0;
        float start;
        if (FadeIn)
        {
            audioSource.Play();
            start = 0;
        }
        else
        {
            start = audioSource.volume;
        }
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            if (audioSource.volume == 0 && !FadeIn)
            {
                audioSource.Stop();
            }
            yield return null;
        }
        yield return null;
    }
}