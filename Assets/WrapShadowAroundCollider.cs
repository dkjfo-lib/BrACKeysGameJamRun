using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WrapShadowAroundCollider : MonoBehaviour
{
    //public float offset = .4f;

    private static BindingFlags accessFlagsPrivate = BindingFlags.NonPublic | BindingFlags.Instance;
    private static FieldInfo meshField = typeof(ShadowCaster2D).GetField("m_Mesh", accessFlagsPrivate);
    private static FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", accessFlagsPrivate);
    private static MethodInfo onEnableMethod = typeof(ShadowCaster2D).GetMethod("OnEnable", accessFlagsPrivate);

    private CompositeCollider2D Composite;

    void Start()
    {
        Composite = GetComponent<CompositeCollider2D>();


        var mesh = Composite.CreateMesh(true, true);

        var islands = MeshSplitter.SplitToTriangles(mesh);

        foreach (var triangle in islands)
        {
            var points = triangle.vertices;
            Vector3[] positions = new Vector3[points.Length];
            //for (int i = 0; i < points.Length; i++)
            //{
            //    for (int j = 0; j < points.Length; j++)
            //    {
            //        if (points[i] == points[j]) continue;
            //        points[j] += (points[i] - points[j]).normalized * offset;
            //    }
            //}
            for (int i = 0; i < points.Length; i++)
            {
                positions[i] = new Vector3(points[i].x, points[i].y, 0);
            }

            GameObject newGameObject = new GameObject("Shadow");
            var shadow = newGameObject.AddComponent<ShadowCaster2D>();
            newGameObject.transform.parent = transform;

            shadow.selfShadows = false;
            shapePathField.SetValue(shadow, positions);
            meshField.SetValue(shadow, null);
            onEnableMethod.Invoke(shadow, new object[0]);
        }
    }
}

public class MeshSplitter
{
    static public List<Mesh> SplitToMeshes(Mesh mesh)
    {
        List<int> foundPoints = new List<int>();
        List<int> restVerts = new List<int>();

        List<Mesh> splitedMeshes = new List<Mesh>();
        Vector3[] verts = mesh.vertices;

        //list all indices
        restVerts.AddRange(mesh.triangles);

        while (restVerts.Count > 0)
        {
            foundPoints.Clear();
            //Get first triangle
            for (int i = 0; i < 3; i++)
            {
                foundPoints.Add(restVerts[i]);
            }
            // run through all triangles
            for (int i = 1; i < restVerts.Count / 3; i++)
            {
                if (foundPoints.Contains(restVerts[(i * 3) + 0]) || foundPoints.Contains(restVerts[(i * 3) + 1]) || foundPoints.Contains(restVerts[(i * 3) + 2]))
                {
                    for (int q = 0; q < 3; q++)
                    {
                        foundPoints.Add(restVerts[(i * 3) + q]);
                    }
                }
            }
            restVerts.RemoveAll(s => foundPoints.Contains(s));

            Mesh newMesh = new Mesh();
            newMesh.vertices = foundPoints.Distinct().Select(s => verts[s]).ToArray();
            newMesh.triangles = foundPoints.ToArray();

            splitedMeshes.Add(newMesh);
        }
        return splitedMeshes;
    }

    static public List<Mesh> SplitToTriangles(Mesh mesh)
    {
        List<Mesh> splitedMeshes = new List<Mesh>();
        List<int> restVerts = new List<int>(mesh.triangles);
        Vector3[] verts = mesh.vertices;

        // run through all triangles
        for (int i = 0; i < restVerts.Count / 3; i++)
        {
            var triangle = new int[]
            {
                    restVerts[(i * 3) + 0],
                    restVerts[(i * 3) + 1],
                    restVerts[(i * 3) + 2]
            };
            var foundPoints = triangle.Select(s => verts[s]).ToArray();

            Mesh newMesh = new Mesh();
            newMesh.vertices = foundPoints;
            newMesh.triangles = triangle;

            splitedMeshes.Add(newMesh);
        }

        return splitedMeshes;
    }
}