using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class audiocontroler : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] clips;
     public AudioSource music, effects, effectsP;
     public void PlayEffects(int indice)
    {
        this.effects.clip = clips[indice];
        this.effects.Play();
    }
    public void PlayEffectsP(int indice)
    {
        this.effectsP.clip = clips[indice];
        this.effectsP.Play();
    }
    public void Playmusic(int indice)
    {
        this.music.clip = clips[indice];
        this.music.Play();
        Debug.Log("olaplay");
    }
}
