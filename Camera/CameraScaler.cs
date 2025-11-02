using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public float baseOrthographicSize = 8f;  // Default orthographic size (adjust this based on your design)
    public float baseAspectRatio = 16f / 9f; // Default aspect ratio

    void Start()
    {
        AdjustCameraSize();
    }

    void AdjustCameraSize()
    {
        float currentAspectRatio = (float)Screen.width / Screen.height;

        // If the current screen is taller (more vertical space), adjust the orthographic size
        Camera.main.orthographicSize = baseOrthographicSize * (baseAspectRatio / currentAspectRatio);
        
        // Prevent over-scaling (ensures it doesn't shrink too much)
        if (Camera.main.orthographicSize < baseOrthographicSize)
        {
            Camera.main.orthographicSize = baseOrthographicSize;
        }
    }
}
