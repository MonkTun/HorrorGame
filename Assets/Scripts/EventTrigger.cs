using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
	[SerializeField] private bool _playMusic;
	[SerializeField] private bool _nextScene;
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private GameObject _jumpScareObj;
	//[SerializeField] private AudioClip _clip;

	private bool isPlayed;

	private void OnTriggerEnter(Collider other)
	{
		if (isPlayed) return;


		if (other.gameObject.CompareTag("Player"))
		{
			if (_nextScene) SystemManager.Instance.LoadScene(2);	

			isPlayed = true;

			if (_playMusic)
			{
				_audioSource.Play();

			}

			if (_jumpScareObj != null)
				_jumpScareObj.SetActive(true);
		}
	}
}
