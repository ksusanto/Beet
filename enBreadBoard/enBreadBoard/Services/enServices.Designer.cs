namespace enBreadBoard
{
  partial class enServices
  {
    private System.Diagnostics.EventLog enServiceLog;
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.enServiceLog = new System.Diagnostics.EventLog();
      ((System.ComponentModel.ISupportInitialize)(this.enServiceLog)).BeginInit();
      // 
      // ReportEventLog Event Log
      this.enServiceLog.Log = "Application";
      this.enServiceLog.Source = "Envision Service Application";
      // 
      // Envision ReportEventLog Service
      this.ServiceName = "Envision Service Application";
      ((System.ComponentModel.ISupportInitialize)(this.enServiceLog)).EndInit();
      //
      // Possible disable below line
      //components = new System.ComponentModel.Container();
    }

    #endregion
  }
}
