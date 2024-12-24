using System;
using SS.File;
using UnityEditor;
using UnityEngine;
using X;

namespace SS.Cmd {
    public class SSCmdToSaveFile : XLoggableCmd {
        //fields
        private string mFilePath = string.Empty;

        // constructor
        private SSCmdToSaveFile(XApp app) : base(app) {}

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToSaveFile cmd = new SSCmdToSaveFile(app);
            return cmd.execute();
        }

        // private constructor
        protected override bool defineCmd() {
            SSApp SS = (SSApp) this.mApp;

            // open dialog at Desktop
            this.mFilePath = EditorUtility.SaveFilePanel("Save",
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                this.createDefaultFileName(), "SS3d");

            // pressed 'SAVE' button
            if (this.mFilePath != string.Empty) {
                SSCmdToSaveFile.writeSketchFile(SS, this.mFilePath);
                return true;

            // pressed 'CANCEL' button
            } else {
                return false;
            }
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("saveFile", this.GetType().Name);
            return data;
        }

        private string createDefaultFileName() {
            return DateTime.Now.ToString("yyyy_MMdd_HHmm_ss") + "_SKETCH";
        }

        public static void writeSketchFile(SSApp SS, string filePath) {
            // make new json file
            SSSavedData savedData = new SSSavedData(DateTime.Now,
                SS.getPerspCameraPerson().getEye(), SS.getPerspCameraPerson().
                getView(), SS.getValueStrokeMgr().getValueStrokes());
            SSSerializableSavedData sSavedData = new SSSerializableSavedData(
                savedData);
            string json = JsonUtility.ToJson(sSavedData);

            // write file
            System.IO.File.WriteAllText(filePath, json);
        }
    }
}