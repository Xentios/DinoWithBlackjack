using UnityEngine;

public class FrameLimiter : MonoBehaviour
{
    [SerializeField] int targetFPS = 60;

    void Awake()
    {
        Application.targetFrameRate = targetFPS;
        QualitySettings.vSyncCount = 0;
    }
}