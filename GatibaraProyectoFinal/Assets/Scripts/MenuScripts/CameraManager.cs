using UnityEngine;
using Unity.Cinemachine;
public class CameraManager : MonoBehaviour
{
    public CinemachineCamera leftCamera;
    public CinemachineCamera rightCamera;
    public CinemachineCamera firsPersonCamera;

    private void Start()
    {
        SetCamera(leftCamera);
    }
    public void SetCamera(CinemachineCamera camera)
    {
        leftCamera.Priority = 0;
        rightCamera.Priority = 0;
        firsPersonCamera.Priority = 0;

        camera.Priority = 10;
    }
    public void LeftCamera() => SetCamera(leftCamera);
    public void RightCamera() => SetCamera(rightCamera);
    public void FirstPersonCamera() => SetCamera(firsPersonCamera);
}
