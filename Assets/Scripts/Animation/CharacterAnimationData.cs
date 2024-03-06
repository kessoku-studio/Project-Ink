using UnityEngine;

[System.Serializable]
public class AttackAnimationData
{
    public AnimationClip startClip;
    public AnimationClip loopClip; // This can be looped multiple times based on the skill level or effect
    public AnimationClip endClip;
    public float animationSpeed = 1.0f; // Default speed
}

[CreateAssetMenu(fileName = "CharacterAnimationData", menuName = "AnimationData/Character")]
public class CharacterAnimationData : ScriptableObject
{
    public AnimationClip spawnAnimation;
    public AnimationClip dieAnimation;
    public AttackAnimationData[] attackAnimations; // Updated to use the new structure
}


