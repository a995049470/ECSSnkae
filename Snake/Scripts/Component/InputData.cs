using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct InputData : IComponentData
{
    // 0 上 1 下 2 左 3右
    public int Value;
}
