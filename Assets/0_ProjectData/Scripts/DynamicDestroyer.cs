using System;
using UnityEngine;

public class DynamicDestroyer : MonoBehaviour
{
    [Flags]
    public enum EPlatform
    {
        None = 0,
        UnityEditor = 1 << 0,
        Standalone = 1 << 1,
        Android = 1 << 2
    }

    [SerializeField] private EPlatform preserveInPlatforms = (EPlatform)(-1);

    private void Awake()
    {
#if UNITY_EDITOR
        if (!preserveInPlatforms.HasFlag(EPlatform.UnityEditor))
            Destroy(this.gameObject);
#elif UNITY_ANDROID
        if (!preserveInPlatforms.HasFlag(EPlatform.Android))
            Destroy(this.gameObject);
#elif UNITY_STANDALONE
        if (!preserveInPlatforms.HasFlag(EPlatform.Standalone))
            Destroy(this.gameObject);
#endif
    }
}
