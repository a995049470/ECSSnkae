using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
// public class WaveSystem : ComponentSystem
// {
//     protected override void OnUpdate()
//     {
//         Entities.ForEach((ref Translation trans, ref MoveSpeed moveSpeed, ref WaveData waveData)=>
//         {
            
//             var z = waveData.amplitude * math.sin((float)Time.ElapsedTime * moveSpeed.Value + 
//                 trans.Value.x * waveData.xOffset + trans.Value.y * waveData.yOffset);
//             trans.Value = new float3(trans.Value.x, trans.Value.y, z);
//         });
//     }
// }

public class WaveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float elapsedTime = (float)Time.ElapsedTime;
        Entities.ForEach((ref Translation trans, ref MoveSpeed moveSpeed, ref WaveData waveData)=>
        {
            
            var z = waveData.amplitude * math.sin(elapsedTime * moveSpeed.Value + 
                trans.Value.x * waveData.xOffset + trans.Value.y * waveData.yOffset);
            trans.Value = new float3(trans.Value.x, trans.Value.y, z);
        }).ScheduleParallel();
    }
}

//只在主线程执行
// [AlwaysSynchronizeSystem]
// public class WaveSystem : JobComponentSystem
// {
//     protected override JobHandle OnUpdate(JobHandle inputDeps)
//     {
//         float elapsedTime = (float)Time.ElapsedTime;
//         Entities.ForEach((ref Translation trans, in MoveSpeed moveSpeed, in WaveData waveData)=>
//         {
            
//             var z = waveData.amplitude * math.sin(elapsedTime * moveSpeed.Value + 
//                 trans.Value.x * waveData.xOffset + trans.Value.y * waveData.yOffset);
//             trans.Value = new float3(trans.Value.x, trans.Value.y, z);
//         }).Run();
//         return default;
//     }
// }


// public class WaveSystem : JobComponentSystem
// {
//     protected override JobHandle OnUpdate(JobHandle inputDeps)
//     {
//         float elapsedTime = (float)Time.ElapsedTime;
//         var handle = Entities.ForEach((ref Translation trans, in MoveSpeed moveSpeed, in WaveData waveData)=>
//         {
            
//             var z = waveData.amplitude * math.sin(elapsedTime * moveSpeed.Value + 
//                 trans.Value.x * waveData.xOffset + trans.Value.y * waveData.yOffset);
//             trans.Value = new float3(trans.Value.x, trans.Value.y, z);
//         }).Schedule(inputDeps);
//         return handle;
//     }
// }
