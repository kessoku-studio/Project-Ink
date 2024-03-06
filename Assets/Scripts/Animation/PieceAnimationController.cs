using System;
using System.Collections;
using UnityEngine;

public class PieceAnimationController : MonoBehaviour
{
    public CharacterAnimationData animationData;
    private Animator animator;
    private AnimatorOverrideController overrideController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetupAnimations();
    }

    private void SetupAnimations()
    {
        if (animator.runtimeAnimatorController is AnimatorOverrideController)
        {
            overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
        }
        else
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = overrideController;
        }

        overrideController["DefaultSpawnAnimation"] = animationData.spawnAnimation;
        overrideController["DefaultDieAnimation"] = animationData.dieAnimation;
        // Assume "DefaultAttackAnimation" is a placeholder for your attacks
        // You would need to switch this dynamically for characters with multiple attacks
    }


    public void PlaySpawnAnimation()
    {
        PlayAnimation(animationData.spawnAnimation);
    }

    public void PlayDieAnimation()
    {
        PlayAnimation(animationData.dieAnimation);
    }

    public void PlayAttackAnimation(int attackIndex, int loopCount = 1)
    {
        if (attackIndex >= 0 && attackIndex < animationData.attackAnimations.Length)
        {
            var attackAnimation = animationData.attackAnimations[attackIndex];

            // Set the animation speed
            animator.speed = attackAnimation.animationSpeed;

            // Assuming you have triggers or a mechanism to play start, loop, and end animations
            // This is a simplified view; your implementation may need to be more complex
            animator.SetTrigger("PlayStartAttack");

            // Optionally loop the middle animation clip
            // You might need a coroutine or another method to handle looping and then transition to the end clip
            StartCoroutine(PlayLoopAnimation(attackAnimation.loopClip, loopCount, () => {
                animator.SetTrigger("PlayEndAttack");
            }));
        }
        else
        {
            Debug.LogWarning("Attack index out of range.");
        }
    }

    IEnumerator PlayLoopAnimation(AnimationClip loopClip, int loopCount, Action onLoopComplete)
    {
        // Note: This is a conceptual example. You might need a custom solution based on your animation setup.
        // Set the loop clip if using an override controller or directly through Animator parameters
        overrideController["DefaultLoopAttackAnimation"] = loopClip;

        // Wait for the loop clip to play the required number of times
        float loopTime = loopClip.length * loopCount / animator.speed;
        yield return new WaitForSeconds(loopTime);

        // Callback when looping is complete
        onLoopComplete?.Invoke();
    }


    private void PlayAnimation(AnimationClip clip)
    {
        // Ensure your Animator has an AnimationClip placeholder to swap with your clip
        // This is a more advanced topic, requiring manual manipulation of the AnimatorOverrideController
        // or direct setting of the AnimationClip at runtime, which might not directly support all use cases.
    }
}
