using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;

namespace SS.Scenario
{
    public partial class SSSperspectiveDecideScenario : XScenario
    {
        public class TumbleCameraScene : SSScene
        {
            //singleton pattern
            private static TumbleCameraScene mSingleton = null;
            public static TumbleCameraScene getSingleton() {
                Debug.Assert(TumbleCameraScene.mSingleton != null);
                return TumbleCameraScene.mSingleton;
            }
            public static TumbleCameraScene createSingleton(XScenario scenario) {
                Debug.Assert(TumbleCameraScene.mSingleton == null);
                TumbleCameraScene.mSingleton = new TumbleCameraScene(scenario);
                return TumbleCameraScene.mSingleton;
            }

            private TumbleCameraScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSperspectiveDecideScenario scenario =
                    (SSSperspectiveDecideScenario)this.mScenario;
                if (ss.getTouchMarkMgr().wasTouchDownJustNow()) {
                    SSTouchMark tm =
                    ss.getTouchMarkMgr().getLastDownTouchMark();
                    scenario.getManipulatingTouchMarks().Add(tm);
                }
            }
            public override void handleKeyDown(Key kc) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                switch(kc) {
                    case Key.LeftAlt:
                        SSSperspectiveDecideScenario scenario =
                            (SSSperspectiveDecideScenario)this.mScenario;
                        XCmdToChangeScene.execute(ss,
                            SSSperspectiveDecideScenario.DollyCameraScene.
                            getSingleton(), this.mReturnScene);
                        break;
                }
            }
            public override void handleKeyUp(Key kc) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                switch(kc) {
                    case Key.LeftCtrl:
                        SSSperspectiveDecideScenario scenario =
                            (SSSperspectiveDecideScenario)this.mScenario;
                        scenario.getManipulatingTouchMarks().Clear();
                        XCmdToChangeScene.execute(ss,
                            SSDefaultScenario.ReadyScene.
                                getSingleton(), null);
                        break;
                }
            }
            public override void handlePenUp(Vector2 pt) {
            }
            public override void handlePenDrag(Vector2 pt) {
            }
            public override void handlePenDown(Vector2 pt) {
            }

            public override void handleEraserDown(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserUp(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserDrag(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleTouchDown() {
                SSApp ss = (SSApp)this.mScenario.getApp();
            }

            public override void handleTouchDrag() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToTumbleCamera.execute(ss);
            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSperspectiveDecideScenario scenario =
                    (SSSperspectiveDecideScenario)this.mScenario;
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    scenario.getManipulatingTouchMarks().Remove(tm);
                    XCmdToChangeScene.execute(ss,
                    SSSperspectiveDecideScenario.CubeHandleReadyScene.
                    getSingleton(), this);
                }
            }

            public override void wrapUp() {
            }
        }
    }
}