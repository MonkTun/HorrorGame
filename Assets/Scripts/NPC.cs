using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class NPC : Interactable
{
    [SerializeField] DialogueSystemTrigger DST;

    private void OnEnable()
    {
        DialogueManager.Instance.conversationEnded += OnConversationEnd();
    }
    
    private void OnDisable()
    {
        if (DialogueManager.Instance) //only check this on OnDisable and OnConversationEnd because these stop causing errors
            DialogueManager.Instance.conversationEnded -= OnConversationEnd();
    }
    
    public override void Interact(Transform interactant)
    {
        GameManager.Instance.StartDialogue();
        DST.OnUse(interactant);

    }

    public TransformDelegate OnConversationEnd()
    {
        if (GameManager.Instance)
            GameManager.Instance.EndDialogue();
        //DialogueManager.Instance.conversationEnded

        return null;
    }
}