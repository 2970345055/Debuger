using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LogEditor 
{   
    [MenuItem("MyLog/打开日志")]
    public static void LoadReport()
    {   
        ///添加宏
        ScriptingDefineSymbols.AddScriptingDefineSymbol("OPEN_LOG");
        
        GameObject reportObj = GameObject.Find("Reporter");
        
        if (reportObj==null)
        {   
            reportObj= GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scripts/UnityDebuger/Unity-Logs-Viewer/Reporter.prefab"));
            reportObj.name = "Reporter";
            AssetDatabase.SaveAssets();
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            AssetDatabase.Refresh();
            Debug.Log("Open Log Finish!");
        }
        
    }
    [MenuItem("MyLog/关闭日志")]
    public static void CloseReport()
    {
        ScriptingDefineSymbols.RemoveScriptingDefineSymbol("OPEN_LOG");
        
        GameObject reportObj = GameObject.Find("Reporter");
        if (reportObj!=null)
        {
            GameObject.DestroyImmediate(reportObj);
            AssetDatabase.SaveAssets();
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            AssetDatabase.Refresh();
            Debug.Log("Cloase Log Finish!");
        }
    }
}
