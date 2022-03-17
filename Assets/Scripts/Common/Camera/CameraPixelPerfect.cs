using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Common
{
[RequireComponent(typeof(Camera))]
public class CameraPixelPerfect : MonoBehaviour
{
    public Camera SceneCamera => sceneCamera;

    [SerializeField] private Camera sceneCamera;

    private Resolution _resolution;
    private TickHandler _tickHandler;

    private const float PPU = 64;
    private const float TARGET_CAMERA_HALF_WIDTH = 3.6f;

    public void Initialize()
    {
       //AdjustCameraFOV();
    }

    private float CalculatePixelPerfectCameraSize(Resolution res)
    {
        float targetWidth = 2f * TARGET_CAMERA_HALF_WIDTH;
        float ratio = (float)res.width / res.height;

        float width = PPU * targetWidth;
        var ratioTarget = res.width / width;

        float ratioSnapped = Mathf.Ceil(ratioTarget);
        float ratioSnappedPrevious = ratioSnapped - 1;
        ratioTarget = (1/ratioTarget - 1 / ratioSnapped < 1/ratioSnappedPrevious - 1 / ratioTarget) ? ratioSnapped : ratioSnappedPrevious;
        if (ratioSnapped <= 1)
        {
            ratioTarget = 1;
        }
        
        float ratioHorizontal = 0;
        float ratioVertical = 0;

        float ratioMin = Mathf.Max(ratioHorizontal, ratioVertical);
        ratioMin = Mathf.Ceil(ratioMin);
        
        float ratioUsed = Mathf.Max(ratioMin, ratioTarget);

        float horizontalFOV = res.width / (PPU * ratioUsed);
        float verticalFOV = horizontalFOV / ratio;

        return verticalFOV / 2;
    }

    private void AdjustCameraFOV()
    {
        _resolution = new Resolution();
        _resolution.width = sceneCamera.pixelWidth;
        _resolution.height = sceneCamera.pixelHeight;
        _resolution.refreshRate = Screen.currentResolution.refreshRate;

        float size = CalculatePixelPerfectCameraSize(_resolution);

        sceneCamera.orthographicSize = size;
    }

    [Button]
    public void LateTick()
    {
        if (_resolution.width != sceneCamera.pixelWidth || _resolution.height != sceneCamera.pixelHeight)
            AdjustCameraFOV();
	}

}
}
