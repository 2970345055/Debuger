
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Description: 支持本地文件写入、自定义颜色日志、FPS实时显示、手机日志运行时查看、日志代码编译剔除、ProtoBuff转Json
public class LogConfig  
{
    /// <summary>
    /// 是否打开日志系统
    /// </summary>
    public bool openLog = true;
    /// <summary>
    /// 日志前缀
    /// </summary>
    public string logHeadFix = "###";
    /// <summary>
    /// 是否显示时间
    /// </summary>
    public bool openTime = true;
    /// <summary>
    /// 显示线程id
    /// </summary>
    public bool showThreadID = true;
    /// <summary>
    /// 日志文件储存开关
    /// </summary>
    public bool logSave = true;
    /// <summary>
    /// 是否显示FPS
    /// </summary>
    public bool showFPS = true;
    /// <summary>
    /// 显示颜色名称
    /// </summary>
    public bool showColorName = true;
    /// <summary>
    /// 文件储存路径
    /// </summary>
    public string logFileSavePath { get { return Application.persistentDataPath + "/"; } }
    /// <summary>
    /// 日志文件名称
    /// </summary>
    public string logFileName { get { return Application.productName + " " + DateTime.Now.ToString("yyyy-MM-dd HH-mm")+".log"; } }
}
