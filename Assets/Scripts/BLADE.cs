using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLADE : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider BladeCollider;
    private bool Slicing;
    Camera mainCamera;
    public float minSliceVelocity = 0.01f;

    public float sliceforce = 5f;
    private TrailRenderer bladetrail;

    public Vector3 direction { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
        BladeCollider = GetComponent<Collider>();
        bladetrail = GetComponentInChildren<TrailRenderer>();
    }


    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
            //BladeCollider.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0)) {
            StopSlicing();
            //BladeCollider.enabled = false;
        } else if (Slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        transform.position = newPosition;

        Slicing = true;
        BladeCollider.enabled = true;

        bladetrail.enabled = true;
        bladetrail.Clear();
    }



    private void StopSlicing()
    {
        Slicing = false;
        BladeCollider.enabled = false;
        bladetrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float Velocity = direction.magnitude / Time.deltaTime;
        BladeCollider.enabled = Velocity > minSliceVelocity;

        transform.position = newPosition;
    }



}
