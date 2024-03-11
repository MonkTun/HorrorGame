using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// PUBIC FIELDS

    public static GameManager Instance { get; private set; }

	public bool isGameOver { get; private set; }

	public Transform Player => _player.transform;
	public int Progress => _progress;


	// PRIVATE FIELDS

	[SerializeField] private Transform _player;
	[SerializeField] private GameObject _deathPanel;

	private int _progress;

	// MONOBEHAVIOURS


	private void Awake()
	{
		Instance = this;

		_deathPanel.SetActive(false);

		isGameOver = false;
		Cursor.lockState = CursorLockMode.Confined;
	}

	// PUBLIC METHODS

	public void IncrementProgress()
	{
		_progress++;
	}

	public void GameOver()
	{
		_deathPanel.SetActive(true);
		isGameOver = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
