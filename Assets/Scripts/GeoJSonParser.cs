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
public struct ZoneFileStruct
{
    public string type;
    public string name;
    public CrsStruct crs;
    public ZoneStruct[] features;
}

[System.Serializable]
public struct CrsStruct
{
    public string type;
    public CrsNameStruct properties;
}

public struct CrsNameStruct
{
    public string name;
}

[System.Serializable]
public struct ZoneStruct
{
    public string type;
    public ZoneProperties properties;
    public ZoneGeometry geometry;
}


[System.Serializable]
public struct ZoneProperties
{
    public float OBJECTID;
    public float gl_2015;
    public string libelles;
    public int strate;
    public float Shape_Leng;
    public float Shape_Area;
}

[System.Serializable]
public struct ZoneGeometry
{
    public string type;
    public double[][][][] coordinates;
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
    private List<string> paths;

    [HideInInspector]
    public List<TreeStruct> trees = new List<TreeStruct>();

    [HideInInspector]
    public List<ZoneStruct> zones = new List<ZoneStruct>();

    /*
     * Constructor of the GeoJsonParser class
     * parameters string path_ : the path to the data file 
     * */
    public GeoJSonParser(string path_)
    {
        path = path_;
    }

    public GeoJSonParser(List<string> paths_)
    {
        paths = paths_;
    }

    public void Parse()
    {
        
        for(int i = 0; i < 2; i++)
        {
            StreamReader reader = new StreamReader(paths[i]);
            string geojson = reader.ReadToEnd();
            switch (i)
            {
                case 0:
                    FileStruct fileStruct = JsonUtility.FromJson<FileStruct>(geojson);
                    foreach (TreeStruct tree in fileStruct.features)
                    {
                        trees.Add(tree);
                    }

                    break;
                case 1:
                    //CA MARCHE PAS CAR NESTED ARRAY
                    //TODO : https://github.com/timokorkalainen/Unity-GeoJSONObject
                    ZoneFileStruct zoneFileStruct = JsonUtility.FromJson<ZoneFileStruct>(geojson);
                    foreach (ZoneStruct zone in zoneFileStruct.features)
                    {
                        


                        zones.Add(zone);
                    }

                    break;
                default:
                    Debug.LogError("Erreur de parsing");
                    break;
            }
            
            
        }
    }


}
