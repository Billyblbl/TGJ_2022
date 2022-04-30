using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#nullable enable

public class Parallax : MonoBehaviour {

	public Renderer?	rd;
	public Vector2	parallaxScale;

	Vector2 Vec2MemberWiseModulo(Vector2 A, Vector2 B) => new Vector2(A.x % B.x, A.y % B.y);

	private void Update() {
		transform.localPosition = Vec2MemberWiseModulo(Vector2.Scale(-transform.parent.localPosition, parallaxScale), rd?.bounds.size ?? Vector2.one * float.MaxValue);
	}

}
