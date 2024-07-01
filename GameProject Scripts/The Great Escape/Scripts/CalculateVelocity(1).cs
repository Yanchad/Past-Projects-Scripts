using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateVelocity : MonoBehaviour
{

    [SerializeField] private float smoothTime = 0.1f;

    private Vector3 lastPos;
    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private Vector3 velocitySmoothed;

    public Vector3 Velocity { get => velocity; }

    public Vector3 VelocitySmoothed { get => velocitySmoothed; }
    public Vector3 InverseVelocity { get => transform.InverseTransformDirection(velocity); }
    public Vector3 InverseVelocitySmoothed { get => transform.InverseTransformDirection(velocitySmoothed); }

    private Vector3 smoothVel;

    private Quaternion previousRotation;

    [SerializeField]
    private Vector3 angularVelocity;


    public Vector3 AngularVelocity { get => angularVelocity; }
    
    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement velocity
        velocity = (transform.position - lastPos) / Time.deltaTime;
        velocitySmoothed = Vector3.SmoothDamp(velocitySmoothed, velocity, ref smoothVel, smoothTime);
        lastPos = transform.position;
        RotationVelocity();
    }

    private void RotationVelocity()
    {
        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);
        previousRotation = transform.rotation;
        deltaRotation.ToAngleAxis(out var angle, out var axis);
        angle *= Mathf.Deg2Rad;
        if (angle < 0.001f)
        {
            axis.x = 0f;
            axis.y = 0f;
            axis.z = 0f;
        }
        angularVelocity = (1.0f / Time.deltaTime) * angle * axis;
        if (angularVelocity.y == float.NaN) Debug.Log("Nan");
    }
}
