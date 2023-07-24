using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamRotate : MonoBehaviour
{
    public float rotationSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

}
