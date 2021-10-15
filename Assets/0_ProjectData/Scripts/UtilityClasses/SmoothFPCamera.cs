using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This can be attached to a gameobject with a camera component to make it follow 
/// a VR rig camera, while smoothing some of minor head movements that would make 
/// the camera feed too shaky to be used to record trailers and in-game videos.
/// To prevent this script from smoothing out teleport effects used in the game, 
/// the gameobject that it's part of should be placed in the scene as child 
/// of the VR rig in use.
/// </summary>
public class SmoothFPCamera : MonoBehaviour
{
    [SerializeField, Tooltip("Target to follow")]
    private Transform target;
    [SerializeField, Tooltip("Applies smoothing. If disabled, this camera " +
        "will move exactly as the target does.")]
    private bool smoothFollow = true;
    [SerializeField, Tooltip("Movement smoothing (the lower, the smoother)")]
    private float movementSmoothing = 5F;
    [SerializeField, Tooltip("Rotation smoothing (the lower, the smoother)")]
    private float rotationSmoothing = 6F;
    [SerializeField, Tooltip("When smoothing is enabled, the rotation over the z " +
        "angle (tilting your head to the side) is applied only if the target's z " +
        "value exceeds this value. Setting this to 180 will completely cancel " +
        "any head tilting."), Range(0, 180)]
    private float zRotationThreshold = 30f;

    private void Start()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    void Update()
    {
        if (smoothFollow)
        {
            // Lerps camera position change.
            transform.position = Vector3.Lerp(transform.position, 
                target.position, Time.deltaTime * movementSmoothing);

            Vector3 targetEulerAngles = target.eulerAngles;
            float headTiltAngle;
            if (targetEulerAngles.z < 180)
            {
                headTiltAngle = targetEulerAngles.z;
            }
            else
            {
                headTiltAngle = 360 - targetEulerAngles.z;
            }
            
            if (headTiltAngle < zRotationThreshold)
            {
                targetEulerAngles.z = 0;
            }
            Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
            // Lerps camera rotation change.
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                targetRotation, Time.deltaTime * rotationSmoothing);
        }
        else
        {
            // No smoothing applied.
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
        
    }
}
