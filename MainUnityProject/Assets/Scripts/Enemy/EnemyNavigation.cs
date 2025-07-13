using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour {
	// ---------------- FIELDS ----------------
	
	// PRIVATE
	EnemySpawner enemySpawner;
	NavMeshAgent navMeshAgent;
	Transform playerTransform;
	// ---------------- METHODS ----------------
	void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		playerTransform = GameObject.FindWithTag("Player").transform;
	}
	public void SetScript(EnemySpawner script) {
		enemySpawner = script;
		// AGENT SETUP
		navMeshAgent.speed = enemySpawner.walkSpeed;
		navMeshAgent.angularSpeed = enemySpawner.turnSpeed;
		navMeshAgent.stoppingDistance = enemySpawner.stoppingDistance;
	}
	void Update()
	{
		if (!playerTransform) {
			navMeshAgent.SetDestination(transform.position);
			return;
		}
		navMeshAgent.SetDestination(playerTransform.position);
		if (Stopped()) {
			var _playerDir = playerTransform.position - transform.position;
			_playerDir.Normalize();
			var _desiredRotation = Quaternion.LookRotation(_playerDir);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, _desiredRotation, enemySpawner.stoppedEnemyRotationSpeed);
		}
	}
	public bool Stopped() {
		return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
	}

	void OnDrawGizmos()
	{
		
		if (Stopped()) Gizmos.color = Color.green;
		else Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,navMeshAgent.stoppingDistance);
	}
}