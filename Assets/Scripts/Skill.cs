using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string Name;
    public int Damage;
    public int Level;


    public Skill(string Name, int Damage, int Level)
    {
        this.Name = Name;
        this.Damage = Damage;
        this.Level = Level;
    }

    public int Attack() {
         return this.Damage * this.Level;
    }

    public int Shield(int absorption)
    {
        return absorption * Level;
    }

    public float Movement(float speed)
    {
        return speed;
    }

}
