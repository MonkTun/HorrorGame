using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float PlayerHealthValue { get; private set; }
    
    public float PriestHealth { get; private set; }
    public bool DeadState { get; private set; }

    public float baseHealth = 100;

    public float priestBaseHealth = 1 * 10 ^ 99;

    private void Start()
    {
        PlayerHealthValue = baseHealth;
        PriestHealth = priestBaseHealth;
    }

    public void Die()
    {
        if (DeadState == false)
        {
            DeadState = true;
            if (DeadState = true)
            {
                Debug.Log("You've Died");
            }
            //if this is part of player, gameover + game death screen shows up
            //if this is enemy, just destroy this gameobject
        }
        
    }
    public void Damage(float incomeDamage)
    {
        PlayerHealthValue -= incomeDamage;
        Debug.Log("Damage Taken");
        if (PlayerHealthValue <= 0)
        {
            Die();
        }
    }
    
}
