using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableShelf : Interactable
{
	[SerializeField] private Shelf _shelf;
	[SerializeField] private GameObject _cameraForHide;

	private bool _isHiding;

	public override void Interact(Transform interactant)
	{

		if (_shelf.IsOpen)
		{
			_cameraForHide.SetActive(true);
			GameManager.Instance.PlayerHide(true);
			_isHiding = true;
			_shelf.ForceClose();
		} 
		
	}

	public void ForceOpen()
	{
		//haha he found you
		_cameraForHide.SetActive(false);
		GameManager.Instance.PlayerHide(false);
		_isHiding = false;
		_shelf.ForceOpen();
	}

	private void Update()
	{
		if (_isHiding)
		{
			GameplayUI.Instance.UpdateInteract("F - to exit");

			if (Input.GetKeyDown(KeyCode.F))
			{
				_cameraForHide.SetActive(false);
				GameManager.Instance.PlayerHide(false);
				_isHiding = false;
				_shelf.ForceOpen();

			}
		}
	}
}
