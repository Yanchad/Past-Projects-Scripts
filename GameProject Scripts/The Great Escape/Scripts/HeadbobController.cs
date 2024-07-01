using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class HeadbobController : MonoBehaviour
{
    [SerializeField] private bool enable = true;

    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.025f;
    [SerializeField, Range(0, 30)] private float frequency = 10.0f;

    [SerializeField] private new Transform camera = null;
    [SerializeField] private Transform cameraHolder = null;

    CalculateVelocity velocity;
    GroundCheck groundCheck;

    private float toggleSpeed;
    private Vector3 startPos;

    // Start is called before the first frame update
    private void Start()
    {
        groundCheck = transform.root.GetComponent<GroundCheck>();
        velocity = GetComponent<CalculateVelocity>();
        startPos = camera.localPosition;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(velocity.Velocity.x, 0, velocity.Velocity.z).magnitude;

        if (speed < toggleSpeed) return;
        if (!groundCheck.IsGrounded) return;
        camera.localPosition = HeadbobMotion();
    }

    private void ResetPosition()
    {
        if (camera.localPosition == startPos) return;
        camera.localPosition = Vector3.Lerp(camera.localPosition, startPos, 1 * Time.deltaTime);
    }

    private Vector3 HeadbobMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15.0f;
        return pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable) return;

        CheckMotion();
        ResetPosition();
        //camera.LookAt(FocusTarget());
    }
}
