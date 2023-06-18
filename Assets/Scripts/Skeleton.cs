using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Skeleton : Enemy
{
    public AudioSource audioSource;
    public AudioClip skeletonHitSound;
    public int enemyDamage;
    public TMP_Text damageDisplay; // Change the type to TMP_Text
    public TextMesh enemyLevel;
    public GameObject CanvasDamageNum;
    public bool isTouchingPlayer = false;

    void Start()
    {
        rb.velocity = new Vector3(speed, 0, 0);
        enemyLevel.text = "lvl. " + level;
        enemyDamage = level * 5;

        if (level > 0 && level < 10) enemyLevel.color = tutEnemy;
        else if (level > 10 && level < 20) enemyLevel.color = smallEnemy;
        else if (level > 20 && level < 30) enemyLevel.color = medEnemy;
        else if (level > 30 && level < 40) enemyLevel.color = bigEnemy;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage Taken: " + damage);
        StartCoroutine(DamageDisplay(damage));
        Debug.Log("Current Health: " + health);
        // audioSource.PlayOneShot(skeletonHitSound, 0.7f);
    }
    IEnumerator DamageDisplay(int damage)
    {
        float xOffset = 12f; 
        float yOffset = 3f; 
        Vector3 positionOffset = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset + Random.Range(1.0f, 3.0f), transform.position.z);
        GameObject text = Instantiate(CanvasDamageNum, positionOffset, Quaternion.identity);


        DamageNumController controller = text.GetComponent<DamageNumController>();
        if (controller != null)
        {
            controller.SetDamageNum(damage);
        }
        // Wait a short time before instantiating the next damage number to make them stack
        yield return new WaitForSeconds(0.1f);
    }
}