using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fancy
{
public class ClearTrigger : StateMachineBehaviour {
	public string trigger;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.ResetTrigger(trigger);
	}
}
}