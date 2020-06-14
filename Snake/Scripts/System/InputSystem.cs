using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class InputSystem : SystemBase
{
    private KeyCode[] m_keycodes;
    protected override void OnCreate()
    {
        m_keycodes = new KeyCode[]
        {
            KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D,
        };
    }

    protected override void OnUpdate()
    {
        int temp = 0;
        for (int i = 0; i < m_keycodes.Length; i++)
        {
            int v = Input.GetKey(m_keycodes[i]) ? 1 : 0;
            temp.SetBit(i, v);
        };
        Entities.ForEach((ref InputData inputData) => 
        {
            inputData.Value = temp;
        }).Schedule();
    }
}
