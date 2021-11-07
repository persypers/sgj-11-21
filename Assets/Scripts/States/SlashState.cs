using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class SlashState : CutsceneState
{
	protected override void OnEnable()
	{
		Ben.Instance.SetTarget(Ben.Instance.killAnchor);
		Ben.Instance.Snap();
		Ben.Instance.isWalking = false;
		base.OnEnable();
		//Global.Instance.gameUi.SetActive(false);
		//Global.Instance.mainCanvas.enabled = true;
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
		GameState.SwitchState<ResolveEffectState>();
	}
}
