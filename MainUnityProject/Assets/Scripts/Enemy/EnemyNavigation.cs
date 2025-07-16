using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour {
	static readonly int WALKING = Animator.StringToHash("Walking");
	// ---------------- FIELDS ----------------
	
	// PRIVATE
	EnemySpawner enemySpawner;
	NavMeshAgent navMeshAgent;
	PlayerStats playerStats;
	Animator animator;
	// ---------------- METHODS ----------------
	void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
	}
	[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
	public void SetScript(EnemySpawner script) {
		enemySpawner = script;
		playerStats = enemySpawner.targetPlayer.GetComponent<PlayerStats>();
		// AGENT SETUP
		navMeshAgent.speed = enemySpawner.walkSpeed;
		navMeshAgent.angularSpeed = enemySpawner.turnSpeed;
		navMeshAgent.stoppingDistance = enemySpawner.stoppingDistance;
	}
	void Update()
	{
		if (Stopped()) {
			animator.SetBool(WALKING,false);
		}
		else {
			animator.SetBool(WALKING,true);
		}
		if (!enemySpawner.targetPlayer || playerStats.GetState() is PlayerStats.STATE_DEAD) {
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
		if (navMeshAgent.hasPath) return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
		return false;
	}

	void OnDrawGizmos()
	{
		if(!navMeshAgent) return;
		if (Stopped()) Gizmos.color = Color.green;
		else Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,navMeshAgent.stoppingDistance);
	}
}