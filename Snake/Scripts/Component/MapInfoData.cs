using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct MapInfoData : IComponentData
{
    public int width;
    public int height;
    public int2 startPoint;
}
