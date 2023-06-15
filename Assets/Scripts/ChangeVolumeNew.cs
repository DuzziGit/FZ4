using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolumeNew : MonoBehaviour
{
    //Invoked when a submit button is clicked.
    public void OnValueChanged(float value)
    {
        AudioListener.volume = value;
    }
}
