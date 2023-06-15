using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{

    public void ScreenChange()
    {
        // Toggles fullscreen
        Screen.fullScreen = !Screen.fullScreen;
    }
}
