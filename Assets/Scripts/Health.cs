using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float CurrentHealth { get; private set; }
   
    public bool DeadState { get; private set; }

    [SerializeField] private float _baseHealth = 100;

    [SerializeField] private bool _isPlayer;

    private void Start()
    {
        CurrentHealth = _baseHealth;
    }

    public void Die()
    {
        if (DeadState == false)
        {
            DeadState = true;
            
            gameObject.SetActive(false);
            if (_isPlayer) GameManager.Instance.GameOver();
        }
        
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Damage Taken");
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    
}
