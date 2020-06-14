using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class SnakeDirSystem : SystemBase
{
    
    protected override void OnUpdate()
    {
        float delateTime = UnityEngine.Time.deltaTime;
        Entities.WithAll<SnkaeHeadTag>().ForEach((ref DirData dirData, ref InputData input) => 
        {
            int v = input.Value;
            int2 dir = new int2(0, 0);
            if(v.GetBit(0) == 1)
            {
                dir.y = 1;
            }
            else if(v.GetBit(1) == 1)
            {
                dir.y = -1;
            }
            else if(v.GetBit(2) == 1)
            {
                dir.x = -1;
            }
            else if(v.GetBit(3) == 1)
            {
                dir.x = 1;
            }
            else
            {
                dir = dirData.dir;
            }
            var res = dir * dirData.dir;
            if(res.x != 0 || res.y != 0)
            {
                return;
            }
            dirData.dir = dir;
        }).Schedule();
    }
}
