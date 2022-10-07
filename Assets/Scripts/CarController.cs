using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CarController : MonoBehaviour
{
    public float acceleration;
    public float turnSpeed;

    public Transform carModel;
    private Vector3 startModelOffset;

    public float groundCheckRate;
    private float lastGroundCheckTime;

    private float curYRot;

    private bool accelateInput;
    private float turnInput;

    public Rigidbody rig;

    private void Start()
    {
        startModelOffset = carModel.transform.localPosition;
    }

    private void Update()
    {
        curYRot += turnInput * turnSpeed * Time.deltaTime;

        carModel.position = transform.position + startModelOffset;
        carModel.eulerAngles = new Vector3(0, curYRot, 0);
    }

    private void FixedUpdate()
    {
        if (accelateInput == true)
        {
            rig.AddForce(carModel.forward * acceleration, ForceMode.Acceleration);
        }
    }

    public void OnAccelerateInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            accelateInput = true;
        }

        else
        {
            accelateInput = false;
        }
    }

    public void OnTurnInput(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
    }


}
