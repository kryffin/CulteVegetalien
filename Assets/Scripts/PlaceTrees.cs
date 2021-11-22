using System.Collections.Generic;
using UnityEngine;

public class PlaceTrees : MonoBehaviour
{
    private List<GameObject> _trees;

    public GameObject treePrefab;
    public string path;
    public float spaceBetweenTrees = 1000;
    public float treeScale = 0.02f;

    private void Start()
    {
        _trees = new List<GameObject>();
        Vector3 mean = Vector3.zero;

        GeoJSonParser parser = new GeoJSonParser(path);
        parser.Parse();

        foreach (TreeStruct ts in parser.trees)
        {
            GameObject tree = Instantiate(treePrefab, transform);
            tree.name = "[" + ts.properties.identifiant + "] " + ts.properties.essencefrancais;
            Vector3 treePos = new Vector3((float)ts.geometry.coordinates[0], 0, (float)ts.geometry.coordinates[1]) * spaceBetweenTrees;
            tree.transform.position = treePos;

            tree.GetComponent<CustomTree>().ReadValues(ts.properties, treeScale);

            _trees.Add(tree);

            mean += treePos;
        }

        mean /= parser.trees.Count;

        foreach (GameObject tree in _trees)
            tree.transform.position = tree.transform.position - mean;
    }
}
