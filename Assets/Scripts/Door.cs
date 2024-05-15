using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private bool _startLocked = false;
    [SerializeField] private Animator _animator;
    
    private bool _locked;

    private bool _isOpen;
    
    private void Awake()
    {
        _locked = _startLocked;
        _isOpen = false;
    }
    
    public override void Interact(Transform interactant)
    {

        if (_locked)
        {
            if (interactant.TryGetComponent(out PlayerInventory inventory))
            {
                if (inventory.HasKey)
                {
                    inventory.UseItem(PlayerInventory.Items.Key);
                    _locked = false;
                }
            }
        }

        if (_locked) return;

        _isOpen = !_isOpen;

        if (_isOpen)
        {
            _animator.SetTrigger("Opens");
        }
        else
        {
            _animator.SetTrigger("Closes");
        }
    }
    
}
