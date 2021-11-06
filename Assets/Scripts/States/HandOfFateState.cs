using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class HandOfFateState : GameState
{
	protected override void OnEnable()
	{
		base.OnEnable();
		Global.Instance.Draw();
		StartCoroutine(Coroutine());
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	public IEnumerator Coroutine()
	{
		yield return new WaitForSeconds(1.0f);
		GameState.SwitchState<JudgementState>();
	}

	protected override void Update()
	{
		base.Update();
	}
}