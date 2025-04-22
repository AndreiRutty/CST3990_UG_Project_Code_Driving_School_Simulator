using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MirrorSignalManoeuvre : MonoBehaviour
{
    private InputAction leftBlinkerAction;
    private InputAction rightBlinkerAction;
    public InputActionAsset inputActionAsset;
    public Camera povCamera;
    public Animator cameraAnimator;
    public bool isLeftBlinkerOn = false;
    public bool isRightBlinkerOn = false;
    public bool hasCheckedLeftMirror = false;
    public bool hasCheckedRightMirror = false;
    public bool hasCheckedLeftBlindspot = false;
    public bool hasCheckedRightBlindspot = false;
    private bool playLookLeftMirror = false;
    private bool playLookRightMirror = false;
    private bool playLookLeftBlindSpot = false;
    private bool playLookRightBlindSpot = false;

    void Update()
    {
        MirrorandBlindspotManoeuvre();
    }

    private void OnEnable()
    {
        // Get the action map for vehicle controls from the input action asset
        InputActionMap vehicleControlsMap = inputActionAsset.FindActionMap("Vehicle Controls");

        // Retrieve the input actions for left and right blinkers
        leftBlinkerAction = vehicleControlsMap.FindAction("LeftBlinker");
        rightBlinkerAction = vehicleControlsMap.FindAction("RightBlinker");

        // Subscribe the blinker actions to their respective event handlers
        leftBlinkerAction.performed += OnLeftBlinkerPressed;
        rightBlinkerAction.performed += OnRightBlinkerPressed;

        // Enable the input action map to start listening for inputs
        vehicleControlsMap.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe the blinker actions from their respective event handlers
        leftBlinkerAction.performed -= OnLeftBlinkerPressed;
        rightBlinkerAction.performed -= OnRightBlinkerPressed;

        // Disable the blinker actions to stop listening for inputs
        leftBlinkerAction.Disable();
        rightBlinkerAction.Disable();
    }

    private void OnLeftBlinkerPressed(InputAction.CallbackContext context)
    {
        // Toggle the left blinker state and ensure the right blinker is turned off
        isLeftBlinkerOn = !isLeftBlinkerOn;
        isRightBlinkerOn = false;
    }

    private void OnRightBlinkerPressed(InputAction.CallbackContext context)
    {
        // Toggle the right blinker state and ensure the left blinker is turned off
        isRightBlinkerOn = !isRightBlinkerOn;
        isLeftBlinkerOn = false;
    }


    void MirrorandBlindspotManoeuvre()
    {
        // Check if the Left Alt key is pressed to check the left mirror
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            // Mark that the left mirror has been checked
            hasCheckedLeftMirror = true;

            // Toggle the state of looking at the left mirror and ensure right mirror is not active
            playLookLeftMirror = !playLookLeftMirror;
            playLookRightMirror = false;

            // Ensure blind spot checks are not active
            playLookLeftBlindSpot = false;
            playLookRightBlindSpot = false;

            // Call the LookAtMirrors function to perform the left mirror check
            LookAtMirrors("left");
        }

        // Check if the Right Alt key is pressed to check the right mirror
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            // Mark that the right mirror has been checked
            hasCheckedRightMirror = true;

            // Toggle the state of looking at the right mirror and ensure left mirror is not active
            playLookRightMirror = !playLookRightMirror;
            playLookLeftMirror = false;

            // Ensure blind spot checks are not active
            playLookLeftBlindSpot = false;
            playLookRightBlindSpot = false;

            // Call the LookAtMirrors function to perform the right mirror check
            LookAtMirrors("right");
        }

        // Check if the Left Control key is pressed to check the left blind spot
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // Mark that the left blind spot has been checked
            hasCheckedLeftBlindspot = true;

            // Toggle the state of looking at the left blind spot and ensure right blind spot is not active
            playLookLeftBlindSpot = !playLookLeftBlindSpot;
            playLookRightBlindSpot = false;

            // Ensure mirror checks are not active
            playLookLeftMirror = false;
            playLookRightMirror = false;

            // Call the LookAtBlindSpot function to perform the left blind spot check
            LookAtBlindSpot("left");
        }

        // Check if the Right Control key is pressed to check the right blind spot
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            // Mark that the right blind spot has been checked
            hasCheckedRightBlindspot = true;

            // Toggle the state of looking at the right blind spot and ensure left blind spot is not active
            playLookRightBlindSpot = !playLookRightBlindSpot;
            playLookLeftBlindSpot = false;

            // Ensure mirror checks are not active
            playLookLeftMirror = false;
            playLookRightMirror = false;

            // Call the LookAtBlindSpot function to perform the right blind spot check
            LookAtBlindSpot("right");
        }
    }

    void LookAtMirrors(string direction)
    {
        // If the direction is "left", check the left mirror
        if (direction == "left")
        {
            // If looking at the left mirror, play the corresponding animation
            if (playLookLeftMirror)
            {
                cameraAnimator.Play("LookLeftMirror");
            }
            else
            {
                // If not looking at the left mirror, play the "back to left mirror" animation
                cameraAnimator.Play("BackLeftMirror");
            }
        }
        else
        {
            // If the direction is "right", check the right mirror
            if (playLookRightMirror)
            {
                // If looking at the right mirror, play the corresponding animation
                cameraAnimator.Play("LookRightMirror");
            }
            else
            {
                // If not looking at the right mirror, play the "back to right mirror" animation
                cameraAnimator.Play("BackRightMirror");
            }
        }
    }

    void LookAtBlindSpot(string direction)
    {
        // If the direction is "left", check the left blind spot
        if (direction == "left")
        {
            // If looking at the left blind spot, play the corresponding animation
            if (playLookLeftBlindSpot)
            {
                cameraAnimator.Play("LookLeftBlindSpot");
            }
            else
            {
                // If not looking at the left blind spot, play the "back to left blind spot" animation
                cameraAnimator.Play("BackLeftBlindSpot");
            }
        }
        else
        {
            // If the direction is "right", check the right blind spot
            if (playLookRightBlindSpot)
            {
                // If looking at the right blind spot, play the corresponding animation
                cameraAnimator.Play("LookRightBlindSpot");
            }
            else
            {
                // If not looking at the right blind spot, play the "back to right blind spot" animation
                cameraAnimator.Play("BackRightBlindSpot");
            }
        }
    }

}
