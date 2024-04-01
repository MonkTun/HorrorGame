using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Transform _cameraRoot;
    [SerializeField] private float _maxInteratableDistance = 2;
    [SerializeField] private LayerMask _interactMask;

    private Interactable _interactable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInteratables();

	}

    private void CheckInteratables() //TODO can't interact if it's far
    {
		if (Physics.Raycast(_cameraRoot.position, _cameraRoot.forward, out RaycastHit hit, _maxInteratableDistance, _interactMask))
		{
			if (hit.collider.TryGetComponent(out Interactable interactable))
			{
				// Debug.Log(interactable.gameObject.name);
				_interactable = interactable;
				GameplayUI.Instance.UpdateInteract(interactable.InteractCheck());

				if (Input.GetKeyDown(KeyCode.F))
				{
					interactable.Interact();
				}
			}
		}
	}
}
