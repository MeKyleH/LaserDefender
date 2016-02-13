using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float padding = 1f;
	public float spawnDelay = 0.5f;

	private bool movingRight = true;
	private float xmin;
	private float xmax;

	void Start () {
		// create clamping on screen for enemy formation
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBound = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBound = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0));
		xmin = leftBound.x;
		xmax = rightBound.x;

		SpawnUntilFull ();
	}

	void SpawnEnemies() {
		foreach (Transform child in transform) {
			GameObject enemy =Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height));
	}
	
	void Update () {
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		float rightFormationEdge = transform.position.x + (0.5f * width);
		float leftFormationEdge = transform.position.x - (0.5f * width);

		if (leftFormationEdge < xmin) {
			movingRight = true;
		}
		else if (rightFormationEdge > xmax) {
			movingRight = false;
		}

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
