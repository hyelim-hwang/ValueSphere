using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using System.Text;
using SS.Scenario;

namespace SS.Cmd {
    public class SSCmdToCheckHighlight : XLoggableCmd {
        //fields
        private Texture2D mScreenshot;
        private Color mBLA;

        //private constructor
        private SSCmdToCheckHighlight(XApp app) : base(app) {

        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToCheckHighlight cmd = new SSCmdToCheckHighlight(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp SS = (SSApp)this.mApp;
            SSPerspCameraPerson cp = new SSPerspCameraPerson();
            Camera tempCam = cp.getCamera();

            SS.StartCoroutine(takeScreenshot(tempCam));

            if (this.mBLA == Color.white) {
                return true;
            } else {
                return false;
            }
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("checkHighlight", this.GetType().Name);
            return data;
        }

        private IEnumerator takeScreenshot(Camera tempCam) {
            SSApp SS = (SSApp)this.mApp;
            yield return new WaitForEndOfFrame();
            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = tempCam.targetTexture;
            RenderTexture tex =
                new RenderTexture(Screen.width, Screen.height, 24);
            this.mScreenshot = new Texture2D(Screen.width, Screen.height);
            tempCam.targetTexture = tex;
            this.mScreenshot.
                ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            this.mScreenshot.Apply();
            RenderTexture.active = currentRT;
            yield return new WaitForEndOfFrame();

            SSSphereHandleScenario scenario =
                SSSphereHandleScenario.getSingleton();
            SSTouchMark tm = scenario.getManipulatingTouchMarks()[0];
            Vector2 curPt = tm.getRecentPt(0);
            Vector2 viewPos = curPt;
            this.mBLA = this.mScreenshot.
                GetPixel((int)viewPos.x, (int)viewPos.y);
            yield return new WaitForEndOfFrame();
        }
    }
}