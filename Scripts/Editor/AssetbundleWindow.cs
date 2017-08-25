//=========================
//作者:邓成
//日期:2017/7/29
//用途:打包Assetbundle
//=========================

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class AssetbundleWindow : EditorWindow {
    public string titleContent = "";
    public string targetPath =  "";

    protected BuildAssetBundleOptions buildOptions = BuildAssetBundleOptions.DeterministicAssetBundle;
    protected BuildTarget buildTarget = BuildTarget.Android;
    protected string togetherAssetbundleName = "";
    protected BuildOptions buildSceneOption = BuildOptions.BuildAdditionalStreamedScenes;
       //利用构造函数来设置窗口名称
    AssetbundleWindow()
    {
        this.titleContent = "AssetbundleWindow";
        this.minSize = new Vector2(600,300);
    }

    //添加菜单栏用于打开窗口
    [MenuItem("Assetbundle/打开打包窗口")]
    static void OpenAssetbundleWindow()
    {
        EditorWindow.GetWindow(typeof(AssetbundleWindow));
    }
    void OnGUI()
    {
        //GUILayout.BeginVertical();
        //绘制标题
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label(titleContent);
        GUILayout.Space(10);

        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;

        buildOptions = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup("BuildAssetBundleOptions",buildOptions);
        buildTarget = (BuildTarget)EditorGUILayout.EnumPopup("BuildTarget", buildTarget);
        if (buildTarget == BuildTarget.iOS)
        {
            this.targetPath = Application.persistentDataPath + "/assetbundle_ios/";
        }
        else
        {
            this.targetPath = Application.persistentDataPath + "/assetbundle_android/";
        }
        EditorGUILayout.LabelField("保存位置:");
        GUILayout.BeginHorizontal();
        targetPath = EditorGUILayout.TextField(targetPath);
        if (GUILayout.Button("...", GUILayout.Width(30)))
        {
            string newPath = EditorUtility.SaveFolderPanel("AssetBundle Exports Path", targetPath, "");
            if (newPath.Length != 0)
            {
                targetPath = newPath;
            }
        }
        GUILayout.EndHorizontal();
        togetherAssetbundleName = EditorGUILayout.TextField("如果打包到一起,请命名:", togetherAssetbundleName);
        if (GUILayout.Button("单个打包or分开打包"))
        {
            CreateAssetBunldesSingle();
        }

        if (GUILayout.Button("将选中的打包到一起"))
        {
            CreateAssetBunldesTogether();
        }

        if (GUILayout.Button("打包场景"))
        {
            CreateSceneAssetbundle();
        }
        //GUILayout.EndVertical();
    }

    void CreateAssetBunldesSingle()
    {
        Caching.CleanCache();
        //获取在Project视图中选择的所有游戏对象  
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        if (!Directory.Exists(targetPath))
            Directory.CreateDirectory(targetPath);
        //遍历所有的游戏对象  
        foreach (Object obj in SelectedAsset)
        {
            //本地测试：建议最后将Assetbundle放在StreamingAssets文件夹下，如果没有就创建一个，因为移动平台下只能读取这个路径  
            //StreamingAssets是只读路径，不能写入  
            //服务器下载：就不需要放在这里，服务器上客户端用www类进行下载。  
            string path = targetPath + obj.name + ".assetbundle";
            if (BuildPipeline.BuildAssetBundle(obj, null, path, buildOptions, buildTarget))
            {
                Debug.Log(obj.name + "资源打包成功");
            }
            else
            {
                Debug.Log(obj.name + "资源打包失败");
            }
        }
        //刷新编辑器  
        AssetDatabase.Refresh();

    }

    void CreateAssetBunldesTogether()
    {
        Caching.CleanCache();
        //获取在Project视图中选择的所有游戏对象  
        if (!Directory.Exists(targetPath))
            Directory.CreateDirectory(targetPath);
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        if (string.IsNullOrEmpty(togetherAssetbundleName))
        {
            Debug.LogError("togetherAssetbundleName is null");
            return;
        }
        string path = targetPath + togetherAssetbundleName + ".assetbundle";
        if (BuildPipeline.BuildAssetBundle(null, SelectedAsset, path, buildOptions, buildTarget))
        {
            Debug.Log(togetherAssetbundleName + "资源打包成功");
        }
        else
        {
            Debug.Log(togetherAssetbundleName + "资源打包失败");
        }
        //刷新编辑器  
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 打包场景
    /// </summary>
    void CreateSceneAssetbundle()
    {
        Caching.CleanCache();
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        if (!Directory.Exists(targetPath))
            Directory.CreateDirectory(targetPath);
        //遍历所有的游戏对象  
        foreach (Object obj in SelectedAsset)
        {
            //本地测试：建议最后将Assetbundle放在StreamingAssets文件夹下，如果没有就创建一个，因为移动平台下只能读取这个路径  
            //StreamingAssets是只读路径，不能写入  
            //服务器下载：就不需要放在这里，服务器上客户端用www类进行下载。  
            string scenePath = AssetDatabase.GetAssetPath(obj);
            string path = targetPath + obj.name + ".object";
            string[] arg = { scenePath };
            string result = BuildPipeline.BuildPlayer(arg, path, buildTarget, buildSceneOption);
            Debug.Log(result);
        }
        //刷新编辑器  
        AssetDatabase.Refresh();
    }
}
