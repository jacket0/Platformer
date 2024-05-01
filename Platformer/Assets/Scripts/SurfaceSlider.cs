using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
	private Vector2 _normal;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		_normal = collision.GetContact(0).normal;
		//Mathf.Clamp(_normal.x, 0, 0.5f);
	}

	public Vector2 Project(Vector2 right)
	{
		return right - Vector2.Dot(right, _normal) * _normal;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Debug.DrawRay(transform.position, _normal * 2, Color.red);
		Gizmos.color = Color.blue;
		Debug.DrawRay(transform.position, Project(transform.right), Color.blue);
	}
}
