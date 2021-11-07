using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;
using UnityEngine.Playables;

public class CutsceneState : GameState
{
	public PlayableDirector cutscene;
	protected override void OnEnable()
	{
		base.OnEnable();
		//Global.Instance.mainCanvas.enabled = false;
		cutscene.gameObject.SetActive(true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		cutscene.gameObject.SetActive(false);
		//Global.Instance.mainCanvas.enabled = true;
	}

	protected override void Update()
	{
		base.Update();
		if(Global.Instance.config.skipCutscenes)
		{
			OnCutsceneEnd();
			return;
		}
		if(cutscene.state != PlayState.Playing)
		{
			OnCutsceneEnd();
		}
	}

	protected virtual void OnCutsceneEnd()
	{

	}
}
