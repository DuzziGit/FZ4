using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinController : MonoBehaviour
{
    public static int coin;
	public GameObject CoinDropped;

    // Start is called before the first frame update
    void Start()
    {
            	Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
    }

     void OnTriggerEnter2D(Collider2D collision)
	{
        Destroy(this.gameObject);
	}
	
}
