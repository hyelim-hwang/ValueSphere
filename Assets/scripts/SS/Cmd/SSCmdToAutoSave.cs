using X;
using UnityEngine;
using System;
using System.IO;
using System.Text;

namespace SS.Cmd {
    public class SSCmdToAutoSave : XLoggableCmd {
        // constants
        private static readonly TimeSpan ONE_MINUTE = new TimeSpan(0, 1, 0);
        private static readonly string AUTO_SAVE_DIR_NAME = "AutoSave";

        // fields
        private static DateTime lastAutoSavedTime = DateTime.Now;
        private bool mShouldSaveNow = false;
        private DateTime mCurTime;
        private string mLogFilePath = string.Empty;
        private string mSketchFilePath = string.Empty;

        // private constructor
        private SSCmdToAutoSave(XApp app, bool shouldSaveNow) : base(app) {
            this.mShouldSaveNow = shouldSaveNow;
            this.mCurTime = DateTime.Now;

            // create file paths for log and sketch
            string desktopPath = Environment.GetFolderPath(Environment.
                SpecialFolder.Desktop);
            string autoSaveDirPath = Path.Combine(desktopPath, SSCmdToAutoSave.
                AUTO_SAVE_DIR_NAME);
            string dateTime = this.mCurTime.ToString("yyyy_MMdd_HHmm_ss");
            string logFileName = $"{ dateTime }_LOG.json";
            string sketchFileName = $"{ dateTime }_SKETCH.SS3d";
            this.mLogFilePath = Path.Combine(autoSaveDirPath, logFileName);
            this.mSketchFilePath =
                Path.Combine(autoSaveDirPath, sketchFileName);
        }

        // static method to construct and execute this command
        public static bool execute(XApp app, bool shouldSaveNow = false) {
            SSCmdToAutoSave cmd = new SSCmdToAutoSave(app, shouldSaveNow);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            TimeSpan timeSpan = this.mCurTime - SSCmdToAutoSave.
                lastAutoSavedTime;

            if (timeSpan > SSCmdToAutoSave.ONE_MINUTE || this.mShouldSaveNow) {
                try {
                    SSCmdToAutoSave.writeLogFile(ss, this.mLogFilePath);
                    SSCmdToSaveFile.writeSketchFile(ss, this.mSketchFilePath);
                    SSCmdToAutoSave.lastAutoSavedTime = this.mCurTime;
                    Debug.Log("Autosaved.");
                    return true;
                } catch {
                    Debug.LogError("Must create 'AutoSave' folder at Desktop!");
                    return false;
                }
            } else {
                return false;
            }
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("shouldSaveNow", this.mShouldSaveNow);
            // "\" is a JSON escape character, so replace it with "/"
            data.addMember("logFilePath", this.mLogFilePath.Replace('\\', '/'));
            data.addMember("sketchFilePath", this.mSketchFilePath.Replace('\\',
                '/'));
            return data;
        }

        // private methods
        public static void writeLogFile(SSApp ss, string filePath) {
            XJson json = new XJson();
            json.addMember("logs", ss.getLogMgr().getLogs());
            System.IO.File.WriteAllText(filePath, json.ToString());
        }

    }
}
