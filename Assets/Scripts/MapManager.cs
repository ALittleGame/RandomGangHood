using System;
using System.Collections.Generic;

public class MapManager
{
    private static readonly MapManager instance = new MapManager();

    private MapManager()
    {
        maps = new Dictionary<String, RMap>();
    }

    public static MapManager Instance()
    {
        return instance;
    }

    private Dictionary<String, RMap> maps;  // 所有地图，使用名称作为索引

    /// <summary>
    /// 添加一个地图，需要指定地图名称
    /// </summary>
    /// <param name="name">地图名称</param>
    /// <param name="map">地图对象</param>
    /// <returns>是否成功</returns>
    public Boolean AddMap(String name, RMap map)
    {
        if (maps.ContainsKey(name)) return false;

        maps.Add(name, map);

        return true;
    }

    /// <summary>
    /// 根据名称找到获取一个地图对象
    /// </summary>
    /// <param name="name">地图名称</param>
    /// <returns>地图对象，不存在时返回null</returns>
    public RMap GetMap(String name)
    {
        if (maps.ContainsKey(name) == false) return null;
        return maps[name];
    }

    public Boolean Save()
    {
        // TODO:chenyufei 调用maps中所有地图的Save
        return true;
    }

    /// <summary>
    /// 从存档文件中加载旧地图，并从配置文件中加载新加入的地图
    /// </summary>
    /// <returns></returns>
    public Boolean Load()
    {
        // TODO: chenyufei 创建RMap对象，根据文件中的数据去初始化，并使用AddMap添加
        return true;
    }
}
