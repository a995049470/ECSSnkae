using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct FoodData : IComponentData
{ 
    public int2 pos;
    //public bool isEat;
}
