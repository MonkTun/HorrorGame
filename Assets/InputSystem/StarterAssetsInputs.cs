using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		//public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			if (GameManager.Instance.IsPlaying == false)
			{
				move = Vector2.zero;
				return;
			}

			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			if (GameManager.Instance.IsPlaying == false)
			{
				look = Vector2.zero;
				return;
			}

			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			if (GameManager.Instance.IsPlaying == false)
			{
				jump = false;
				return;
			}

			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			if (GameManager.Instance.IsPlaying == false)
			{
				sprint = false;
				return;
			}

			sprint = newSprintState;
		}
	}
	
}