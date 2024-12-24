using System;
using SS.AppObject;
using SS.File;
using UnityEditor;
using UnityEngine;
using X;

namespace SS.Cmd {
    public class SSCmdToOpenFile : XLoggableCmd {
        //fields
        private string mFilePath = string.Empty;

        // constructor
        private SSCmdToOpenFile(XApp app) : base(app) {}

        // static method to construct and execute this method
        public static bool execute(XApp app) {
            SSCmdToOpenFile cmd = new SSCmdToOpenFile(app);
            return cmd.execute();
        }

        // private constructor
        protected override bool defineCmd() {
            SSApp SS = (SSApp) this.mApp;
            // open dialog at Desktop
            this.mFilePath = EditorUtility.OpenFilePanel("Open",
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "SS3d");

            // pressed 'OPEN' button
            if (this.mFilePath != string.Empty) {
                if (this.readFile(this.mFilePath)) {
                    SS.getSnapshotMgr().takeSnapshot();
                    // prevent undo by unlinking the snapshot to prev snapshot
                    SS.getSnapshotMgr().getCurSnapshot().setPrevSnapshot(null);
                    return true;
                } else {
                    return false;
                }

            // pressed 'CANCEL' button
            } else {
                return false;
            }
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("openFile", this.GetType().Name);
            return data;
        }

        private bool readFile(string filePath) {
            SSApp SS = (SSApp)this.mApp;
            // parse json file (it may fail if format is incorrect)
            try {
                string json = System.IO.File.ReadAllText(filePath);
                SSSerializableSavedData sSavedData = JsonUtility.
                    FromJson<SSSerializableSavedData>(json);
                SSSavedData savedData = sSavedData.toSavedData();

                // clear existing valueStrokes
                foreach (SSValueStroke vs in SS.getValueStrokeMgr().
                    getValueStrokes()) {
                    vs.destroyGameObject();
                }
                SS.getValueStrokeMgr().getValueStrokes().Clear();

                // load file
                SS.getPerspCameraPerson().setEye(savedData.getEye());
                SS.getPerspCameraPerson().setView(savedData.getView());
                Vector3 leftBottomPtInWorld =
                    SS.getPerspCameraPerson().getCamera().
                    ScreenToWorldPoint(new Vector2(0f, 0f));
                foreach (SSValueStroke vst in savedData.getValueStrokes()) {
                    SS.getValueStrokeMgr().getValueStrokes().Add(vst);
                }
                SS.getValueStrokeMgr().updateValueCoordinate();
                return true;
            } catch {
                return false;
            }
        }
    }
}
