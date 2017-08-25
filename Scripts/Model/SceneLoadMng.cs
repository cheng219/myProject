using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoadMng : MonoBehaviour {

    public static SceneLoadMng instance;
    protected string folderName = "/scene/";
    void Awake()
    {
        instance = this;
    }

    public void LoadScene(string sceneName,System.Action<string> finish)
    {
        string path = PathTool.GetWWWPath(folderName + sceneName + ".object", AssetPathType.StreamingAssetsPath);
        Debug.Log("path:"+path);
        StartCoroutine(StartLoadScene(sceneName,path,finish));
    }

    IEnumerator StartLoadScene(string sceneName,string path,System.Action<string> finish)
    {
        WWW www = new WWW(path);
        yield return www;
        while (!www.isDone)
        {
            if (www.error != null)
            {
                finish(www.error);
                yield break;
            }
            yield return null;
        }
        if (www.isDone)
        {
            if (www.assetBundle != null)
            {
                AsyncOperation asyn = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
                while (asyn == null || !asyn.isDone)
                {
                    yield return new WaitForFixedUpdate();
                }
                GameObject scene = GameObject.Find(sceneName);
                MeshRenderer[] mesh = scene.GetComponentsInChildren<MeshRenderer>(true);
                for (int i = 0,length=mesh.Length; i < length; i++)
                {
                    Material[] materials = mesh[i].materials;
                    for (int j = 0,lengthJ = materials.Length; j < lengthJ; j++)
                    {
                        Shader shader = Shader.Find(materials[j].shader.name);
                        if (shader == null) Debug.LogError("cant find  Shader:" + materials[j].shader.name);
                        materials[j].shader = shader;
                    }
                }
                finish(string.Empty);
            }
        }
        www.Dispose();
        www = null;
    }
}
