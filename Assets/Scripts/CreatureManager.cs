using System;
using System.Collections.Generic;

public class CreatureManager
{
    private static readonly CreatureManager instance = new CreatureManager();

    private CreatureManager()
    {
        creatures = new Dictionary<Int64, RCreature>();
    }

    public static CreatureManager Instance()
    {
        return instance;
    }

    private Dictionary<Int64, RCreature> creatures;  // 所有生物，使用id作为索引


    /// <summary>
    /// 根据id找到并获取一个生物对象
    /// </summary>
    /// <param name="id">生物id</param>
    /// <returns>生物对象，不存在时返回null</returns>
    public RCreature GetCreature(Int64 id)
    {
        if (creatures.ContainsKey(id) == false) return null;
        return creatures[id];
    }
}