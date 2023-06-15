using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WalletMoney : MonoBehaviour
{

    public Text money;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        money.text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().coins.ToString();
    }
}
