using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;


public class LogData
{
    public string log;
    
    public string trace;//堆栈信息 显示报错的行数

    public LogType type;
}

public class UnityLogHelper : MonoBehaviour
{
    
    /// <summary>
    /// 文件写入流
    /// </summary>
    private StreamWriter _streamWriter;

    /// <summary>
    /// 日志消息队列   ConcurrentQueue 用于子线程的安全队列可以帮助我们解决多个线程在调用时的异常
    /// </summary>
    private readonly ConcurrentQueue<LogData> mConcurrentQueue = new ConcurrentQueue<LogData>();

    /// <summary>
    /// 工作信号事件  
    /// </summary>
    private readonly ManualResetEvent _manualResetEvent = new ManualResetEvent(false);

    private bool mThreadRunnin=false;
    private string mNowTime
    {
        get { return DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss"); }
    }

    public void InitLogFileModule(string SavePath,string logfineName)
    {

        string logFilePath = Path.Combine(SavePath,logfineName);
        
        Debug.Log(logFilePath);
        
        //获得一条写入流
        _streamWriter = new StreamWriter(logFilePath);
        
        Application.logMessageReceivedThreaded += OnlogMessageReceivedThreaded;
        
        mThreadRunnin = true;
        
        Thread fileThread = new Thread(FileLogThread);
        
        fileThread.Start();
    }

    public void FileLogThread()
    {
        while (mThreadRunnin)
        {
            _manualResetEvent.WaitOne(); //让线程进入等待 进行阻塞
            if (_streamWriter==null)
            {
                break;
            }

            LogData data;
            
            while (mConcurrentQueue.Count>0 && mConcurrentQueue.TryDequeue(out data) )
            {   
                if (data.type==LogType.Log)
                {
                 _streamWriter.Write("Log >>>");
                 _streamWriter.WriteLine(data.log);
                 
                 _streamWriter.WriteLine(data.trace);
                    
                }else if (data.type==LogType.Error)
                {
                    _streamWriter.Write("Error >>>");
                    _streamWriter.WriteLine(data.log);
                 
                    _streamWriter.WriteLine(data.trace);

                }
                else if (data.type==LogType.Warning)
                {
                    _streamWriter.Write("Warning >>>");
                    _streamWriter.WriteLine(data.log);
                    _streamWriter.Write("\n");
                    _streamWriter.WriteLine(data.trace);

                }
                _streamWriter.Write("\r\n");
            }
            //保存内容 写入
            _streamWriter.Flush();
            //重置信号 ，结束工作
            _manualResetEvent.Reset();
            Thread.Sleep(1);
        }
    }

    public void OnApplicationQuit()
    {
        Application.logMessageReceivedThreaded -= OnlogMessageReceivedThreaded;
        
        mThreadRunnin = false;
        //让线程工作 讲关闭的那一刻的日志写入
        _manualResetEvent.Set();
        //关闭写入流
        _streamWriter.Close();
        _streamWriter = null;
    }

    
    private void OnlogMessageReceivedThreaded(string condition, string stackTrace, LogType type)
    {  
        ///添加队列
        mConcurrentQueue.Enqueue(new LogData
        {
            log = mNowTime +" "+condition,
            trace = stackTrace,
            type  =type,
        });

        _manualResetEvent.Set();

        /*_manualResetEvent.WaitOne();//让线程进入等待 进行阻塞
        _manualResetEvent.Set(); //设置一个信号 表示线程是需要工作
        _manualResetEvent.Reset(); // 重置 信号 ，表示没有人指定需要工作*/
    }
}
