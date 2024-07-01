using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAudio : MonoBehaviour
{
    [Header("Actions")]
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource doubleJumpAudio;
    [SerializeField] private AudioSource crouchSlideAudio;
    [SerializeField] private AudioSource grapple;
    [SerializeField] private AudioSource grappleRopeStretch;
    [SerializeField] private AudioSource glide;
    [SerializeField] private AudioSource wallTouch;
    [SerializeField] private AudioSource wallSlide;
    [Header("Walking")]
    [SerializeField] private AudioSource walkingAudio;
    [SerializeField] private AudioSource walking150BPM;
    [SerializeField] private AudioSource walking200BPM;
    

    private Rigidbody playerRB;
    private PlayerKeyBinds playerKeybinds;
    private PlayerMove playerMove;

    // Getters
    public AudioSource Grapple => grapple;
    public AudioSource GrappleRopeStretch => grappleRopeStretch;

    private void Start()
    {
        playerKeybinds = GetComponent<PlayerKeyBinds>();
        playerMove = GetComponent<PlayerMove>();
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        WalkingAudio();
        JumpAudio();
        CrouchSlideAudio();
        GlideAudio();
        WallRunAudio();

    }
    private void WalkingAudio()
    {
        if (Input.GetKey(playerKeybinds.ForwardBtn) || Input.GetKey(playerKeybinds.BackwardBtn) || Input.GetKey(playerKeybinds.LeftBtn) || Input.GetKey(playerKeybinds.RightBtn))
        {
            if (playerMove.Grounded && playerRB.velocity.magnitude < 20 && !Input.GetKey(playerKeybinds.CrouchSlideBtn))
            {
                walkingAudio.enabled = true;
                walking150BPM.enabled = false;
                walking200BPM.enabled = false;
            }
            else if (playerMove.Grounded && playerRB.velocity.magnitude >= 20 && playerRB.velocity.magnitude < 60)
            {
                walkingAudio.enabled = false;
                walking150BPM.enabled = true;
                walking200BPM.enabled = false;
            }
            else if (playerMove.Grounded && playerRB.velocity.magnitude >= 60)
            {
                walkingAudio.enabled = false;
                walking150BPM.enabled = false;
                walking200BPM.enabled = true;
            }
            else
            {
                walkingAudio.enabled = false;
                walking150BPM.enabled = false;
                walking200BPM.enabled = false;
            }
        }
        else
        {
            walkingAudio.enabled = false;
            walking150BPM.enabled = false;
            walking200BPM.enabled = false;
        }
    }

    private void JumpAudio()
    {
        if (Input.GetKeyDown(playerKeybinds.JumpBtn) && playerMove.Grounded)
        {
            jumpAudio.Play();
        }

        if (Input.GetKeyDown(playerKeybinds.JumpBtn) && !playerMove.Grounded && playerMove.CanDoubleJump && playerMove.DoubleJump && !playerMove.Wallrunning)
        {
            doubleJumpAudio.Play();
        }
    }
    private void CrouchSlideAudio()
    {
        if (Input.GetKey(playerKeybinds.CrouchSlideBtn) && playerMove.Grounded && playerRB.velocity.magnitude >= 10)
        {
            crouchSlideAudio.enabled = true;
        }
        else if (!playerMove.Grounded || playerMove.Wallrunning)
        {
            crouchSlideAudio.enabled = false;
        }
    }
    private void GlideAudio()
    {
        if (playerMove.IsGliding) glide.enabled = true;
        else glide.enabled = false;
    }
    private void WallRunAudio()
    {
        if (playerMove.Wallrunning && !playerMove.IsGliding)
        {
            wallTouch.enabled = true;
            wallSlide.enabled = true;
        }
        else
        {
            wallTouch.enabled = false;
            wallSlide.enabled = false;
        }
    }
}
