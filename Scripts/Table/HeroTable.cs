using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroTable : TableBase{

    public Dictionary<int, HeroData> infoDic = new Dictionary<int, HeroData>();

    public override void ReadExcel()
    {
        using(var item = excelData.GetEnumerator())
        {
            while (item.MoveNext())
            {
                infoDic[item.Current.Key] = ReadLine(item.Current.Value);
            }  
        }
    }

    public HeroData ReadLine(Dictionary<string,string> lineDic)
    {
        HeroData data = new HeroData();
        data.eid = int.Parse(lineDic["eid"]);
        return data;
    }
}

public class HeroData
{
    public int eid;
    public int type;
    public string name;
    public string model;
    public string icon;
}
