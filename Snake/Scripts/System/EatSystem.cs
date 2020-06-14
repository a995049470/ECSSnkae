using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(SnakeMoveSystem))]
public class EatSystem : SystemBase
{
    private BeginSimulationEntityCommandBufferSystem m_commandBufferSystem;
    private Entity m_bodyEntityPrefab;
    private Random m_random;
    protected override void OnStartRunning()
    {
        m_commandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        m_bodyEntityPrefab = ResManager.Instance.GetEntityPrefab("Prefabs/SnakBody");
        m_random = new Random((uint)System.DateTime.Now.Second);
    }
    protected override void OnUpdate()
    {
        bool isEatFood = false;
        Entity headEntity = GetSingletonEntity<SnkaeHeadTag>();
        SnakeBodyData headBody = GetComponent<SnakeBodyData>(headEntity);
        var entityCommandBuffer = m_commandBufferSystem.CreateCommandBuffer();
        
        Entities.ForEach((Entity enitity, ref FoodData food) =>
        {
            
            if (food.pos.x == headBody.pos.x &&
                food.pos.y == headBody.pos.y)
            {
                isEatFood = true;
            }
             if (isEatFood)
            {
                
                var finalEnitiy = headEntity;
                var finalBody = GetComponent<SnakeBodyData>(finalEnitiy);
                while (finalBody.nextEntity != Entity.Null)
                {
                    finalEnitiy = finalBody.nextEntity;
                    finalBody = GetComponent<SnakeBodyData>(finalEnitiy);
                }
                Entity bodyEntity;
                bodyEntity = entityCommandBuffer.Instantiate(m_bodyEntityPrefab);
                var body = finalBody;
                finalBody.nextEntity = bodyEntity;
                entityCommandBuffer.SetComponent(finalEnitiy, finalBody);
                body.nextEntity = Entity.Null;
                entityCommandBuffer.SetComponent(bodyEntity, body);

                var mpaData =  m_commandBufferSystem.GetSingleton<MapInfoData>();
                var pos = food.pos;
                pos.x = m_random.NextInt(0, mpaData.width);
                pos.y = m_random.NextInt(0, mpaData.height);
                food.pos = pos;
            }

        }).WithoutBurst().Run();
        m_commandBufferSystem.AddJobHandleForProducer(this.Dependency);
       
    }

}
