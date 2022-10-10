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

    private bool accelerateInput;
    private float turnInput;

    public Rigidbody rig;

    private void Start()
    {
        startModelOffset = carModel.transform.localPosition;
        GameManager.instance.cars.Add(this);
    }

    private void Update()
    {
        // calculate the amount we can turn based on the dot product between our velocity and facing direction
        float turnRate = Vector3.Dot(rig.velocity.normalized, carModel.forward);
        turnRate = Mathf.Abs(turnRate);

        curYRot += turnInput * turnSpeed * turnRate * Time.deltaTime;

        carModel.position = transform.position + startModelOffset;
        //carModel.eulerAngles = new Vector3(0, curYRot, 0);

        CheckGround();

    }

    private void FixedUpdate()
    {
        if (accelerateInput == true)
        {
            rig.AddForce(carModel.forward * acceleration, ForceMode.Acceleration);
        }
    }

    // rotate with the surface below us
    void CheckGround()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, -0.75f, 0), Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            carModel.up = hit.normal;
        }
        else
        {
            carModel.up = Vector3.up;
        }

        carModel.Rotate(new Vector3(0, curYRot, 0), Space.Self);
    }

    // called when we press down the accelerate input
    public void OnAccelerateInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            accelerateInput = true;
        else
            accelerateInput = false;
    }

    // called when we modify the turn input
    public void OnTurnInput(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
    }


}
