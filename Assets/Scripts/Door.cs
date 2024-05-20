using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private bool _startLocked = false;
	[SerializeField] private bool _startOpened = false;
	[SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _unlockClip, _lockedClip, _openClip, _closeClip;

    public bool IsLocked => _locked;
    public bool IsOpen => _isOpen;

    private bool _locked;

    private bool _isOpen;

    private bool _isBroken;
    
    private void Awake()
    {
        _locked = _startLocked;
        _isOpen = false;
    }

	private void Start()
	{
		if (_startOpened)
        {
            ForceOpen();
		}
	}

	public void ForceOpen()
    {
        Destroy(this, 2); //break;
        _isBroken = true;
		_animator.SetTrigger("Opens");
		_audioSource.PlayOneShot(_openClip);
	}
    
    public override void Interact(Transform interactant)
    {
        if (_isBroken) return;

        if (_locked)
        {
            if (interactant.TryGetComponent(out PlayerInventory inventory))
            {
                if (inventory.HasKey)
                {
                    inventory.UseItem(PlayerInventory.Items.Key);
                    _locked = false;
                    _audioSource.PlayOneShot(_unlockClip);

				}
            }
        }

        if (_locked)
        {
			_audioSource.PlayOneShot(_lockedClip);
			return;
        }

        _isOpen = !_isOpen;

        if (_isOpen)
        {
            _animator.SetTrigger("Opens");
			_audioSource.PlayOneShot(_openClip);
		}
        else
        {
            _animator.SetTrigger("Closes");
			_audioSource.PlayOneShot(_closeClip);
		}
    }
    
}
