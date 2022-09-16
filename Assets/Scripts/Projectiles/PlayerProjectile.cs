using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile: Projectile {
	
	[System.Serializable]
	public enum SpecializedType {
		Unarmored,
		Armor,
		Shield
	}
	public SpecializedType specialized;

	private Rigidbody2D rb;
	//private float damageMultiplier = 2.5f;

	public override void Start() {
		base.Start();
		rb = transform.GetComponent<Rigidbody2D>();
		rb.velocity = transform.up * speed;
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Enemy")) {
			Enemy enemy = collision.GetComponent<Enemy>();
			enemy.TakeDamage(damage);
			Destroy(gameObject);
		}
	}
}

