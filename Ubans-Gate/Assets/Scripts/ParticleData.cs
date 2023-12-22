using UnityEngine;

[CreateAssetMenu(fileName = "Particle Data", menuName = "Particle Data", order = 1)]
public class ParticleData : ScriptableObject
{
    /// <summary>
    /// Can only be used with normal effects!
    /// Unable to play with numeric or text values.
    /// </summary>
    public GameObject clickEffectPrefab;
}