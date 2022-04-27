using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#nullable enable

public class Health : MonoBehaviour {

	public Gauge gauge;

	public List<(float, float end)> dots = new List<(float, float)>();

	private void Update() {
		gauge.Update(Time.time, Time.deltaTime);

		dots.RemoveAll((dot) => dot.end < Time.time);
		foreach (var (dmg, _) in dots) gauge.current -= dmg * Time.deltaTime;
	}

	public void SelfDestroy(float time = 0f) {
		Destroy(gameObject, time);
	}

}
