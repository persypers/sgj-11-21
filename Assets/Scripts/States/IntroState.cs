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
		Ben.Instance.SetTarget(Ben.Instance.normalAnchor);
		Ben.Instance.Snap();
		Ben.Instance.isWalking = true;
		base.OnDisable();
	}

	public bool cutsceneEnded;
	protected override void Update()
	{
		base.Update();
		if(cutsceneEnded && Ben.Instance.IsTargetReached() || Global.Instance.config.skipIntro)
		{
			GameState.SwitchState<MenuState>();
		}
	}

	protected override void OnCutsceneEnd()
	{
		cutsceneEnded = true;
		Ben.Instance.SetTarget(Ben.Instance.normalAnchor);
		Ben.Instance.isWalking = true;
	}
}
