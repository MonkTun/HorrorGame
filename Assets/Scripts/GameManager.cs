using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GameManager : MonoBehaviour
{
	// PUBIC FIELDS

    public static GameManager Instance { get; private set; }

	public Transform Player => _player.transform;
	public bool PlayerHidden { get; private set; }
	public int Progress => _progress;

	public enum State
	{
		paused,
		playing,
		talking,
		gameOver,
		//...
	}

	public State GameState { get; private set; }

	public bool IsPaused => GameState == State.paused;
	public bool IsPlaying => GameState == State.playing;
	public bool IsTalking => GameState == State.talking;
	public bool IsGameOver => GameState == State.gameOver;

	// PRIVATE FIELDS

	[SerializeField] private Transform _player;
	[SerializeField] private GameObject _deathPanel;
	[SerializeField] private GameObject _pausedPanel;

	private int _progress;

	// MONOBEHAVIOURS



	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;

		_deathPanel.SetActive(false);
		_pausedPanel.SetActive(false);

		GameState = State.playing;

		SetCursorState(true);
	}

	private void Update()
	{
		if (IsGameOver || IsTalking) return;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (IsPaused == false)
			{

				PauseGame();
				_pausedPanel.SetActive(true);
			} 
			else
			{
				UnPauseGame();
				_pausedPanel.SetActive(false);
			}
		}
	}


	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		//Cursor.visible = newState;
	}

	// PUBLIC METHODS

	public void PauseGame()
	{
		GameState = State.paused;
		SetCursorState(false);
	}

	public void UnPauseGame()
	{
		GameState = State.playing;
		SetCursorState(true);
	}

	public void IncrementProgress()
	{
		_progress++;
	}

	public void GameOver()
	{
		_deathPanel.SetActive(true);

		GameState = State.gameOver;
		Cursor.lockState = CursorLockMode.None;
	}

	public void PlayerHide(bool hide)
	{
		if (hide)
		{
			_player.gameObject.SetActive(false);
			PlayerHidden = true;
		} else
		{
			_player.gameObject.SetActive(true);
			PlayerHidden = false;
		}
	}
	
	public void StartDialogue()
	{
		if (IsGameOver) return;
		
		//TODO disable UIs

		GameState = State.talking; 

		SetCursorState(false);
	}

	public void EndDialogue()
	{
		GameState = State.playing;

		SetCursorState(true);
	}
	
}
