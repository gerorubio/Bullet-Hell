using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : Projectile {

	[System.Serializable]
	public enum SpecializedType {
		Unarmored,
		Armor,
		Shield
	}
	public SpecializedType specialized;

	public float ignitionTime;
	public float initialSpeed;

	private Rigidbody2D rb;

	public override void Start() {
		base.Start();
		rb = transform.GetComponent<Rigidbody2D>();
		rb.velocity = transform.up * initialSpeed;
		StartCoroutine(IgnitionDelay());
	}

	IEnumerator IgnitionDelay() {
		yield return new WaitForSeconds(ignitionTime);
		rb.velocity = transform.up * (speed / 2);
		StartCoroutine(SecondIgnitionDelay());
	}

	IEnumerator SecondIgnitionDelay() {
		yield return new WaitForSeconds(ignitionTime);
		rb.velocity = transform.up * speed;
	}

	// Update is called once per frame
	public override void Update() {
		base.Update();
    }

	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Enemy")) {
			Enemy enemy = collision.GetComponent<Enemy>();
			enemy.TakeDamage(damage);
			Destroy(gameObject);
		}
	}
}

