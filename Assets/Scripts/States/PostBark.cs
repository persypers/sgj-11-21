using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class PostBark : GameState
{
	float t;
	protected override void OnEnable()
	{
		base.OnEnable();
		var go = Global.Instance.barkLabel;
		go.Show();
		var resolve = Global.Instance.resolveCache;
		var enc = Global.Instance.nextEncounter;
		go.GetComponent<TMPro.TMP_Text>().text = 
			resolve.type == Encounter.ResolveType.Bad ? enc.badBark : 
			(resolve.type == Encounter.ResolveType.Medium ? enc.mediumBark : enc.goodBark);
		t = 0.0f;

		Ben.Instance.isWalking = true;
		Ben.Instance.SetTarget(Ben.Instance.normalAnchor);
	}

	protected override void OnDisable()
	{
		Global.Instance.barkLabel.Hide();
		base.OnDisable();
	}


	protected override void Update()
	{
		base.Update();
		t += Time.deltaTime;
		if(t >= Global.Instance.config.minBarkTime)
		{
			GameState.SwitchState<WalkState>();
		}
	}
}
