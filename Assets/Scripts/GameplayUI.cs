using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance { get; private set; }

	[Header("Interaction")]
	[SerializeField] private TMP_Text _interactText;
	[SerializeField] private float _interactDisplayTime = 0.2f;
	[SerializeField] private CanvasGroup _interactGroup;
	[SerializeField] private float _fadeMultiplier = 2f;
	[SerializeField] private TMP_Text _inventoryText;
	
	
	private float _lastInteractiontime;


	private void Awake()
	{
		Instance = this;
	}

	public void UpdateInteract(string information)
	{
		_interactText.text = information;
		_lastInteractiontime = Time.time;
		_interactGroup.alpha = 1;
	}

	private void Update()
	{
		if (_lastInteractiontime + _interactDisplayTime < Time.time)
		{
			if (_interactGroup.alpha > 0)
			{
				_interactGroup.alpha -= Time.deltaTime * _fadeMultiplier;
			}
		}
	}

	public void UpdateInventoryText(string message)
	{
		_inventoryText.text = message;
	}
}
