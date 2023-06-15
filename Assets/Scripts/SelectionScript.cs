/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScript : MonoBehaviour
{
    
        [SerializeField] Button[] _IconPrefabs;

        void Awake()
        {
            // Instantiate your icons(Buttons) with characters and add them listener to change CharacterIndex in main script.
            int length = _IconPrefabs.Length;
            for (int i = 0; i < length; i++)
            {
                int j = i;
                Button _Icon = Instantiate(_IconPrefabs[i]);
                _Icon.onClick.AddListener(() => GameManager.CharacterIndex = j);
            }
        }
    

}*/
