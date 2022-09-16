using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour {
	public float scrollSpeed;
	public Material[] materials;

	private Renderer rend;

	void Start() {
		rend = transform.GetComponent<Renderer>();
		rend.material = materials[UnityEngine.Random.Range(0, materials.Length)];
	}

	void Update() {
		float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2(0, y);
		rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
