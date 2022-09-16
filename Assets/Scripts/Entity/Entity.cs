using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

	public int health;
	public GameObject deathEffect;
	public bool vulnerable;
	public int scorePoints;

	protected GameObject canvas;

	private void Awake() {
		canvas = GameObject.Find("Canvas");
	}

	public virtual void TakeDamage(int damage) {
		health -= damage;
		if (health <= 0) {
			canvas.GetComponent<UI>().SetScore(scorePoints);
			Instantiate(deathEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

}

