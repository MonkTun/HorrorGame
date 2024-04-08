using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : Interactable
{
	public bool IsOpen => _isOpen;

	[SerializeField] private Animator _animator;
	[SerializeField] private bool _startOpen = false;
	bool _isOpen;

	private void Awake()
	{
		_isOpen = _startOpen;
		_animator.SetBool("IsOpen", _isOpen);
	}

	public override void Interact(Transform interactant)
	{
		_isOpen = !_isOpen;

		_animator.SetBool("IsOpen", _isOpen);
	}

	public void ForceClose()
	{
		_isOpen = false;
		_animator.SetBool("IsOpen", false);
		//TODO audio
	}
	public void ForceOpen()
	{
		_isOpen = true;
		_animator.SetBool("IsOpen", true);
		//TODO audio
	}
}
