using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
	[SerializeField] private bool _playMusic;
	[SerializeField] private AudioSource _audioSource;
	//[SerializeField] private AudioClip _clip;

	private bool isPlayed;

	private void OnTriggerEnter(Collider other)
	{
		if (isPlayed) return;

		if (other.gameObject.CompareTag("Player"))
		{
			isPlayed = true;

			if (_playMusic)
			{
				_audioSource.Play();

			}
		}
	}
}
