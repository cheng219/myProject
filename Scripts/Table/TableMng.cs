using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TableMng{
    protected static TableMng tableMng = null;
    public static TableMng intance
    {
        get
        {
            if (tableMng == null)
            {
                tableMng = new TableMng();
            }
            return tableMng;
        }
    }
    /// <summary>
    /// 保存客户端数据的列表
    /// </summary>
    protected Dictionary<System.Type, TableBase> TableDic = new Dictionary<System.Type, TableBase>();
    /// <summary>
    /// 剩余加载数量
    /// </summary>
    protected int LoadCount = 0;
    public bool LoadFinish
    {
        get
        {
            return LoadCount == TableDic.Count;
        }
    }

    public void InitAll()
    { 
        InitTable<HeroTable>();
    }

    protected void InitTable<T>() where T : TableBase,new ()
    {
        LoadCount++;
        T t = new T();
        t.Init();
        TableDic[typeof(T)] = t;
    }

    /// <summary>
    /// 获取一个table数据类
    /// </summary>
    public T GetTable<T>() where T : TableBase
    {
        System.Type type = typeof(T);
        if (TableDic.ContainsKey(type))
        {
            return TableDic[type] as T;
        }
        return null;
    }
    /// <summary>
    /// 获取一个table数据类，获取之后强转再使用
    /// </summary>
    public TableBase GetTable(System.Type type)
    {
        if (TableDic.ContainsKey(type))
        {
            return TableDic[type];
        }
        return null;
    }
}

public class TableBase
{
    protected Dictionary<int, Dictionary<string, string>> excelData = new Dictionary<int, Dictionary<string, string>>();
    private string dataText = string.Empty;
    public virtual void Init()
    {

    }

    protected void LoadExcel(string excelName)
    {
        //load
        string path = PathTool.GetFilePath("table/" + excelName, AssetPathType.StreamingAssetsPath);
        Debug.Log("path:"+path);
        dataText = File.ReadAllText(path);
        Debug.Log(dataText);
        ReadExcel();
    }
    public virtual void ReadExcel()
    {
        dataText = dataText.Replace("\r", "");
        string[] hList = dataText.Split('\n');
        string tile = hList[1];
        string[] titles = tile.Split('\t');
        for (int i = 2; i < hList.Length; i++)
        {
            string[] line = hList[i].Split('\t');
            Dictionary<string, string> lineKeyValue = new Dictionary<string, string>();
            for (int j = 0; j < line.Length; j++)
            {
                if (string.IsNullOrEmpty(titles[j])) break;//排除空列
                lineKeyValue[titles[j]] = line[j];
            }
            if (line.Length > 1)
            {
                if (!excelData.ContainsKey(int.Parse(line[0])))
                    excelData.Add(int.Parse(line[0]), lineKeyValue);
            }
        }

    }
}
