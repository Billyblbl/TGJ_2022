using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class TopDown2DController : MonoBehaviour {

	public Rigidbody2D?	rb;

	[System.Serializable] public struct MovementStat {
		public float max;
		public float acceleration;
		public float multiplier;

		public float TargetVel(float input) => Mathf.Clamp(input, -1f, 1f) * max * multiplier;
		public Vector2 TargetVel(Vector2 input) => Vector2.ClampMagnitude(input, 1f) * max * multiplier;
		public Vector3 TargetVel(Vector3 input) => Vector3.ClampMagnitude(input, 1f) * max * multiplier;

		public float Apply(float current, float input, float dt) => Mathf.Lerp(current, TargetVel(input), dt * acceleration * multiplier);
		public Vector2 Apply(Vector2 current, Vector2 input, float dt) => Vector2.Lerp(current, TargetVel(input), dt * acceleration * multiplier);
		public Vector3 Apply(Vector3 current, Vector3 input, float dt) => Vector3.Lerp(current, TargetVel(input), dt * acceleration * multiplier);
	}

	public MovementStat lateral;
	public MovementStat angular;
	public Transform?	target;
	public bool aimAtTarget = true;
	public float turnDistanceThreshold = .1f;

	protected Vector2 movement;
	protected float turn;

	protected void UpdateControls(float dt) {
		rb!.velocity = lateral.Apply(rb!.velocity, movement, dt);
		rb!.angularVelocity = angular.Apply(rb!.angularVelocity, turn, dt);
	}

	public float towardsTarget { get => deltaTarget.magnitude > turnDistanceThreshold ? Vector2.SignedAngle(transform.up, deltaTarget) : 0f; }
	public Vector2 deltaTarget { get => (target != null) ? target!.position - transform.position : Vector2.zero ; }

	virtual protected void Update() {
		if (target && target == null) enabled = false;
		else if (target != null && aimAtTarget) turn = towardsTarget;
	}
}
