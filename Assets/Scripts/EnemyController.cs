using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#nullable enable

public class EnemyController : TopDown2DController {
	public AbilityController? abilityController;

	private void OnEnable() {
		target = FindObjectOfType<PlayerController>().transform;
	}

	public float targetDistance = 2f;
	public bool	maintainDistance = false;
	public float aimCone = 20f;
	public float maxAttackRange = 4f;

	public float nearRange = 1f;
	Collider2D[]	nearAttacks = new Collider2D[0];

	public float targetRangeWeight = 1f;
	public float attacksWeight = 1f;

	Vector2 targetMove { get => (deltaTarget.magnitude > targetDistance || maintainDistance) ? deltaTarget - deltaTarget.normalized * targetDistance : Vector2.zero; }

	Vector2 ResolveMovement() {

		Vector2 pos = transform.position;
		var awayFromAttacks = nearAttacks.Select(c => {
			var delta = c.ClosestPoint(pos) - pos;
			return -delta / Mathf.Clamp(delta.magnitude * delta.magnitude * delta.magnitude, float.Epsilon, float.MaxValue);
		}).Aggregate(Vector2.zero, (a, b) => a+b) / Mathf.Clamp(nearAttacks.Length, 1, int.MaxValue);

		return Vector2.ClampMagnitude(
			targetMove * targetRangeWeight +
			awayFromAttacks * attacksWeight,
		1f);
	}

	private void OnDrawGizmos() {
		Vector3 vel = rb!.velocity;
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(transform.position, transform.position + vel);
		Gizmos.color = Color.cyan;
		foreach (var collider in nearAttacks) if (collider != null) {
			Gizmos.DrawLine(transform.position, collider.transform.position);
		}
	}

	bool aimedShot { get {
		var delta = target!.position - transform.position;
		if (delta.magnitude > maxAttackRange) return false;
		var angle = Vector2.Angle(delta, transform.up);
		return angle * 2 < aimCone;
	} }

	override protected void Update() {
		base.Update();
		if (abilityController!.abilities.Length >= 0) abilityController.TrySetActivity(0, aimedShot);
	}

	Collider2D[]	ProbeNearAttacks() {
		return Physics2D.OverlapCircleAll(transform.position, nearRange, LayerMask.GetMask("Attack")).ToArray();
	}

	private void FixedUpdate() {
		nearAttacks = ProbeNearAttacks();
		movement = ResolveMovement();
		UpdateControls(Time.fixedDeltaTime);
	}

}
