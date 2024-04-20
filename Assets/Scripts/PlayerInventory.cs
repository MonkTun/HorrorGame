using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public enum Items
    {
        Apple,
        Banana,
        Bone,
        Knife,
        Potion,
        Key
    }
    
    public bool HasKey => _key > 0;
    
    private int _key;
    
    public void AddItem(Items item)
    {
        switch (item)
        {
            case Items.Key:

                _key++;
                
                break;
        }
    }

    public void UseItem(Items item)
    {
        switch (item)
        {
            case Items.Key:

                _key--;
                
                break;
        }
    }
    
    
}
