using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed =15.0f;
	public float padding = 1f;
	
	public GameObject projectile;
	public float projectileSpeed = 5f;
	public float firingRate = 0.2f;
	
	public float health = 200f;
	
	float xmin;
	float xmax;
	
	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;		
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
	}
	
	void Fire(){
		Vector3 offset = new Vector3(0,1,0);
		GameObject beam = Instantiate(projectile,transform.position + offset,Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0,projectileSpeed,0);
	}
	
	void OnTriggerEnter2D(Collider2D col){
		EnemyProjectile enemyMissile = col.gameObject.GetComponent<EnemyProjectile>();
		if(enemyMissile){
			health -= enemyMissile.GetDamage();
			enemyMissile.Hit();
			if(health <= 0){
				Die();
			}
		}
		
	}
	
	void Die(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		
		man.LoadLevel("Win Screen");
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire",0.000001f,firingRate);
		}
		
		if (Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}

		if(Input.GetKey(KeyCode.LeftArrow)){
			//this.transform.position += new Vector3(-speed * Time.deltaTime,0,0);
			transform.position += Vector3.left * speed * Time.deltaTime ;
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime ;
		} 
		
		//Restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x,xmin,xmax);
		transform.position = new Vector3(newX,transform.position.y,transform.position.z);
	}
}
