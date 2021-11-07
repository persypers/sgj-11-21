using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class InnocentClashState : GameState
{
	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void Update()
	{
		base.Update();
		GameState.SwitchState<ResolveEffectState>();
	}
}
