
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    // PUBLIC VARIABLES

    public enum EnemyState
    {
        Wander,
        Chase,
		Stare,
		Done,
    }

    public EnemyState CurrentState;


    // PRIVATE VARIABLES

    [SerializeField] private float _startChaseRange = 5;
	[SerializeField] private float _endChaseRange = 10;
	[SerializeField] private float _wanderRange = 5;
    [SerializeField] private float _nextDestinationMin = 0.5f;
    [SerializeField] private float _wanderSpeed = 1;
	[SerializeField] private float _chaseSpeed = 2;
	[SerializeField] private float _recalculatePathAtStareCooltime = 0.5f;
	[SerializeField] private float _stareRotationSpeed = 15;
	[SerializeField] private float _ignoreHideRange = 5;

	[Header("Audio")] [SerializeField] private AudioSource _footstepAudioSource;
	[SerializeField] private AudioClip[] _footstepClips;
	[SerializeField] private AudioClip _jumpScareClip;
	[Header("Camera")]
	[SerializeField] private GameObject _scareAnimation;

	private NavMeshAgent _navMeshAgent;
    private Animator _animator;
	private AudioSource _audioSource;
    private Transform _target;
    private bool _hasTarget;

	private float _lastRecalculatePathDuringStareTime;
    

    // MONOBEHAVIOURS

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
		_animator = GetComponent<Animator>();
		_audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
		UpdateState();
		UpdateAnimation();
	}

    private void OnTriggerEnter(Collider other)
    {
		if (GameManager.Instance.PlayerHidden) return;

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Health health))
            {
                Debug.Log("Contact Detected");
                health.TakeDamage(100);
				_scareAnimation.SetActive(true);
				_navMeshAgent.ResetPath();
				_navMeshAgent.isStopped = true;
				_navMeshAgent.enabled = false;
				GetComponent<Rigidbody>().velocity = Vector2.zero;
				CurrentState = EnemyState.Done;
				_animator.SetTrigger("Scream");
			}
        }
    }
	
    // PUBLIC

    public void PlayFootStep()
    {
	    _footstepAudioSource.PlayOneShot(_footstepClips[Random.Range(0, _footstepClips.Length -1)]);
    }
    
    
	// PRIVATE METHODS

	private void UpdateState()
	{
		switch (CurrentState)
		{

			case EnemyState.Stare:


				if (_recalculatePathAtStareCooltime + _lastRecalculatePathDuringStareTime < Time.time)
				{
					_lastRecalculatePathDuringStareTime = Time.time;


					NavMeshPath path = new NavMeshPath();
					bool pathFound = _navMeshAgent.CalculatePath(GameManager.Instance.Player.position, path);

					if (pathFound && path.status == NavMeshPathStatus.PathComplete)
					{
						Debug.Log("found: " + pathFound + " status: " + path.status);
						CurrentState = EnemyState.Chase;
						break;
					}

				}

				//TODO if player hides while staring, force open the shelf.

				// Rotate toward player so it looks like staring. Use Quaternion.Lerp for smooth rotation
				Vector3 directionToPlayer = GameManager.Instance.Player.position - transform.position;
				directionToPlayer.y = 0; // This ensures the rotation only happens in the y-axis, preventing the enemy from tilting upwards or downwards
				Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
				transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _stareRotationSpeed);

				//Rotate toward player so it looks like staring. use lerp



				break;


			case EnemyState.Chase:

				if (Vector3.Distance(GameManager.Instance.Player.position, transform.position) > _endChaseRange)
				{
					CurrentState = EnemyState.Wander;
					_navMeshAgent.speed = _wanderSpeed;
					break;
				}

				if (GameManager.Instance.PlayerHidden)
				{
					if (Vector3.Distance(GameManager.Instance.Player.position, transform.position) < _ignoreHideRange)
					{
						_navMeshAgent.SetDestination(GameManager.Instance.Player.position);
						//Find all Shelf and open them
						
						if (Vector3.Distance(GameManager.Instance.Player.position, transform.position) < 1)
						{
							FindObjectOfType<HideableShelf>().ForceOpen();
						}

					}
					else
					{
						CurrentState = EnemyState.Wander;
						_navMeshAgent.speed = _wanderSpeed;
						break;
					}
				} 
				else
				{
					_navMeshAgent.SetDestination(GameManager.Instance.Player.position);
				}

				


				if (_navMeshAgent.pathStatus == NavMeshPathStatus.PathPartial || _navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
				{
					print("ANGRY");

					CurrentState = EnemyState.Stare;
					_navMeshAgent.ResetPath();

					float random = UnityEngine.Random.Range(0, 100);

					if (20 > random)
					{
						_animator.SetTrigger("Scream");
					}
					else if (random > 70)
					{
						_animator.SetTrigger("Looking");
					}
					else
					{
						_animator.SetTrigger("LookAway");
					}

					Collider[] cols = Physics.OverlapSphere(transform.position, 3, 1 << LayerMask.NameToLayer("Interaction"));

					foreach (Collider col in cols)
					{
						if (col.TryGetComponent(out Door door))
						{
							if (door.IsOpen == false && door.IsLocked == false)
							{
								door.ForceOpen();
							}
						}
					}
				}

				break;
			case EnemyState.Wander:
				if (Vector3.Distance(GameManager.Instance.Player.position, transform.position) < _startChaseRange 
					&& GameManager.Instance.PlayerHidden == false)
				{
					if (_navMeshAgent.SetDestination(GameManager.Instance.Player.position) == true)
					{
						if (_navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
						{
							CurrentState = EnemyState.Chase;
							_navMeshAgent.speed = _chaseSpeed;
							_audioSource.PlayOneShot(_jumpScareClip);
							break;
						}	
					}
				}

				if (_navMeshAgent.remainingDistance < _nextDestinationMin)
				{
					_navMeshAgent.SetDestination(GetRandomLocation(transform.position)); //later you can hand in position near current wander point
				}

				break;
		}
	}

	private void UpdateAnimation()
	{
		_animator.SetBool("Walk", _navMeshAgent.velocity.magnitude > 0f);
	} 


	private Vector3 GetRandomLocation(Vector3 offset)
	{
		// Get a random direction (and distance) within a sphere, but we only use the X and Z axes for a flat, horizontal displacement
		Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _wanderRange;
		randomDirection += offset; // Offset this by the player's (or any reference object's) position

		NavMeshHit hit; // This will store the result of the NavMesh sampling
		Vector3 finalPosition = Vector3.zero; // Initialize final position to zero

		// Try to find a nearest point on the NavMesh to this random direction, within the specified range
		if (NavMesh.SamplePosition(randomDirection, out hit, _wanderRange, NavMesh.AllAreas))
		{
			finalPosition = hit.position; // If successful, use this as the final position
		}

		return finalPosition; // Return the found position, or Vector3.zero if nothing was found
	}


}