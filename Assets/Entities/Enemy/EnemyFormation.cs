using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {

	public float health = 150;
	
	public GameObject projectile;
	public float projectileSpeed = 5f;
	
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;
	
	private ScoreKeeper scoreKeeper;
	
	public AudioClip destroySound;
	
	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}
	
	void Update(){
		float probability = shotsPerSeconds * Time.deltaTime;
		if(Random.value < probability){
			Fire ();
		}
	}
	
	void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0f,-0.5f,0f); 
		GameObject beam = Instantiate(projectile,startPosition,Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector2(0,-projectileSpeed);
	}
	
	void OnTriggerEnter2D(Collider2D col){
		Debug.Log("Enemy hit !");
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			Debug.Log("Hit by a projectile");
			missile.Hit();
			if(health <= 0){
				AudioSource.PlayClipAtPoint(destroySound,transform.position);
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}
		}
	}
}
