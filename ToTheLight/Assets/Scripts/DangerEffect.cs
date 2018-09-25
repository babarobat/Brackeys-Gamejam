using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DangerEffect : MonoBehaviour {

    
    private Animator _anim;
	void Start ()
    {
        _anim = GetComponent<Animator>();
        
	}
	
	public void AnimationTriggerSwitch(string animationName)
    {
        _anim.SetTrigger(animationName);
    }
    public void AnimationSwitchToLoopDanger()
    {
        _anim.SetTrigger("DangerAnimation");
    }
}
