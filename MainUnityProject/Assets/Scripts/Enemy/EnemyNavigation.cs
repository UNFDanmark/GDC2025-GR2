using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour {
	// ---------------- FIELDS ----------------
	
	// PRIVATE
	EnemySpawner enemySpawner;
	NavMeshAgent navMeshAgent;
	// ---------------- METHODS ----------------
	void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
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
		if (!enemySpawner.targetPlayer) {
			navMeshAgent.SetDestination(transform.position);
			return;
		}
		navMeshAgent.SetDestination(enemySpawner.targetPlayer.transform.position);
		if (Stopped()) {
			var _playerDir = enemySpawner.targetPlayer.transform.position - transform.position;
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