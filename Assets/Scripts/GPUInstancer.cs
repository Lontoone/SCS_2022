using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
[ExecuteInEditMode]
public class GPUInstancer : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 maxPostion;
    public Vector3 maxScale = Vector3.one;
    public Vector3 minScale;
    public Vector3 minRotation;
    public Vector3 maxRotation;

    public int instanceCount = 100000;
    public Mesh instanceMesh;
    public Material instanceMaterial;
    public int subMeshIndex = 0;

    GPUPoint[] points;
    int[] drawArgs;
    private ComputeBuffer cbDrawArgs;
    public ComputeBuffer pointsBuffer;

    public struct GPUPoint
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        //public float depth;
    }

    private void Start()
    {
        SetUp();

    }
    private void OnValidate()
    {
        SetUp();
    }

    private void SetUp()
    {

        points = new GPUPoint[instanceCount];
        drawArgs = new int[]
            {
                (int)instanceMesh.GetIndexCount(0),
                instanceCount,
                (int)instanceMesh.GetIndexStart(0),
                (int)instanceMesh.GetBaseVertex(0),
                0
        };
        if (pointsBuffer == null)
        {
            int strip = Marshal.SizeOf(typeof(GPUPoint));
            pointsBuffer = new ComputeBuffer(instanceCount, strip);
        }
        //設定Mesh資料

        if (cbDrawArgs == null)
        {

            cbDrawArgs = new ComputeBuffer(1, drawArgs.Length * 4, ComputeBufferType.IndirectArguments); //each int is 4 bytes
        }

        for (int i = 0; i < instanceCount; i++)
        {
            //每秒新增points 
            GPUPoint _p = new GPUPoint();

            _p.position = transform.position + offset + new Vector3(Random.Range(-maxPostion.x, maxPostion.x), Random.Range(-maxPostion.y, maxPostion.y), Random.Range(-maxPostion.z, maxPostion.z)) * 0.5f;
            _p.rotation = Quaternion.Euler(new Vector3(Random.Range(minRotation.x, maxRotation.x), Random.Range(minRotation.y, maxRotation.y), Random.Range(minRotation.z, maxRotation.z))).eulerAngles;
            _p.scale = new Vector3(Random.Range(minScale.x, maxScale.x), Random.Range(minScale.y, maxScale.y), Random.Range(minScale.z, maxScale.z));

            points[i] = _p;
        }
        Debug.Log(points.Length);
        pointsBuffer.SetData(points);
        cbDrawArgs.SetData(drawArgs);

        Debug.Log(points[0].scale);
    }

    private void Update()
    {

        instanceMaterial.SetBuffer("_PointsBuffer", pointsBuffer);

        Graphics.DrawMeshInstancedIndirect(instanceMesh, 0, instanceMaterial, new Bounds(transform.position + offset, maxPostion), cbDrawArgs);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(transform.position + offset, maxPostion);
    }
}
