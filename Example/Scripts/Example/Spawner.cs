using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public class Spawner : MonoBehaviour
{
    [SerializeField] Mesh m_unitMesh;
    [SerializeField] Material m_unitMaterial;
    [SerializeField] GameObject m_goPrefab;
    [SerializeField] int m_xSize = 10;
    [SerializeField] int m_ySize = 10;
    [Range(0.1f, 2f)]
    [SerializeField] float m_spacing = 1f;
    
    Entity m_entityPrefab;
    World m_defaultWordld;
    EntityManager m_entityManager;

    private void Start()
    {
        //MakeEntity();
        m_defaultWordld = World.DefaultGameObjectInjectionWorld;
        m_entityManager = m_defaultWordld.EntityManager;

        var settings = GameObjectConversionSettings.FromWorld(m_defaultWordld, null);
        m_entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(m_goPrefab, settings);

        InstantiateEntityGrid(m_xSize, m_ySize, m_spacing);
    }

    private void InstantiateEntity(float3 pos)
    {
        var entity = m_entityManager.Instantiate(m_entityPrefab);
        m_entityManager.SetComponentData(entity, new Translation()
        {
            Value = pos
        });
    }

    private void InstantiateEntityGrid(int dimX, int dimY, float spacing = 1f)
    {
        for (int i = 0; i < dimX; i++)
        {
            for (int j = 0; j < dimY; j++)
            {
                InstantiateEntity(new float3(i * spacing, j * spacing, 0));
            }
        }
    }

    private void MakeEntity()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityArchetype archetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld)
        );
        var entity = entityManager.CreateEntity(archetype);
        entityManager.AddComponentData(entity, new Translation()
        {
            Value = new float3(2.0f, 0.0f, 0.0f)
        });
        entityManager.AddSharedComponentData(entity, new RenderMesh()
        {
            mesh = m_unitMesh,
            material = m_unitMaterial
        });
    }
}
