using System;

namespace X {
    public abstract class XLoggableCmd : XExecutable {
    //field
    protected XApp mApp = null;
    private DateTime mCreatedTime;

    //constructor
    protected XLoggableCmd(XApp app) {
        this.mApp = app;
        this.mCreatedTime = DateTime.Now;
    }

    public bool execute() {
        if (this.defineCmd()) {
                this.mApp.getLogMgr().addLog(this.createLog(this.createLogData()));
                return true;
        } else {
            return false;
        }
    }

    //abstract method
    protected abstract bool defineCmd();
    //protected abstract string createLog();
    // protected abstract string createLog();
    protected abstract XJson createLogData();

    // concrete methods
    private XLog createLog(XJson data) {
        XScene curScene = this.mApp.getScenarioMgr().getCurScene();
        XScenario curScenario = curScene.getScenario();
        string time = DateTime.Now.ToString("o"); // ISO 8601 format
        // see https://docs.microsoft.com/en-us/dotnet/standard/base-types/
        // standard-date-and-time-format-strings
        string scenario = curScenario.GetType().Name;
        string scene = curScene.GetType().Name;
        string cmd = this.getName();
        double timeTakenInMs = (DateTime.Now - this.mCreatedTime).
            TotalMilliseconds;
        XLog log = new XLog(time, scenario, scene, cmd, timeTakenInMs, data);
        return log;
    }
    public string getName() {
        return this.GetType().Name;
    }
    }
}
