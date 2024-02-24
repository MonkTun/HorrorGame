using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    NavMeshAgent _priestNavMeshAgent;
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    
    
    // Start is called before the first frame update
    void Start()
    {
        _priestNavMeshAgent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerTracing(transform.position, 4.3f, GameObject.FindGameObjectWithTag("Player"));
        
        // Ensure Player is found
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void PlayerTracing(Vector3 center, float radius, GameObject player)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // Make the enemy follow the player
                if (_priestNavMeshAgent != null && player != null)
                {
                    _priestNavMeshAgent.SetDestination(player.transform.position);
                }
                }
            }
        }
    }

