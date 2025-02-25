using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;

namespace SS.Scenario {
    public partial class SSSperspectiveDecideScenario : XScenario {
        public class ChangePositionScene : SSScene {
            //singleton pattern
            private static ChangePositionScene mSingleton = null;
            public static ChangePositionScene getSingleton() {
                Debug.Assert(ChangePositionScene.mSingleton != null);
                return ChangePositionScene.mSingleton;
            }
            public static ChangePositionScene createSingleton(XScenario scenario) {
                Debug.Assert(ChangePositionScene.mSingleton == null);
                ChangePositionScene.mSingleton = new ChangePositionScene(scenario);
                return ChangePositionScene.mSingleton;
            }

            private ChangePositionScene(XScenario scenario) : base(scenario) {}

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
                SSCmdToChangePosition.execute(ss);
            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSperspectiveDecideScenario scenario =
                    (SSSperspectiveDecideScenario)this.mScenario;
                SSPerspectiveCubeMgr cubeMgr = ss.getSSPerspectiveCubeMgr();
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