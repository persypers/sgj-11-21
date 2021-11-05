using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fancy
{
public class DisableSelf : StateMachineBehaviour {
	public bool onExit = false;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(!onExit) animator.gameObject.SetActive(false);
	}
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(onExit) animator.gameObject.SetActive(false);
	}
}
}