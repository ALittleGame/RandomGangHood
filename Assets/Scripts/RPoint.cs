using System;

// 地点类型掩码
public enum RPointType
{
    RPT_NORMAL      = 0x00000000,   // 普通
    RPT_STORE       = 0x00000001,   // 商店
    RPT_INSTANCE    = 0x00000002,   // 副本
    RPT_EVENT       = 0x00000004,   // 事件
    RPT_OTHER       = 0x00000008,   // 预留
};

// 表示地图中的一个点
public class RPoint
{
    public  Int64       locationX;  // x坐标
    public  Int64       locationY;  // y坐标
    private String      name;       // 地图点名称
    private Int64       id;         // 地图点id
    private RPointType  type;       // 地图点类型

    #region  // Get与Set方法

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public long Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public RPointType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }
    #endregion

    // 构造函数
    public RPoint(Int64 _id, Int64 _x, Int64 _y)
    {
        id = _id;
        locationX = _x;
        locationY = _y;
    }


    #region  // 其他函数
    public Boolean AddType(RPointType _type)
    {
        if (_type > RPointType.RPT_OTHER) return false;
        type |= _type;
        return true;
    }

    public Boolean RemoveType(RPointType _type)
    {
        if (_type > RPointType.RPT_OTHER) return false;
        if ((type & _type) == RPointType.RPT_NORMAL) return false;

        type ^= _type;

        return true;
    }

    #endregion 
}

// 构造地图点的工厂（这种写法是c#特有的线程安全的单例）
public class RPointFactory
{
    private static readonly RPointFactory instance = new RPointFactory();

    private RPointFactory() { }

    public static RPointFactory Instance()
    {
        return instance;
    }

    private Int64 idCount = 0;
    private Boolean inited = false;

    #region  // 逻辑相关函数
    public void Init()
    {
        if (inited == false)
        {
            // TODO:chenyufei 从文件中读取idCount
        }

        inited = true;
    }

    public Boolean Save()
    {
        // TODO:chenyufei 将当前的idCount存入文件
        return true;
    }

    // 创建一个地图点
    public RPoint CreatePoint(Int64 _x, Int64 _y)
    {
        return new RPoint(++idCount, _x, _y);
    }
    #endregion
}