using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private PlayerInventory.Items _item; 
    
    public override void Interact(Transform interactant)
    {
        if (interactant.TryGetComponent(out PlayerInventory inventory))
        {
            inventory.AddItem(_item);
            Destroy(gameObject);
        }
    }
}
