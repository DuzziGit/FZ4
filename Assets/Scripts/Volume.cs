using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Volume : MonoBehaviour
{

    public void AdjustVolume(float volume)
    {
        AudioListener.volume = volume;
    }

}
