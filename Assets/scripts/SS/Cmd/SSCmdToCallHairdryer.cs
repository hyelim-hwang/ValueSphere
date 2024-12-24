using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using System.Text;
using SS.File;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToCallHairdryer : XLoggableCmd {
        //fields

        //private constructor
        private SSCmdToCallHairdryer(XApp app) : base(app) {

        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToCallHairdryer cmd = new SSCmdToCallHairdryer(app);
            return cmd.execute();
        }

        protected override bool defineCmd()
        {
            return this.readFile(
                "C:\\Users\\sketc\\Desktop\\SS3dSaveFiles\\hairdryer.SS3d");

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

                SS.getHairdryerUnderlay().getGameObject().SetActive(true);
                SS.getRobotUnderlay().getGameObject().SetActive(false);
                SS.getBuildingUnderlay().getGameObject().SetActive(false);

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

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("callHairdryer", this.GetType().Name);
            return data;
        }
    }
}