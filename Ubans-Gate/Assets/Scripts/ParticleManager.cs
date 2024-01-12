using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    /// <summary>
    /// Can only be used with normal effects!
    /// Unable to play with numeric or text values.
    /// </summary>
    public static ParticleManager Instance { get; private set; }
    public ParticleData particleData;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}