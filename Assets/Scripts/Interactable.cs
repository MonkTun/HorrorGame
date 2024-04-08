using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected string _interactText;

    public virtual void Interact(Transform interactant)
    {

    }

    public virtual string InteractCheck()
    {
        return _interactText;
    }
}
