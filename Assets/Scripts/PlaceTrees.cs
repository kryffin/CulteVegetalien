using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTrees : MonoBehaviour
{

    public string path;
    public float spaceBetweenTrees = 1000;
    public float treeScale = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        GeoJSonParser parser = new GeoJSonParser(path);
        parser.Parse();
        foreach(TreeStruct tree in parser.trees)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Vector3 spherePos = new Vector3((float)tree.geometry.coordinates[0], 0, (float)tree.geometry.coordinates[1]) * spaceBetweenTrees;
            sphere.transform.position = spherePos;
            sphere.transform.localScale = Vector3.one * treeScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
