using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    int maxHealth;
    int health;
    bool alive; 


    public HealthSystem(int maxHealth)
    {
        this.alive = true;
        this.maxHealth = maxHealth;
        this.health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public bool isAlive(){
        return alive;
    } 


    public void TakeDamage(int damageAmount)
    { 
        health = health - damageAmount; 
        if (health <= 0)
        {  
            alive = false; 
        }
    }

    public float GetHealthPct()
    {
        return  (float)health / maxHealth;
    }

    public void Heal(int healAmount)
    { 
        health = Mathf.Min(health + healAmount, maxHealth); 
    }


}
