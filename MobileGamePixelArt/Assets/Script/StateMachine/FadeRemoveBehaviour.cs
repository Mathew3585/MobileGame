using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float FadeTime = 0.5f;
    private float timeElapsed = 0;
    SpriteRenderer spriteRenderer;
    GameObject ObjRmeove;
    Color color;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        ObjRmeove = animator.gameObject;
    }

// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;

        float newAlpha = color.a * (1 - (timeElapsed / FadeTime));

        spriteRenderer.color = new Color(color.r,color.g,color.b, newAlpha);

        if(timeElapsed > FadeTime)
        {
            Destroy(ObjRmeove);
        }
    }
}
