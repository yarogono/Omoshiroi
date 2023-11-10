using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public NavMeshTriangulation navMeshData;
    public GameObject navMeshPrefab;

    private void Update()
    {
        ExportNavMesh();
    }

    private void ExportNavMesh()
    {
        navMeshData = NavMesh.CalculateTriangulation();

        List<string> lines = new List<string>();

        // 저장할 포인트의 개수와 삼각형의 개수를 파일에 저장
        lines.Add(navMeshData.vertices.Length.ToString());
        lines.Add(navMeshData.indices.Length.ToString());

        // 포인트 좌표를 파일에 저장
        foreach (Vector3 vertex in navMeshData.vertices)
        {
            lines.Add(vertex.x + " " + vertex.y + " " + vertex.z);
        }

        // 삼각형 인덱스를 파일에 저장
        foreach (int index in navMeshData.indices)
        {
            lines.Add(index.ToString());
        }

        // 텍스트 파일로 저장
        File.WriteAllLines("NavMeshData.txt", lines.ToArray());
    }

    private void ImportNavMesh()
    {
        string[] lines = File.ReadAllLines("NavMeshData.txt");

        int numVertices = int.Parse(lines[0]);
        int numIndices = int.Parse(lines[1]);

        Vector3[] vertices = new Vector3[numVertices];
        int[] indices = new int[numIndices];

        int lineIndex = 2;

        // 포인트 좌표 불러오기
        for (int i = 0; i < numVertices; i++)
        {
            string[] vertexData = lines[lineIndex].Split(' ');
            vertices[i] = new Vector3(
                float.Parse(vertexData[0]),
                float.Parse(vertexData[1]),
                float.Parse(vertexData[2])
            );
            lineIndex++;
        }

        // 삼각형 인덱스 불러오기
        for (int i = 0; i < numIndices; i++)
        {
            indices[i] = int.Parse(lines[lineIndex]);
            lineIndex++;
        }

        // Mesh 생성
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = indices;

        // MeshRenderer 및 MeshFilter를 가진 GameObject에 Mesh 할당
        GameObject navMeshObject = Instantiate(navMeshPrefab);
        navMeshObject.GetComponent<MeshFilter>().mesh = mesh;
        navMeshObject.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
    }
}
