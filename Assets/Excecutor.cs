using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excecutor : MonoBehaviour
{
    public bool paused = true;
    public InstructionQueue instructionQueue;

    public Manipulator manipulator;

    public int instructionIdx;

    public float delay;

    private bool _executing;

    void StopExecuting()
    {
        _executing = false;
    }
    
    private void Update()
    {
        if (paused) return;
        if (_executing) return;
        if (instructionQueue.instructions.Count == 0) return;

        if (instructionIdx == instructionQueue.instructions.Count) instructionIdx = 0;
        DecodeInstruction(instructionQueue.instructions[instructionIdx]);
        instructionIdx++;
        _executing = true;
        Invoke("StopExecuting", delay);
    }

    void DecodeInstruction(Instruction instruction)
    {
        switch (instruction.instructionType)
        {
            case InstructionType.Grab:
                manipulator.Grab();
                break;
            case InstructionType.Move:
                manipulator.Move(instruction.instructionValue);
                Debug.Log($"Moving {instruction.instructionValue.ToString()}");
                break;
            case InstructionType.Release:
                manipulator.Release();
                break;
            case InstructionType.Rotate:
                manipulator.Rotate(instruction.instructionValue);
                break;
            default: return;
        }
    }
}
