using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroTable : TableBase{

    public Dictionary<int, HeroData> infoDic = new Dictionary<int, HeroData>();
    public override void Init()
    {
        base.Init();
        LoadExcel("type_hero.txt");
    }

    public override void ReadExcel()
    {
        base.ReadExcel();
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
        foreach (var item in lineDic.Keys)
        {
            switch (item.ToLower())
            {
                case "id": data.eid = int.Parse(lineDic[item]);
                    break;
                case "heroid": data.type = int.Parse(lineDic[item]);
                    break;
                case "name": data.name = lineDic[item];
                    break;
                case "model": data.model = lineDic[item];
                    break;
                case "icon": data.icon = lineDic[item];
                    break;
                case "skill0": data.skill0 = int.Parse(lineDic[item]);
                    break;
            }
        }
        return data;
    }

    public HeroData GetDataByEID(int eid)
    {
        if (infoDic.ContainsKey(eid))
        {
            return infoDic[eid];
        }
        return null;
    }
}

public class HeroData
{
    public int eid;
    public int type;
    public string name;
    public string model;
    public string icon;
    public int skill0;
}
