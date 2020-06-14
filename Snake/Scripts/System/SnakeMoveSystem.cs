using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class SnakeMoveSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem m_commandBufferSystem;
    protected override void OnStartRunning()
    {
        m_commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var t = new ReadOnlyValue<ComponentDataFromEntity<SnakeBodyData>>();
        t.Value = GetComponentDataFromEntity<SnakeBodyData>(true);
        var deltaTime = UnityEngine.Time.deltaTime;
        var entityCommandBuffer = m_commandBufferSystem.CreateCommandBuffer();
        var mapInfoData = GetSingleton<MapInfoData>();
        Entities.WithAll<SnkaeHeadTag>().ForEach((Entity entity, ref TimerData timer, in DirData dirData) => 
        {
            timer.currnetTime += deltaTime;
            if(timer.currnetTime < timer.targetTime)
            {
                return;
            }
            timer.currnetTime = 0;
            Entity curEntity = entity;
            var bodyData = t.Value[curEntity];
            int2 lastpos = bodyData.pos;
            bodyData.lastpos = bodyData.pos;
            int2 curpos = bodyData.pos + dirData.dir;
            curpos.x = (curpos.x + mapInfoData.width) % mapInfoData.width;
            curpos.y = (curpos.y + mapInfoData.height) % mapInfoData.height;
            bodyData.pos = curpos;
            entityCommandBuffer.SetComponent(curEntity, bodyData);
            curEntity = bodyData.nextEntity;
            while (curEntity != Entity.Null)
            {
                bodyData = t.Value[curEntity];
                bodyData.lastpos = bodyData.pos;
                curpos = bodyData.pos;
                bodyData.pos = lastpos;
                entityCommandBuffer.SetComponent(curEntity, bodyData);
                lastpos = curpos;
                curEntity = bodyData.nextEntity;
            }
        }).Schedule();
        m_commandBufferSystem.AddJobHandleForProducer(this.Dependency);
    }

}
