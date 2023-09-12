using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioSource audioSource;
    public AudioClip monsterHurtClip;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMonsterHurtSound()
    {
               audioSource.PlayOneShot(monsterHurtClip);

    }
}
