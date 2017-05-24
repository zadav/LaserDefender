﻿using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	public float damage = 50f;
	
	public void Hit(){
		Destroy(gameObject);	
	}
	
	public float GetDamage(){
		return damage;
	}
}
