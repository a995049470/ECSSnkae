using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct SnakeBodyData : IComponentData
{
    public int2 pos;
    public int2 lastpos;
    //public Entity lastEntity;
    public Entity nextEntity;
}
