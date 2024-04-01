using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : Interactable
{
	[SerializeField] private Animator _animator;
	[SerializeField] private bool _startOpen = false;
	bool _isOpen;

	private void Awake()
	{
		_isOpen = _startOpen;
		_animator.SetBool("IsOpen", _isOpen);
	}

	public override void Interact()
	{
		_isOpen = !_isOpen;

		_animator.SetBool("IsOpen", _isOpen);
	}
}
