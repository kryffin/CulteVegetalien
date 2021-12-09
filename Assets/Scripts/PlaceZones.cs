using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceZones : MonoBehaviour
{

    public string path;
    public float scale = 1000;


    public Material mat;
    public Gradient gradient;

    // Start is called before the first frame update
    void Start()
    {

        GeoJSonParser parser = new GeoJSonParser();
        parser.Parse(path,1);

        Vector3 meanZone = Vector3.zero;

        int count = 0;
        foreach (List<Vector3> zs in parser.zones)
        {
            foreach (Vector3 pointPos in zs)
            {
                meanZone += pointPos / scale;
                count += 1;
            }
        }
        meanZone /= count;

        foreach (List<Vector3> zs in parser.zones)
        {
            /*LineRenderer lineRenderer;
            lineRenderer = new GameObject(this.name + "Line").AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.useWorldSpace = true;
            lineRenderer.material = mat;
            lineRenderer.colorGradient = gradient;
            int lengthOfLineRenderer = zs.Count;
            lineRenderer.positionCount = lengthOfLineRenderer;*/

            Mesh newMesh = new Mesh();
            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();

            Vector3 meanVertex = Vector3.zero;
            for (int i = 0; i < zs.Count; i++)
            {
                //lineRenderer.SetPosition(i, (zs[i] / scale) - meanZone);
                meanVertex += (zs[i] / scale) - meanZone;
                vertices.Add((zs[i] / scale) - meanZone);
                normals.Add(Vector3.up);
            }
            meanVertex /= zs.Count;

            vertices.Add(meanVertex);
            normals.Add(Vector3.up);
            newMesh.SetVertices(vertices);

            newMesh.SetNormals(normals);
            

            int[] triangles = new int[vertices.Count * 3];
            int k = 0;
            int j = 0;
            for (; j < vertices.Count-1; j+=3)
            {
                triangles[j] = k;
                triangles[j + 1] = k + 1;
                triangles[j + 2] = vertices.Count - 1;
                k++;
            }

            triangles[j] = k;
            triangles[j + 1] = 0;
            triangles[j + 2] = vertices.Count - 1;

            newMesh.triangles = triangles;
            GameObject mesh = new GameObject(this.name + "Mesh");
            mesh.AddComponent<MeshFilter>();
            mesh.AddComponent<MeshRenderer>();
            mesh.GetComponent<MeshFilter>().mesh = newMesh;
            mesh.GetComponent<MeshRenderer>().material = mat;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
