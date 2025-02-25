using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;

namespace SS.Scenario {
    public partial class SSSperspectiveDecideScenario : XScenario {
        public class ChangeRotationScene : SSScene {
            //singleton pattern
            private static ChangeRotationScene mSingleton = null;
            public static ChangeRotationScene getSingleton() {
                Debug.Assert(ChangeRotationScene.mSingleton != null);
                return ChangeRotationScene.mSingleton;
            }
            public static ChangeRotationScene createSingleton(XScenario scenario) {
                Debug.Assert(ChangeRotationScene.mSingleton == null);
                ChangeRotationScene.mSingleton = new ChangeRotationScene(scenario);
                return ChangeRotationScene.mSingleton;
            }

            private ChangeRotationScene(XScenario scenario) : base(scenario) {}

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
            }
            public override void handleKeyUp(Key kc) {
                SSApp ss = (SSApp)this.mScenario.getApp();
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
                SSCmdToChangeRotation.execute(ss);
            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSperspectiveDecideScenario scenario =
                    (SSSperspectiveDecideScenario)this.mScenario;
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    scenario.getManipulatingTouchMarks().Remove(tm);
                    XCmdToChangeScene.execute(ss,
                    SSDefaultScenario.ReadyScene.getSingleton(), null);
                }
            }

            public override void wrapUp() {
            }
        }
    }
}