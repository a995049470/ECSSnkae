using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
[UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
public class UpdateTranslationSystem : SystemBase
{
    protected override void OnStartRunning()
    {
    }
    protected override void OnUpdate()
    {
        var mapData = GetSingleton<MapInfoData>();
        Entities.ForEach((ref Translation translation, in SnakeBodyData body) => 
        {
           var pos = translation.Value;
           pos.x = body.pos.x + mapData.startPoint.x;
           pos.y = body.pos.y + mapData.startPoint.y;
           translation.Value = pos;
        }).Schedule();
        Entities.ForEach((ref Translation translation, in FoodData food)=>
        {
           var pos = translation.Value;
           pos.x = food.pos.x + mapData.startPoint.x;
           pos.y = food.pos.y + mapData.startPoint.y;
           translation.Value = pos;
        }).Schedule();
    }
}
