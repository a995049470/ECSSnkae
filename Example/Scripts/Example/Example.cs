using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        DoExample();
    }

    private void DoExample()
    {
        NativeArray<float> resultArray = new NativeArray<float>(1, Allocator.TempJob);
        SampleJob job1 = new SampleJob()
        {
            a = 100,
            result = resultArray,
        };

        AnotherJob job2 = new AnotherJob()
        {
            result = resultArray,
        };
        
    
        var handle = job1.Schedule();
        handle.Complete();
        job2.Schedule().Complete();
        var value = job1.result[0];
        Debug.Log($"result : {value}");
        Debug.Log($"job.a : {job1.a}");
        resultArray.Dispose();
    }
}

public struct SampleJob : IJob
{
    public float a;
    public NativeArray<float> result;
    public void Execute()
    {
        result[0] = a;
    }
}


public struct AnotherJob : IJob
{
    public NativeArray<float> result;
    public void Execute()
    {
        result[0] += 100;
    }
}

