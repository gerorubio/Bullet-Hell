using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundries : MonoBehaviour {
	private float objectWidth;
	private float objectHeight;
	private GameObject quad;
	private float quadWidth, quadHeight;

	// Use this for initialization
	void Start() {
		quad = GameObject.FindGameObjectWithTag("Background");
		quadWidth = quad.GetComponent<MeshRenderer>().bounds.extents.x;
		quadHeight = quad.GetComponent<MeshRenderer>().bounds.extents.y;

		objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
		objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
	}

	private void Update() {
		float posX = Mathf.Clamp(transform.position.x, quad.transform.position.x - quadWidth + objectWidth, quad.transform.position.x + quadWidth - objectWidth);
		float poxY = Mathf.Clamp(transform.position.y, quad.transform.position.y - quadHeight + objectHeight, quad.transform.position.y + quadHeight - objectHeight);
		transform.position = new Vector3(posX, poxY, transform.position.z);
	}

}