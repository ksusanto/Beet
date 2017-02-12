using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ServiceProcess;
using System.Diagnostics;
using NLog;

namespace enBreadBoard
{
  namespace Utility
  {
    public static class __CONST
    {
      private static string _DLDir = @"c:\_Envision\App";
      private static string _FLCNF = @"config.xml";
      private static string _DPCnf = _DLDir + @"\" + _FLCNF;
      public static string DLDir
      {
        get { return (_DLDir); }                                   //  Report Directory Data
        set { _DLDir = value; _DPCnf = value + @"\" + _FLCNF; }
      }
    }


  }

  class Program
  {
    static void Main(string[] args)
    {
#if DEBUG
      args = new string[] { @"--service", @"--configdir", @"C:\_Envision\Reporting" };
#endif
      Logger logIt = LogManager.GetCurrentClassLogger();
      Program pr = new Program();

      if (Array.FindAll(args, s => s.Equals("--service")).Count() > 0)
      {
        ServiceBase[] ServicesToRun;
        ServicesToRun = new ServiceBase[]
              {
              new enServices(args)
              };
        ServiceBase.Run(ServicesToRun);
      }
      Thread.Sleep(500);
    }

    private void nlogTrace (string sText, Logger logIt)
    {
      logIt.Trace("Start Tracing");
    }
    protected EventWaitHandle _sdEvents;
    protected Thread[] iThread = new Thread[10];
    private int _MaxThread = 10;

    public void exeThread (Logger logIt)
    {
      Process currentProcess = Process.GetCurrentProcess();
      currentProcess.PriorityClass = ProcessPriorityClass.High;
      _sdEvents = new EventWaitHandle(false, EventResetMode.ManualReset);
      for (int i = 0; i < _MaxThread; i++)
      {
        iThread[i] = new Thread(() => taskThread(i, logIt));
        iThread[i].Priority = ThreadPriority.Highest;
        iThread[i].Name = "__ID_" + i.ToString();
        iThread[i].Start();
      }
      Thread.Sleep(50);
    }

    private void taskThread (int iTH, Logger logIt)
    {
      Random r = new Random();
      while (true)
      {
        logIt.Trace("{0}, Process Thread", iTH);
        if (_sdEvents.WaitOne(new TimeSpan(0, 0, 0, 2, 0), true))
        {
          logIt.Trace("About to exit Thread, {0}", iTH);
          break;
        }
        for(int i=0; i<299; i++)
        {
          logIt.Trace("{0}, Count:{1} and Random:{2}", iTH, i, r.Next(0, 1000));
        }
        Thread.Sleep(500);
      }
    }
    public void exitNow(Logger logIt)
    {
      logIt.Trace("Exit Thread");
      _sdEvents.Set();
      for (int i = 0; i < _MaxThread; i++)
      {
        iThread[i].Join();
      }
    }
  }
}
