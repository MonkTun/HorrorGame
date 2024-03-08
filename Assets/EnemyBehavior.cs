using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public enum EnemyState
    {
        Wander,
        Chase,
        Attack,
    }

    public EnemyState _currentState;
    
    NavMeshAgent _priestNavMeshAgent;
    private GameObject player;

    private Coroutine _coroutine;
    public float range = 10.0f;


    private bool _hasTarget;
    
    // public bool _IsWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        _priestNavMeshAgent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        float _DurationUntilReset = 5f;
        //Vector3 _PriestRandomArea = GetRandomLocation(); // Defaulting to this. In the future can change to nearest set default zone (multiple across location) and/or have a set movement rotation that walks around the zone
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Attack:

                break;
            case EnemyState.Chase:
                
                break;
            case EnemyState.Wander:
                if (_hasTarget)
                {
                    FindPlayer(transform.position, 4.3f, player);
                }
                break;
        }
        
        if (_priestNavMeshAgent.remainingDistance <= 1f)
        {
            _hasTarget = false;
        }
        
        
        // Ensure Player is found
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void FindPlayer(Vector3 center, float radius, GameObject player)
    {
        Collider[] hitColliders = UnityEngine.Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // Make the enemy follow the player
                if (_priestNavMeshAgent != null && player != null)
                {
                    _priestNavMeshAgent.SetDestination(player.transform.position);
                    _hasTarget = true;
                    _currentState = EnemyState.Chase;
                }
            }
        }
    }
    
    

    
    public Vector3 GetRandomLocation()
    {
        // Get a random direction (and distance) within a sphere, but we only use the X and Z axes for a flat, horizontal displacement
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * range;
        randomDirection += transform.position; // Offset this by the player's (or any reference object's) position
        
        NavMeshHit hit; // This will store the result of the NavMesh sampling
        Vector3 finalPosition = Vector3.zero; // Initialize final position to zero
        
        // Try to find a nearest point on the NavMesh to this random direction, within the specified range
        if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
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
                health.Damage(100);
            }
        }
    }
}