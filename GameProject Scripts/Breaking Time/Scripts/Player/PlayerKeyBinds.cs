using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyBinds : MonoBehaviour
{
    // Default Inputs
    [Header("Default Inputs")]
    public KeyCode ForwardBtn = KeyCode.W;
    public KeyCode BackwardBtn = KeyCode.S;
    public KeyCode LeftBtn = KeyCode.A;
    public KeyCode RightBtn = KeyCode.D;
    public KeyCode JumpBtn = KeyCode.Space;
    public KeyCode GlideBtn = KeyCode.LeftShift;
    public KeyCode CrouchSlideBtn = KeyCode.LeftControl;
    public KeyCode GrappleBtn = KeyCode.Mouse0;
    public KeyCode F1 = KeyCode.F1;
    public KeyCode F2 = KeyCode.F2;
    public KeyCode F3 = KeyCode.F3;

    [Header("Read Only")]
    public float Xmove = 0f;
    public float Ymove = 0f;
    public bool Jump, Glide, Crouch, CrouchDown, CrouchUp, Grapple;

    private void Start()
    {
        LoadNewInputs();
    }

    private void Update()
    {
        PlayerInputs();
    }
    private void PlayerInputs()
    {
        // Vertical
        if (Input.GetKey(ForwardBtn)) Ymove = 1;
        else if (Input.GetKey(BackwardBtn)) Ymove = -1;
        else Ymove = 0;

        // Horizontal
        if (Input.GetKey(RightBtn)) Xmove = 1;
        else if(Input.GetKey(LeftBtn)) Xmove = -1;
        else Xmove = 0;

        // Jump
        if (Input.GetKeyDown(JumpBtn)) Jump = true;
        if (Input.GetKeyUp(JumpBtn)) Jump = false;

        // Crouch & Slide
        if (Input.GetKey(CrouchSlideBtn)) Crouch = true;
        else Crouch = false;
        if (Input.GetKeyDown(CrouchSlideBtn)) CrouchDown = true;
        else CrouchDown = false;
        if (Input.GetKeyUp(CrouchSlideBtn)) CrouchUp = true;
        else CrouchUp = false;

        // Glide
        if (Input.GetKey(GlideBtn)) Glide = true;
        else Glide = false;

        //// Grapple
        //if (Input.GetKeyDown(GrappleBtn)) Grapple = true;
        //if(Input.GetKeyUp(GrappleBtn)) Grapple = false;
    }

    // Call on Save Button in settings
    public void LoadNewInputs()
    {
        LoadInputs();
    }
    private void LoadInputs()
    {
        // Forward // Backward // Left // Right
        if (PlayerPrefs.HasKey("ForwardBtn"))
        {
            ForwardBtn = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ForwardBtn"));
        }
        if (PlayerPrefs.HasKey("BackwardBtn"))
        {
            BackwardBtn = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BackwardBtn"));
        }
        if (PlayerPrefs.HasKey("LeftBtn"))
        {
            LeftBtn = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftBtn"));
        }
        if (PlayerPrefs.HasKey("RightBtn"))
        {
            RightBtn = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightBtn"));
        }

        // CrouchSlide
        if (PlayerPrefs.HasKey("CrouchSlideBtn"))
        {
            CrouchSlideBtn = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CrouchSlideBtn"));
        }
        // Glide
        if (PlayerPrefs.HasKey("GlideBtn"))
        {
            GlideBtn = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GlideBtn"));
        }
        // Grapple
        if (PlayerPrefs.HasKey("GrappleBtn"))
        {
            GrappleBtn = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GrappleBtn"));
        }
    }
}
