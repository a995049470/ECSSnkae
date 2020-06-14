using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

public class MapSystem : SystemBase
{
    protected override void OnStartRunning()
    {
        // var mapEntiy = GetSingletonEntity<MapTag>();
        // BufferFromEntity<MapBufferElement> buffers = GetBufferFromEntity<MapBufferElement>(false);
        // var mapBuffer = buffers[mapEntiy];
        // MapInfoData sizeData = GetSingleton<MapInfoData>();
        // var len = sizeData.height * sizeData.width - 1;
        // NativeArray<MapBufferElement> bufferArray = new NativeArray<MapBufferElement>(len, Allocator.Temp);
        // mapBuffer.AddRange(bufferArray);
        // bufferArray.Dispose();
    }
    protected override void OnUpdate()
    {
        //GetEntityQuery
        Entities.ForEach((in SnakeBodyData bodyData) =>
        {

        }).Schedule();
        Entities.ForEach((in FoodData foodData) =>
        {

        }).Schedule();
    }
}
