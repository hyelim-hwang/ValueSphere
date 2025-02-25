using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;

namespace SS.Scenario {
    public partial class SSSperspectiveDecideScenario : XScenario {
        public class DollyCameraScene : SSScene {
            //singleton pattern
            private static DollyCameraScene mSingleton = null;
            public static DollyCameraScene getSingleton() {
                Debug.Assert(DollyCameraScene.mSingleton != null);
                return DollyCameraScene.mSingleton;
            }
            public static DollyCameraScene createSingleton(XScenario scenario) {
                Debug.Assert(DollyCameraScene.mSingleton == null);
                DollyCameraScene.mSingleton = new DollyCameraScene(scenario);
                return DollyCameraScene.mSingleton;
            }

            private DollyCameraScene(XScenario scenario) : base(scenario) {}

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
            }
            public override void handleKeyUp(Key kc) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSperspectiveDecideScenario scenario =
                            (SSSperspectiveDecideScenario)this.mScenario;
                switch(kc) {
                    case Key.LeftCtrl:
                        scenario.getManipulatingTouchMarks().Clear();
                        XCmdToChangeScene.execute(ss,
                        SSDefaultScenario.ReadyScene.getSingleton(), null);
                        break;
                    case Key.LeftAlt:
                        XCmdToChangeScene.execute(ss,
                            SSSperspectiveDecideScenario.TumbleCameraScene.
                            getSingleton(),
                            this.mReturnScene);
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
            }

            public override void handleTouchDrag() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToDollyCamera.execute(ss);
            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSperspectiveDecideScenario scenario =
                    (SSSperspectiveDecideScenario)this.mScenario;
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    scenario.getManipulatingTouchMarks().Remove(tm);
                    XCmdToChangeScene.execute(ss,
                    this.mReturnScene, null);
                }
            }

            public override void wrapUp() {
            }
        }
    }
}