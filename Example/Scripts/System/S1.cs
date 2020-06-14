using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class S1 : SystemBase
{
    // private EntityQuery m_Query;
    // protected override void OnCreate()
    // {
    //     var queryDescription = new EntityQueryDesc
    //     {
    //         All = new ComponentType[] {
    //           ComponentType.ReadWrite<C1>(),
    //           ComponentType.ReadWrite<C2>(),
    //        },
    //         Options = EntityQueryOptions.FilterWriteGroup
    //     };
    //     m_Query = GetEntityQuery(queryDescription);
    // }
    protected override void OnUpdate()
    {
        float elapsedTime = (float)Time.ElapsedTime;
        Entities
         .WithEntityQueryOptions(EntityQueryOptions.FilterWriteGroup)
         .ForEach((ref C1 c1, ref C2 w) =>
        {
            UnityEngine.Debug.Log($"{c1.Value}");
        }).Run();
    }
}

