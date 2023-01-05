using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 100f; 
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;


//
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem sideThrusterRight;
    [SerializeField] ParticleSystem sideThrusterLeft;

//
    public AudioSource audioSource; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
           TurnLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
           TurnRight();
        }
        else
        {
           StopTurning();
        }
    }

    void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    void StopThrust()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    void TurnLeft()
    {
        ApplyRotation(rotationThrust);
        if (!sideThrusterRight.isPlaying)
        {
            sideThrusterRight.Play();
        }
    }
    void TurnRight()
    {
        ApplyRotation(-rotationThrust);
        if (!sideThrusterLeft.isPlaying)
        {
            sideThrusterLeft.Play();
        }
    }
    void StopTurning()
    {
        sideThrusterRight.Stop();
        sideThrusterLeft.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation caused by physic/ collision, so we can rotate manually
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation, so the physics system can tke over
    }
}
