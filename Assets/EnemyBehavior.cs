using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    NavMeshAgent _priestNavMeshAgent;
    private GameObject player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _priestNavMeshAgent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        float _DurationUntilReset = 5f;
        Vector3 _PriestStartArea = new Vector3(0, 0, 0); // Defaulting to this. In the future can change to nearest set default zone (multiple across location) and/or have a set movement rotation that walks around the zone
    }

    // Update is called once per frame
    void Update()
    {
        PlayerTracing(transform.position, 4.3f, player);
        
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
            else
            {
                Debug.Log("Hello world"); // It works

                IEnumerator TestRoutine(float _DurationUntilReset)
                {
                    // Player outside of range for set amount of time
                    yield return new WaitForSeconds(_DurationUntilReset);
                    
                    // Return to x location
                    _priestNavMeshAgent.SetDestination();
                }
            }
        }
    }
}

