using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using enBreadBoard.Utility;
using NLog;

namespace enBreadBoard
{
  partial class enServices : ServiceBase
  {
    private string[] _inline_args;
    Program _pr = new Program();
    Logger _logIt = LogManager.GetCurrentClassLogger();

    public enServices(string[] inline_args)
    {
      _inline_args = inline_args;
      InitializeComponent();
    }

    protected override void OnStart(string[] args)
    {
      Version v = Assembly.GetExecutingAssembly().GetName().Version;
      string aboutV = string.Format(@"ReportServer Ver. {0}.{1}.{2} (r{3})", v.Major, v.Minor, v.Build, v.Revision);
      this.enServiceLog = new System.Diagnostics.EventLog();
      this.enServiceLog.Log = "Application";
      this.enServiceLog.Source = "Envision Server Service";
      this.enServiceLog.WriteEntry(aboutV + " -- Error log check Nlog.config" +
                                      @" -- config.xml must be in " + __CONST.DLDir);
      _pr.exeThread(_logIt);
    }

    protected override void OnStop()
    {
      _pr.exitNow(_logIt);
    }
  }
}
