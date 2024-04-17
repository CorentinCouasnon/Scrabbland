using UnityEngine;

public class CameraFollowMouse : MonoBehaviour
{
    [SerializeField] Transform _transform;
    [SerializeField] Camera _camera;
    [SerializeField] float _xRange;
    [SerializeField] float _yRange;
    [SerializeField] float _xOffset;
    [SerializeField] float _yOffset;

    void Update()
    {
        var pointerPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
        
        _transform.position = new Vector3(
            Mathf.Lerp(-_xRange, _xRange, pointerPosition.x) + _xOffset,
            Mathf.Lerp(-_yRange, _yRange, pointerPosition.y) + _yOffset,
            -10
        );
    }
}