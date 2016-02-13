using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {
	public GameObject playerPrefab;
	public static int lives = 5;

	public float spawnDelay = 2f;

	void Start () {
		// create clamping on screen for enemy formation
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBound = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBound = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0));

		SpawnUntilFull ();
	}

	void SpawnPlayer() {
		foreach (Transform child in transform) {
			GameObject player =Instantiate (playerPrefab, child.transform.position, Quaternion.identity) as GameObject;
			player.transform.parent = child;
		}
	}

	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (playerPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	public void OnDrawGizmos() {
		//Gizmos.DrawWireCube (transform.position, new Vector3(width, height));
	}
	
	void Update () {
		if (AllMembersDead ()) {
			SpawnUntilFull ();
		}
	}

	bool AllMembersDead() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}

	Transform NextFreePosition() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}
}
