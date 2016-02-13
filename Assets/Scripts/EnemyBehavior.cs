using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public GameObject projectile;
	public AudioClip fireSound;
	public AudioClip deathSound;
	private ScoreKeeper scoreKeeper;

	public float health = 200f;
	public float projectileSpeed = 10f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 151;

	void Start() {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}
	void Update() {
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {
			Fire ();
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();

		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				Die ();
			}
		}
	}

	void Die() {
		AudioSource.PlayClipAtPoint (deathSound, transform.position);
		Destroy (gameObject);
		scoreKeeper.Score (scoreValue);
	}

	void Fire() {
		GameObject missile = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -projectileSpeed);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}
}
