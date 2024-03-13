using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logsystem : MonoBehaviour
{
    private void Awake()
    {
#if OPEN_LOG
        Debuger.Init(new LogConfig
        {
            openTime = true,
            openLog = true,
            showThreadID = true,
            showColorName = false,
            showFPS = true,
            logSave = true
        });
        
        Debug.Log("log");
        Debuger.LogRed("Log");
        Debuger.ColorLog("log",LogColor.Cyan);
        
        Debuger.Log("log");
        Debuger.LogWarning("log");
        Debuger.LogError("log");
    }
    #else
         //关闭Unity的日志打印
      Debug.unityLogger.logEnabled = false;
#endif
}
