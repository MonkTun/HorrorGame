using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    // PUBLIC VARIABLES

    public enum EnemyState
    {
        Wander,
        Chase,

    }

    public EnemyState CurrentState;


    // PRIVATE VARIABLES

    [SerializeField] private float _startChaseRange = 5;
	[SerializeField] private float _endChaseRange = 10;
	[SerializeField] private float _wanderRange = 5;
    [SerializeField] private float _nextDestinationMin = 0.5f;
    [SerializeField] private float _wanderSpeed = 1;
	[SerializeField] private float _chaseSpeed = 2;

	private NavMeshAgent _navMeshAgent;
    private Transform _target;
    private bool _hasTarget;
    
    // MONOBEHAVIOURS

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        switch (CurrentState)
        {

            case EnemyState.Chase:

				if (Vector3.Distance(GameManager.Instance.Player.position, transform.position) > _endChaseRange)
				{
					CurrentState = EnemyState.Wander;
					_navMeshAgent.speed = _wanderSpeed;
					break;
				}

				_navMeshAgent.SetDestination(GameManager.Instance.Player.position);                

				break;
            case EnemyState.Wander:
                if (Vector3.Distance(GameManager.Instance.Player.position, transform.position) < _startChaseRange)
                {
                    CurrentState = EnemyState.Chase;
					_navMeshAgent.speed = _chaseSpeed;
					break;
				}

                if (_navMeshAgent.remainingDistance < _nextDestinationMin)
                {
				    _navMeshAgent.SetDestination(GetRandomLocation(transform.position)); //later you can hand in position near current wander point
                }

				break;
        }
    }


	public Vector3 GetRandomLocation(Vector3 offset)
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Health health))
            {
                Debug.Log("Contact Detected");
                health.TakeDamage(100);
            }
        }
    }
}