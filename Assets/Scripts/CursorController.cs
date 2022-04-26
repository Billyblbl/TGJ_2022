using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

#nullable enable

public class CursorController : MonoBehaviour {
	public PlayerInput?	controls;
	public Transform? origin;
	public float gamepadCursorDist = 0.7f;

	Camera? cam;

	private void Start() {
		cam = Camera.main;
	}

	public Vector2 TransformControlSchemeInput(Vector2 input) {
		var offset = Vector2.zero;
		if (origin != null) offset = origin.position;

		if (controls!.currentControlScheme == "Keyboard & Mouse") {
			return cam!.ScreenToWorldPoint(input);
		} else {
			var onViewport = (Vector2.Scale(gamepadCursorDist * input, new Vector2 (1f/cam!.aspect, 1f)) + Vector2.one) / 2f;
			return cam!.ViewportToWorldPoint(onViewport) + new Vector3(offset.x, offset.y, 0);
		}
	}

	private void Update() {
		var input = controls!.actions["Aim"].ReadValue<Vector2>();
		transform.position = TransformControlSchemeInput(input);
	}
}
