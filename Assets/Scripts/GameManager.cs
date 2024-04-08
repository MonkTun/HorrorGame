using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// PUBIC FIELDS

    public static GameManager Instance { get; private set; }

	public bool isGameOver { get; private set; }

	public Transform Player => _player.transform;
	public bool PlayerHidden { get; private set; }
	public int Progress => _progress;

	public bool CursorLocked = true;
	
	// PRIVATE FIELDS

	[SerializeField] private Transform _player;
	[SerializeField] private GameObject _deathPanel;

	private int _progress;
	private bool _onDialogue;

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
		//gameState = State.paused; //TODO
		//PanelManage(null);
		_onDialogue = true;
		SetCursorState(false);
	}

	public void EndDialogue()
	{
		//gameState = State.playing;
		//PanelManage(playPanel);
		_onDialogue = false;
		SetCursorState(true);
	}
	
	private void OnApplicationFocus(bool hasFocus)
	{
		if (_onDialogue == false)
			SetCursorState(CursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
}
