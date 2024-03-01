using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HealthValue { get; private set; }
    public bool DeadState { get; private set; }
    public void Die()
    {
        if (DeadState == false)
        {
            DeadState = true;
            //if this is part of player, gameover + game death screen shows up
            //if this is enemy, just destroy this gameobject
        }
        
    }
    public void Damage(float incomeDamage)
    {
        HealthValue = HealthValue - incomeDamage;
        if (HealthValue <= 0)
        {
            Die();
        }
    }
    
}
