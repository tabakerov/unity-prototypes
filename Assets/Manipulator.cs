using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manipulator : MonoBehaviour
{
    private bool _moving;
    private bool _grabbing;
    private bool _ready;

    public int grabbingCapacity;
    public float grabbingRadius;
    public Collider[] grabbedColliders;
    private int _grabbedNum;

    public bool IsReady()
    {
        return true;
    } 
    
    public void Grab()
    {
        
        _grabbedNum = Physics.OverlapSphereNonAlloc(transform.position, grabbingRadius, grabbedColliders);
        for (var i = 0; i < _grabbedNum; i++)
        {
            grabbedColliders[i].transform.SetParent(this.transform);
        }
    }

    public void Release()
    {
        Debug.Log("Releasing!");
        transform.DetachChildren();
        for (var i = 0; i < _grabbedNum; i++)
        {
            grabbedColliders[i] = null;
        }
    }

    public void Move(float distance)
    {
        transform.Translate(distance * Vector3.forward, Space.Self);
    }

    public void Rotate(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }
    
    
    void Start()
    {
        grabbedColliders = new Collider[grabbingCapacity];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
