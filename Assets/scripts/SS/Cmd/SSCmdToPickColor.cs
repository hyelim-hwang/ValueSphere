using UnityEngine;
using X;

namespace SS.Cmd {
    public class SSCmdToPickColor: XLoggableCmd {
        //fields
        int maxIteration = 10000;

        //private constructor
        private SSCmdToPickColor(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToPickColor cmd = new SSCmdToPickColor(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSCameraPerson cam = ss.getPerspCameraPerson();
            takeSnapshot(cam);
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("pickColor", this.GetType().Name);
            return data;
        }

        public void takeSnapshot(SSCameraPerson cam) {
            SSApp ss = (SSApp)this.mApp;
            //turn off the red line.
            ss.getValueSphereMgr().getValueSphere().getEquator().
            getGameObject().SetActive(false);
            ss.getValueSphereMgr().getValueSphere().getPole().getGameObject().
                SetActive(false);
            foreach (SSCursor2D tc in ss.getCursorMgr().getTouchCursors()) {
                tc.getGameObject().SetActive(false);
            }
            ss.getCursorMgr().getPenCursor().getGameObject().SetActive(false);
            // ss.getCanvas().GetComponent<Canvas>().enabled = false;
            //WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();
            ////yield return frameEnd;

            Texture2D screenshot = new Texture2D(Screen.width, Screen.height);
            screenshot.ReadPixels(
                new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenshot.Apply();
            Vector2 viewPos =
            ((SSApp)this.mApp).getPenMarkMgr().getLastPenMark().getLastPt();
            Color bla = screenshot.GetPixel((int)viewPos.x, (int)viewPos.y);
            //color calibration
            float RValue = bla.r;
            float GValue = bla.g;
            float BValue = bla.b;
            //Debug.LogWarning("setted with:" + bla);
            bool isInGreyScale = (RValue - GValue) < 0.3;
            while (!isInGreyScale && (maxIteration > 0)) {
                int newCoordX = (int)viewPos.x + 10;
                int newCoordY = (int)viewPos.y + 10;
                bla = screenshot.GetPixel(newCoordX, newCoordY);
                RValue = bla.r;
                GValue = bla.g;
                BValue = bla.b;
                isInGreyScale = (RValue - GValue) < 0.3;
                // RValue = bla.r;
                Debug.LogWarning("color modified to" + bla);
                maxIteration--;
            }
            ss.getValueStrokeMgr().setCurColor(bla);

            //turn the red line on.
            ss.getValueSphereMgr().getValueSphere().
            getEquator().getGameObject().SetActive(true);
            ss.getValueSphereMgr().getValueSphere().getPole().getGameObject().
                SetActive(true);
        }
    }
}