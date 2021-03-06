using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class Ben : MonoSingleton<Ben>
{
	public bool isWalking;
	public bool die;
	public bool reachedTarget;

	public float walkSpeed = 2.2f;
	public float catchUpSpeed = 1.1f;
	public float catchUpDistance = 1f;
	public Animator animator;
	Para para;
	public Transform target;
	public Transform appearAnchor;
	public Transform normalAnchor;
	public Transform killAnchor;
	public Transform winAnchor;
	Transform ben;

	protected override void Init()
	{
		base.Init();
		para = Global.Instance.para;
		ben = animator.transform;
	}

	public void Start()
	{
		Snap();
	}

	public void Snap()
	{
		ben.position = target.position;
		reachedTarget = true;
	}

	public bool IsTargetReached()
	{
		return 0.01f > Mathf.Abs(target.position.x - ben.position.x);
	}

	public float delta;
	public float benSpeed;
	public float worldSpeed;

	public void SetTarget(Transform target)
	{
		reachedTarget = false;
		this.target = target;
	}

	public void Reset()
	{
		die = false;
	}
	public void Update()
	{
		animator.SetBool("Die", die);
		animator.SetBool("Walk", isWalking);

		delta = (target.position.x - ben.position.x);
		if(Mathf.Abs(delta) < 0.01f)
		{
			ben.position = target.position;
			delta = 0;
			reachedTarget = true;
		} else {
			delta /= catchUpDistance;
			reachedTarget = false;
		}
		delta = Mathf.Clamp(delta, -1f, 1f);

		if(isWalking)
		{
			benSpeed = catchUpSpeed * delta;
			worldSpeed = walkSpeed - benSpeed;
		} else {
			worldSpeed = - delta * catchUpSpeed;
			benSpeed = -worldSpeed;
		}

		Vector3 bp = ben.position;
		bp.x += Time.deltaTime * benSpeed;
		ben.position = bp;

		para.speed = -worldSpeed;
	}
}
