using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUInstancer : MonoBehaviour
{
    public int instances;
    public Mesh mesh;
    public Material[] materials;
    public Vector3 offset;
    public Vector3 maxPostion;
    public Vector3 maxScale =Vector3.one;
    public Vector3 minScale;
    public Vector3 minRotation;
    public Vector3 maxRotation;

    private List<List<Matrix4x4>> batches = new List<List<Matrix4x4>>(); //2D list

    private void RenderBatches()
    {
        //DrawMeshInstanced 每次只能渲染長度為1000的batch
        foreach (List<Matrix4x4> batch in batches)
        {
            //每個submesh都需要一個material
            Debug.Log(mesh.subMeshCount);
            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                Graphics.DrawMeshInstanced(mesh, i, materials[i], batch);
            }
        }
    }

    private void Update()
    {
        RenderBatches();
    }

    private void Start()
    {
        int addedMatricies = 0;
        batches.Add(new List<Matrix4x4>());
        for (int i = 0; i < instances; i++)
        {
            if (addedMatricies < 1000)
            {
                //隨機產生 位移 旋轉 scale值
                batches[batches.Count-1].Add(Matrix4x4.TRS(
                   new Vector3(Random.Range(-maxPostion.x, maxPostion.x), Random.Range(-maxPostion.y, maxPostion.y), Random.Range(-maxPostion.z, maxPostion.z)),
                   Quaternion.Euler(new Vector3(Random.Range(minRotation.x, maxRotation.x), Random.Range(minRotation.y, maxRotation.y), Random.Range( minRotation.z, maxRotation.z))),
                    new Vector3(Random.Range(minScale.x, maxScale.x), Random.Range(minScale.y, maxScale.y), Random.Range(minScale.z, maxScale.z))
                    ));
                addedMatricies += 1;

            }
            else {
                batches.Add(new List<Matrix4x4>());
                addedMatricies = 0;
            }

        }
        Debug.Log(batches.Count);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(offset , maxPostion);
    }
}
