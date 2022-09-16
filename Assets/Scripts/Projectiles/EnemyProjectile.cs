using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile {

	private float x;
	private float y;
	private float direction;
	private float acceleration;
	private float curve;
	private float dirX;
	private float dirY;

	private void FixedUpdate() {
		direction += curve;

		if (speed < 0.2) {
			speed = 0.2f;
		} else {
			speed += acceleration;
		}

		dirX = xDir(direction);
		dirY = yDir(direction);
		transform.GetComponent<Rigidbody2D>().velocity = new Vector3(dirX * speed, dirY * speed, 0);
	}

	internal void Initialize(float _x, float _y, float _direction, float _speed, float _acceleration, float _curve) {
		x = _x;
		y = _y;
		direction = _direction;
		speed = _speed;
		acceleration = _acceleration;
		curve = _curve;
	}

	private float xDir(float angle) {
		float radians = angle * Mathf.PI / 180;
		return Mathf.Cos(radians);
	}

	private float yDir(float angle) {
		float radians = angle * Mathf.PI / 180;
		return -Mathf.Sin(radians);

	}

	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			Player player = collision.GetComponent<Player>();
			player.TakeDamage(1);
			Destroy(gameObject);
		}
	}
}

