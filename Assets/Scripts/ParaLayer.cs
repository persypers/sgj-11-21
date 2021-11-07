using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaLayer : MonoBehaviour
{
	public float length = 19.2f;
	public float factor = 1.0f;

	public float startPos;

	float pos = 0;
	public void Start()
	{
		pos = startPos;
		Move();
	}
	public void Move(float delta = 0f)
	{
		delta *= factor;
		pos += delta;
		while(pos >= length)
		{
			pos -= length;
		}
		while(pos < -length)
		{
			pos += length;
		}
		Vector3 p = transform.position;
		p.x = pos;
		transform.position = p;
	}

	public void Pop()
	{
		while(pos < 0)
		{
			pos += length;
			Vector3 p = transform.position;
			p.x = pos;
			transform.position = p;
		}
	}
}
