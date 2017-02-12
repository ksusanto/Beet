using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace enBreadBoard
{
  [RunInstaller(true)]
  public partial class enInstaller : System.Configuration.Install.Installer
  {
    private ServiceInstaller svcInstaller;
    private ServiceProcessInstaller prcInstaller;
    public enInstaller()
    {
      //
      // Instantiate installers for process and services.
      prcInstaller = new ServiceProcessInstaller();
      svcInstaller = new ServiceInstaller();

      prcInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
      prcInstaller.Password = null;
      prcInstaller.Username = null;
      //
      //  Add Service Type and and Service Name
      svcInstaller.StartType = ServiceStartMode.Automatic;
      svcInstaller.ServiceName = "EnvisionServiceApp";
      svcInstaller.DisplayName = "Envision Service App";
      svcInstaller.Description = "Envision Service Application";
      //
      //  Add Installers to collector
      Installers.Add(svcInstaller);
      Installers.Add(prcInstaller);
      InitializeComponent();
    }

    public override void Install(IDictionary stateSaver)
    {
      string aAsmPath = "/assemblypath=\"{0}\" --service";
      Context = new InstallContext("", new string[] { String.Format(aAsmPath, System.Reflection.Assembly.GetExecutingAssembly().Location) });
      base.Install(stateSaver);
    }
  }
}
