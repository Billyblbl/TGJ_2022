using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
	public float speed = 1f;
	private void Update() {
		transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
	}
}
