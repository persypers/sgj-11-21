using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Para : MonoBehaviour
{
	public float speed = 0f;

	ParaLayer[] layers;
	public void Start()
	{
		layers = transform.GetComponentsInChildren<ParaLayer>();
	}

	public void Update()
	{
		for(int i = 0; i < layers.Length; i++)
		{
			layers[i].Move(Time.deltaTime * speed);
		}
	}
}
