using System;
using UnityEngine;

public class NPC_Rotation : MonoBehaviour {
	[SerializeField] Transform player;
	[SerializeField] float rotationsSpeed;
	float rayHeight = 1f;

	void Update() {
		Vector3 pos = new Vector3(transform.position.x, transform.position.y + rayHeight, transform.position.z);
		Vector3 playerPos = new Vector3(player.position.x, player.position.y + rayHeight, player.position.z);
		Vector3 playerDir = playerPos - pos;
		RaycastHit hitInfo;
		bool hit = Physics.Raycast(pos,playerDir, out hitInfo);
		if (hit && hitInfo.transform.CompareTag("Player")) {
			Vector2 direction = new Vector2(transform.forward.x, transform.forward.z);
			Vector2 targetDirection = new Vector2(playerDir.x, playerDir.z);
			float angle = Vector2.SignedAngle(targetDirection,direction);
			if (Mathf.Abs(angle) > 1) {
				int turn = angle < 0 ? -1 : 1;
				transform.Rotate(0, turn * rotationsSpeed, 0);
			}
		}
	}
}
