using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingState : StateMachineBehaviour {

	public AudioClip rocket;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		AudioSource audioSource = animator.gameObject.GetComponent<AudioSource> ();
		audioSource.clip = rocket;
        audioSource.volume = 0.5f;
		audioSource.Play();
	}
}
