using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public GameObject itemToDrop; // The actual item that will be dropped. This can be a prefab.
    public int dropChance; // The chance that this item will drop.
}

public class LootTable : MonoBehaviour
{
    public List<Loot> loots; // The list of potential items to drop.

    public void DropLoot()
    {
        int cummulativeProbability = 0;
        int randomNumber = Random.Range(0, 100); // This will give us a random number between 0 and 100.

        foreach (Loot loot in loots)
        {
            cummulativeProbability += loot.dropChance;
            if (randomNumber <= cummulativeProbability)
            {
                // Drop the item.
                Instantiate(loot.itemToDrop, transform.position, Quaternion.identity);
                return;
            }
        }
    }
}