using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using UnityEngine;

public class Debuger
{
   public static LogConfig Config;
   
   
   [Conditional("OPEN_LOG")]
   public static void Init(LogConfig logConfig=null)
   {
      if (logConfig==null)
      {
         Config = new LogConfig();
      }
      else
      {
         Config = logConfig;
      }
      
      if (Config.logSave)
      {
         GameObject logObj = new GameObject("LogHelper");
         GameObject.DontDestroyOnLoad(logObj);
         UnityLogHelper unityLogHelper =  logObj.AddComponent<UnityLogHelper>();
         unityLogHelper.InitLogFileModule(Config.logFileSavePath,Config.logFileName);
      }
    
      if (Config.showFPS)
      {
         GameObject FPSObj = new GameObject("FPS");
         GameObject.DontDestroyOnLoad(FPSObj);
       
      
      }
   }

   #region 生成普通日志
   [Conditional("OPEN_LOG")]
   public static void Log(object obj)
   {
      if (!Config.openLog)
      {
         return;
      }

      string str = GenerateLog(obj.ToString());
      
      UnityEngine.Debug.Log(str);
   }
   [Conditional("OPEN_LOG")]
   public static void Log(object obj,params object[] arges)
   {
      if (!Config.openLog)
      {
         return;
      }

      string conent = String.Empty;
      if (arges!=null)
      {
         foreach (var item in arges)
         {
            conent += item;
         }
      }
      string str = GenerateLog(obj+conent.ToString());
      
      UnityEngine.Debug.Log(str);
   }
   
   
   [Conditional("OPEN_LOG")]
   public static void LogWarning(object obj)
   {
      if (!Config.openLog)
      {
         return;
      }

      string str = GenerateLog(obj.ToString());
      
      UnityEngine.Debug.LogWarning(str);
   }
   [Conditional("OPEN_LOG")]
   public static void LogWarning(object obj,params object[] arges)
   {
      if (!Config.openLog)
      {
         return;
      }

      string conent = String.Empty;
      if (arges!=null)
      {
         foreach (var item in arges)
         {
            conent += item;
         }
      }
      string str = GenerateLog(obj+conent.ToString());
      
      UnityEngine.Debug.LogWarning(str);
   }
   
   
   [Conditional("OPEN_LOG")]
   public static void LogError(object obj)
   {
      if (!Config.openLog)
      {
         return;
      }

      string str = GenerateLog(obj.ToString());
      
      UnityEngine.Debug.LogError(str);
   }
   [Conditional("OPEN_LOG")]
   public static void LogError(object obj,params object[] arges)
   {
      if (!Config.openLog)
      {
         return;
      }

      string conent = String.Empty;
      if (arges!=null)
      {
         foreach (var item in arges)
         {
            conent += item;
         }
      }
      string str = GenerateLog(obj+conent.ToString());
      
      UnityEngine.Debug.LogError(str);
   }

   
   #endregion


   #region 颜色日志打印
   [Conditional("OPEN_LOG")]
   public static void ColorLog(object obj,LogColor color)
   {
      if (!Config.openLog)
      {
         return;
      }

      string log = GenerateLog(obj.ToString(),color);

      log = GetUnityColor(log, color);
      
      UnityEngine.Debug.Log(log);
   }
   
   [Conditional("OPEN_LOG")]
   public static void LogRed(object msg)
   {
       ColorLog(msg,LogColor.Red);
   }

   #endregion
   public static String GenerateLog(string log,LogColor color=LogColor.None)
   {
      StringBuilder stringBuilder = new StringBuilder(Config.logFileName, 100);
      
      if (Config.openTime)
      {
         stringBuilder.AppendFormat("-- {0}", DateTime.Now.ToString("hh:mm:ss-fff"));
      }
      
      //是否显示线程ID
      if (Config.showThreadID)
      {
         stringBuilder.AppendFormat("   ThreadID: "+"{0}",Thread.CurrentThread.ManagedThreadId);
      }

      if (Config.showColorName)
      {
         stringBuilder.AppendFormat("{0}",color.ToString());
      }

      stringBuilder.AppendFormat(" : {0}", log);
      
      return stringBuilder.ToString();
   }
  
   public static string GetUnityColor(string msg,LogColor color)
   {
      if (color==LogColor.None)
      {
         return msg;
      }
      switch (color)
      {
         case LogColor.Blue:
            msg = $"<color=#0000FF>{msg}</color>";
            //  msg = $"<color=red>{msg}</color>";
            break;
         case LogColor.Cyan:
            msg = $"<color=#00FFFF>{msg}</color>";
            break;
         case LogColor.Darkblue:
            msg = $"<color=#8FBC8F>{msg}</color>";
            break;
         case LogColor.Green:
            msg = $"<color=#00FF00>{msg}</color>";
            break;
         case LogColor.Orange:
            msg = $"<color=#FFA500>{msg}</color>";
            break;
         case LogColor.Red:
            msg = $"<color=#FF0000>{msg}</color>";
            break;
         case LogColor.Yellow:
            msg = $"<color=#FFFF00>{msg}</color>";
            break;
         case LogColor.Magenta:
            msg = $"<color=#FF00FF>{msg}</color>";
            break;
      }
      return msg;
   }
}
