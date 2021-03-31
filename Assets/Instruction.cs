using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Instruction : MonoBehaviour
{
    public InstructionType instructionType;
    public float instructionValue;
    public float distanceToCamera;
    public bool dragging;
    public Vector3 shift;
    public Collider[] colliders;
    public InstructionQueue instructionQueue;

    private void OnMouseDown()
    {
        colliders = new Collider[5];
        dragging = true;
        distanceToCamera = Vector3.Distance(Camera.main.transform.position, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        shift = transform.position - ray.GetPoint(distanceToCamera);
    }

    private void OnMouseUp()
    {
        dragging = false;
        //Colliders.Initialize();
        Physics.OverlapSphereNonAlloc(transform.position, 0.5f, colliders);
        foreach (var collider1 in colliders)
        {
            if (collider1 == null) continue;
            var ok = collider1.gameObject.TryGetComponent<InstructionQueue>(out var iq);
            if (ok)
            {
                Debug.Log("There is an InstructionQueue!");

                if (instructionQueue == iq)
                {
                    iq.ArrangeInstructions();
                    return;
                }
                if (instructionQueue != null) instructionQueue.Dequeue(this);
                
                
                iq.Enqueue(this);
                instructionQueue = iq;
                return;
            }
        }

        if (instructionQueue == null) return;
        
        instructionQueue.Dequeue(this);
        instructionQueue = null;
    }

    void Update()
    {
        if (!dragging) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.SetPositionAndRotation(ray.GetPoint(distanceToCamera) + shift, transform.rotation);
    }
}
