using UnityEngine;
using System.Collections;

public class FourthPlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 1f;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 500f;

	public GameObject projectile;
	public AudioClip fireSound;
	private LivesKeeper livesKeeper;

	private float xmin;
	private float xmax;

	void Start () {
		livesKeeper = GameObject.Find ("Lives").GetComponent<LivesKeeper> ();

		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	void Fire() {
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}
	
	void Update () {
		// launches projectile
		if (Input.GetKeyDown (KeyCode.Alpha8)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Alpha8)) {
			CancelInvoke ("Fire");
		}

		//move the ship left and right
		if (Input.GetKey(KeyCode.Alpha4)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}else if(Input.GetKey(KeyCode.Alpha6)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
			
		// restricts ship to the screen boundaries
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();

		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				if (PlayerSpawner.lives > 0) {
					LoseLife ();
				}
				else {
					Die ();
				}
			}
		}
	}

	void LoseLife() {
		PlayerSpawner.lives--;
		livesKeeper.SetLives (PlayerSpawner.lives);
		Destroy (gameObject);
		Invoke ("PlayerSpawner.SpawnPlayers", 5);
	}

	void Die() {
		Destroy (gameObject);
		LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		man.LoadLevel ("Win Screen");
	}
}
