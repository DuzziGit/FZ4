using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{


	public int expSent;
	public ExperienceController expObject;
    public int rValue_1;

	public static int expToBeGained;

	//When Enemy Dies
	void OnTriggerEnter2D(Collider2D collision)
	{
		//Example Exp Spawn for an Enemy
		rValue_1 = Random.Range(20, 32);
		ExperienceController exp = Instantiate(expObject);
		ExperienceController.experience += rValue_1;
		
	}
}
