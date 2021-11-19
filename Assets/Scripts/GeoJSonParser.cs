using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct FileStruct
{
    public string type;
    public string name;
    public TreeStruct[] features;
}

[System.Serializable]
public struct TreeStruct
{
    public string type;
    public TreeProperties properties;
    public TreeGeometry geometry;
}

[System.Serializable]
public struct TreeProperties
{
    public string essencefrancais;
    public int circonference_cm;
    public int hauteurtotale_m;
    public int hauteurfut_m;
    public int diametrecouronne_m;
    public int rayoncouronne_m;
    public string dateplantation;
    public string genre;
    public string espece;
    public string variete;
    public string essence;
    public string architecture;
    public string localisation;
    public string naturerevetement;
    public string mobilieurbain;
    public int anneeplantation;
    public string commune;
    public string codeinsee;
    public string nomvoie;
    public int codefuv;
    public int identifiant;
    public int numero;
    public int codegenre;
    public int gid;
}

[System.Serializable]
public struct TreeGeometry
{
    public string type;
    public double[] coordinates;
}

public class GeoJSonParser
{

    /*
    * Path to the data file
    * */
    private string path;

    [HideInInspector]
    public List<TreeStruct> trees = new List<TreeStruct>();

    /*
     * Constructor of the GeoJsonParser class
     * parameters string path_ : the path to the data file 
     * */
    public GeoJSonParser(string path_)
    {
        path = path_;
    }

    public void Parse()
    {
        StreamReader reader = new StreamReader(path);
        string geojson = reader.ReadToEnd();
        FileStruct fileStruct = JsonUtility.FromJson<FileStruct>(geojson);
        foreach(TreeStruct tree in fileStruct.features)
        {
            trees.Add(tree);
        }
    }


}
