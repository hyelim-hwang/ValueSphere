using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;

namespace SS.Scenario {
    public partial class SSSperspectiveDecideScenario : XScenario {
        public class CubeHandleReadyScene : SSScene {
            //singleton pattern
            private static CubeHandleReadyScene mSingleton = null;
            public static CubeHandleReadyScene getSingleton() {
                Debug.Assert(CubeHandleReadyScene.mSingleton != null);
                return CubeHandleReadyScene.mSingleton;
            }
            public static CubeHandleReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(CubeHandleReadyScene.mSingleton == null);
                CubeHandleReadyScene.mSingleton = new CubeHandleReadyScene(scenario);
                return CubeHandleReadyScene.mSingleton;
            }

            private CubeHandleReadyScene(XScenario scenario) : base(scenario) {}

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
                        XCmdToChangeScene.execute(ss,
                        SSDefaultScenario.ReadyScene.getSingleton(), null);
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
                XCmdToChangeScene.execute(ss,
                    SSSperspectiveDecideScenario.TumbleCameraScene.getSingleton(),
                    this.mReturnScene);
            }

            public override void handleTouchDrag() {
            }

            public override void handleTouchUp() {
                // SSApp ss = (SSApp)this.mScenario.getApp();
                // SSSperspectiveDecideScenario scenario =
                //     (SSSperspectiveDecideScenario)this.mScenario;
                // SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                // if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                //     scenario.getManipulatingTouchMarks().Remove(tm);
                //     XCmdToChangeScene.execute(ss,
                //     this.mReturnScene, null);
                // }
            }

            public override void wrapUp() {
            }
        }
    }
}