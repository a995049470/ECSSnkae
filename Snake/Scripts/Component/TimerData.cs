using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct TimerData : IComponentData
{
    public float currnetTime;
    public float targetTime;
}
