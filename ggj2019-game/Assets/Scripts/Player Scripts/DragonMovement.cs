﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class DragonMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float flyForce;

    private bool canFly = true;

    public float flightPower = 100;
    public float increaseRate = 0.5f;
    public float decreaseRate = 1.0f;

    private Rigidbody rb;

    private KeyCode flyKey;

    public AudioClip woosh;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        flyKey = GetComponent<DragonControls>().flyControl;       
    }

    private void LateUpdate()
    {
        GetKeyPress();
    }

    private void GetKeyPress() {
        
        float v = Input.GetAxis("Horizontal");

        // fly only when w pressed
        if (Input.GetKeyDown(flyKey) && canFly)
            Fly();

        transform.position += new Vector3(v * moveSpeed * Time.deltaTime, 0, 0);
    }

    private void Fly() {
        rb.AddForce(Vector3.up * flyForce, ForceMode.Acceleration);

        source.pitch = 0.25f;
        source.PlayOneShot(woosh, 0.3f);
        source.pitch = 1;
        
        // TODO
        // add limit to fly ability
    }
}
