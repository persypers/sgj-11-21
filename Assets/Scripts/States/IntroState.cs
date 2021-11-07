using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : CutsceneState
{
	protected override void OnEnable()
	{
		Ben.Instance.SetTarget(Ben.Instance.appearAnchor);
		Ben.Instance.Snap();
		Ben.Instance.Reset();
		cutsceneEnded = false;
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	public bool cutsceneEnded;
	protected override void Update()
	{
		base.Update();
		if(Global.Instance.config.skipIntro)
		{
			GameState.SwitchState<IntroWalk>();
		}
	}

	protected override void OnCutsceneEnd()
	{
		GameState.SwitchState<IntroWalk>();
	}
}
