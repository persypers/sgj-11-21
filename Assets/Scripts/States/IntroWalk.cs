using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroWalk : GameState
{
	protected override void OnEnable()
	{
		base.OnEnable();
		Ben.Instance.SetTarget(Ben.Instance.appearAnchor);
		Ben.Instance.Snap();
		Ben.Instance.Reset();

		Ben.Instance.SetTarget(Ben.Instance.normalAnchor);
		Ben.Instance.isWalking = true;
	}

	protected override void OnDisable()
	{
		Ben.Instance.SetTarget(Ben.Instance.normalAnchor);
		Ben.Instance.Snap();
		Ben.Instance.isWalking = true;
		base.OnDisable();
	}

	protected override void Update()
	{
		base.Update();
		if(Ben.Instance.IsTargetReached())
		{
			GameState.SwitchState<MenuState>();
		}
	}
}
