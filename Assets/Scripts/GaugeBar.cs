using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

[RequireComponent(typeof(RectTransform))]
public class GaugeBar : MonoBehaviour {

	public RectTransform?	fill;

	private void OnValidate() {
		fill = GetComponent<RectTransform>();
	}

	public float value { set {
		fill!.localScale = new Vector3(value, fill.localScale.y, fill.localScale.z);
	}}
}
