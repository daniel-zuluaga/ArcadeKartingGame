using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackZone : MonoBehaviour
{
    //public bool isGate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CarController car = GetComponent<CarController>();
            car.curTrackZone = this;
            car.zonesPassed++;
        }
    }
}
