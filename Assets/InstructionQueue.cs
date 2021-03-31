using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InstructionQueue : MonoBehaviour
{

    public List<Instruction> instructions;

    private Vector3 _originPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        _originPoint = transform.position;
        instructions = new List<Instruction>();
    }

    public void Enqueue(Instruction instruction)
    {
        instructions.Add(instruction);
        Resize();
        ArrangeInstructions();
    }

    public void Dequeue(Instruction instruction)
    {
        instructions.Remove(instruction);
        Resize();
    }

    public void ArrangeInstructions()
    {
        var i = 0;
        foreach (var instruction in instructions)
        {
            instruction.gameObject.transform.SetPositionAndRotation(i++ * Vector3.up + _originPoint, Quaternion.identity);
        }
    }

    public void Resize()
    {
        transform.SetPositionAndRotation(((float)instructions.Count / 2.0f) * Vector3.up + _originPoint - new Vector3(0f, 0.5f, 0f), transform.rotation);
        transform.localScale = new Vector3(1, Mathf.Max(1f, instructions.Count), 1f);
    }
    

}
