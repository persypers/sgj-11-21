using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class GuiltyClashState : CutsceneState
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
	}

	protected override void OnCutsceneEnd()
	{
		GameState.SwitchState<SlashState>();
	}
}
