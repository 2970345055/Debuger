using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// 方便转换proto结构体位Json  
/// </summary>
public class ProtoBuffConvert 
{    
    
    public static string ToJson<T>(T proto)
    { 
        string log=JsonConvert.SerializeObject(proto);
        Debuger.Log(log);
        return log;
    }

}
