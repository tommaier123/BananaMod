using UnityEngine;
using UMod;
using UnityEngine.SceneManagement;
using System.IO;


public class BananaScript : ModScript
{
    public GameObject object_left = null;
    public GameObject indicator_left = null;
    public MeshRenderer[] mrs_left = null;
    public MeshFilter[] mfs_left = null;

    public GameObject object_right = null;
    public GameObject indicator_right = null;
    public MeshRenderer[] mrs_right = null;
    public MeshFilter[] mfs_right = null;

    public override void OnModLoaded()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        log("BananaMod loaded");
    }

    public override void OnModUpdate()
    {
        if (indicator_left == null && object_left != null)
        {
            indicator_left = GameObject.Find("Left Indicator");
            mfs_left = indicator_left.GetComponentsInChildren<MeshFilter>();
            mfs_left[0].sharedMesh = object_left.GetComponent<MeshFilter>().sharedMesh;

            mrs_left = indicator_left.GetComponentsInChildren<MeshRenderer>();
            mrs_left[0].material = object_left.GetComponent<MeshRenderer>().material;

            object_left.GetComponent<MeshFilter>().sharedMesh = null;
        }

        if (indicator_right == null && object_right != null)
        {
            indicator_right = GameObject.Find("Right Indicator");
            mfs_right = indicator_right.GetComponentsInChildren<MeshFilter>();
            mfs_right[0].sharedMesh = object_right.GetComponent<MeshFilter>().sharedMesh;

            mrs_right = indicator_right.GetComponentsInChildren<MeshRenderer>();
            mrs_right[0].material = object_right.GetComponent<MeshRenderer>().material;

            object_right.GetComponent<MeshFilter>().sharedMesh = null;
        }
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name.Contains("Stage"))
        {
            object_left = ModAssets.Instantiate<GameObject>("BananaL_pre");
            object_right = ModAssets.Instantiate<GameObject>("BananaR_pre");
        }
    }

    public override void OnModUnload()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        log("BananaMod loaded");
    }

    public void log(string str)
    {
        //get file path
        var dataPath = Application.dataPath;
        var filePath = dataPath.Substring(0, dataPath.LastIndexOf('/')) + "/Novalog.txt";

        //write
        using (var streamWriter = new StreamWriter(filePath, true))
        {
            streamWriter.WriteLine(str);
        }
    }
}
