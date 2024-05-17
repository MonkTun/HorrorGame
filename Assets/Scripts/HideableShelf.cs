using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableShelf : Interactable
{
	[SerializeField] private GameObject _cameraForHide;
	[SerializeField] private Animator _animator;

	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private AudioClip _openClip, _closeClip;

	private bool _isHiding;

	private Coroutine _coroutine;

	public override void Interact(Transform interactant)
	{

		if (_isHiding == false)
		{
			if (_coroutine == null)
				_coroutine = StartCoroutine(EnterRoutine());


		} 
		
	}

	private IEnumerator EnterRoutine()
	{
		_animator.SetBool("IsOpen", true);
		_audioSource.PlayOneShot(_openClip);

		yield return new WaitForSeconds(0.5f);

		_cameraForHide.SetActive(true);
		
		GameManager.Instance.PlayerHide(true);
		yield return new WaitForSeconds(1);
		_animator.SetBool("IsOpen", false);
		_isHiding = true;

		_coroutine = null;
	}

	private IEnumerator ExitRoutine()
	{
		_animator.SetBool("IsOpen", true);
		_audioSource.PlayOneShot(_closeClip);

		yield return new WaitForSeconds(0.5f);

		_cameraForHide.SetActive(false);
		GameManager.Instance.PlayerHide(false);
		_isHiding = false;
		yield return new WaitForSeconds(1);
		_animator.SetBool("IsOpen", false);

		_coroutine = null;
	}


	public void ForceOpen()
	{
		//haha he found you
		_cameraForHide.SetActive(false);
		GameManager.Instance.PlayerHide(false);
		_isHiding = false;
		_animator.SetBool("IsOpen", true);
	}

	private void Update()
	{
		if (_isHiding)
		{
			GameplayUI.Instance.UpdateInteract("F - to exit");

			if (Input.GetKeyDown(KeyCode.F))
			{
				if (_coroutine == null)
					_coroutine = StartCoroutine(ExitRoutine());

			}
		}
	}
}
