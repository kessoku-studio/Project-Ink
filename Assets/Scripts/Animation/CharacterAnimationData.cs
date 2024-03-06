using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAnimationData", menuName = "AnimationData/Character")]
public class CharacterAnimationData : ScriptableObject
{
    public AnimationClip spawnAnimation;
    public AnimationClip dieAnimation;
    public AnimationClip[] attackAnimations; // Assuming characters can have multiple attacks or skills
}

