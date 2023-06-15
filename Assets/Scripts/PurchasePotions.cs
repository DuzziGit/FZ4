using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PurchasePotions : MonoBehaviour
{

    public InputField amount;


   public void purchasePotions()
    {
       int playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().coins;
        int maxHealthPotions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().maxHealthPotions;
    

        if ((Int32.Parse(amount.text) * 50) <= playerMoney)
        {
           GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().coins -= Int32.Parse(amount.text) * 50;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().healthPotions += Int32.Parse(amount.text);


            
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().healthPotions > maxHealthPotions)
			{
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().healthPotions = maxHealthPotions;

            }




        }
    }
}
