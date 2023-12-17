using System.Collections;
using UnityEngine;

//Used for Fading audio
//source: https://johnleonardfrench.com/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best/
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