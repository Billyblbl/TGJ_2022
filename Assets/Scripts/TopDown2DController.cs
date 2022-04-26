using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class TopDown2DController : MonoBehaviour {

	public Rigidbody2D?	rb;
	public float speed = 10f;
	public float acceleration = 5f;
	public float speedMultiplier = 1f;
	public float angularSpeed = 10f;
	public float angularAcceleration = 5f;
	public float angularSpeedMultiplier = 1f;
	public Transform?	target;
	public bool aimAtTarget = true;


	[System.Serializable] public struct Ability {
		public string name;
		public MonoBehaviour?[] effects;
		public bool enabled { set {
			foreach (var effect in effects) if (effect != null) effect.enabled = value;
		}}
	}

	public Ability[] abilities = new Ability[0];

	protected Vector2 movement;
	protected float turn;

	protected void UpdateControls(float dt) {
		var targetVelocity = Vector2.ClampMagnitude(movement, 1f) * speed * speedMultiplier;
		rb!.velocity = Vector2.Lerp(rb!.velocity, targetVelocity, dt * acceleration * speedMultiplier);
		var targetAngularVelocity = Mathf.Clamp(turn, -1f, 1f) * angularSpeed * angularSpeedMultiplier;
		rb!.angularVelocity = Mathf.Lerp(rb!.angularVelocity, targetAngularVelocity, dt * angularAcceleration * angularSpeedMultiplier);
	}

	public float towardsTarget { get => Vector2.SignedAngle(transform.up, target!.position - transform.position); }

	virtual protected void Update() {
		if (target != null && aimAtTarget) turn = towardsTarget;
	}
}
