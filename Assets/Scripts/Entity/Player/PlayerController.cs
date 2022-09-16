using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 5f;

	private Rigidbody2D rb;
	private Vector2 movement;
	private Player player;

	private void Start() {
		rb = transform.GetComponent<Rigidbody2D>();
		player = transform.GetComponent<Player>();
	}

	private void Update() {
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		if (Input.GetKey("space")) {
			player.Shoot();
		}

		if (Input.GetKeyDown(KeyCode.U)) {
			player.ChangeWeapon();
		}

		if (Input.GetKeyDown(KeyCode.I)) {
			player.ChangeAbility();
		}

		if (Input.GetKeyDown(KeyCode.O)) {
			player.CastSkill();
		}

		if (Input.GetKey(KeyCode.LeftShift)) {
			movement.x /= 2;
			movement.y /= 2;
		}
	}

	private void FixedUpdate() {
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}
}
