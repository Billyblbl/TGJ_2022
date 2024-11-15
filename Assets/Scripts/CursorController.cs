using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

#nullable enable

public class CursorController : MonoBehaviour {
	public PlayerInput?	controls;
	public float gamepadCursorDist = 0.7f;
	public Transform? gamepadOrigin;

	Camera? cam;

	private void Start() {
		cam = Camera.main;
	}

	Vector3 AxisToScreenSquare(Vector2 input) => input * Mathf.Min(Screen.height, Screen.width) / 2f * gamepadCursorDist + (Vector2)cam!.WorldToScreenPoint((gamepadOrigin!.position));

	bool usingGamepad { get => controls!.currentControlScheme == "Gamepad"; }

	public Vector2 TransformControlSchemeInput(Vector2 input) => cam!.ScreenToWorldPoint(usingGamepad ? AxisToScreenSquare(input) : input);

	private void Update() {
		var input = controls!.actions["Aim"].ReadValue<Vector2>();
		transform.position = TransformControlSchemeInput(input);
	}
}
