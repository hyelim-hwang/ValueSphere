using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;
using SS.Scenario;
using SS.AppObject;

namespace SS.Scenario {
    public partial class SSStickHandleScenario : XScenario {
        public class ChangeSunlightPositionScene : SSScene {
            //singleton pattern
            private static ChangeSunlightPositionScene mSingleton = null;
            public static ChangeSunlightPositionScene getSingleton() {
                Debug.Assert(
                    ChangeSunlightPositionScene.mSingleton != null);
                return ChangeSunlightPositionScene.mSingleton;
            }
            public static ChangeSunlightPositionScene createSingleton(
                XScenario scenario) {
                Debug.Assert(
                    ChangeSunlightPositionScene.mSingleton == null);
                ChangeSunlightPositionScene.mSingleton =
                    new ChangeSunlightPositionScene(scenario);
                return ChangeSunlightPositionScene.mSingleton;
            }

            private ChangeSunlightPositionScene(XScenario scenario) :
                base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSShadowStickMgr SSMgr= ss.getShadowStickMgr();
                SSStickHandleScenario scenario =
                    (SSStickHandleScenario)this.mScenario;
                SSCameraPerson cam = ss.getGridCameraPerson();
                if (ss.getTouchMarkMgr().wasTouchDownJustNow()) {
                    SSTouchMark tm =
                        ss.getTouchMarkMgr().getLastDownTouchMark();
                    scenario.getManipulatingTouchMarks().Add(tm);
                }
            }

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {}

            public override void handlePenDown(Vector2 pt) {
            }

            public override void handlePenDrag(Vector2 pt) {}

            public override void handlePenUp(Vector2 pt) {}

            public override void handleEraserDown(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserDrag(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserUp(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleTouchDown() {
                //if one more touch is down, system goes to
                // local light change scene.
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSShadowStickMgr stickMgr = ss.getShadowStickMgr();
                SSStickHandleScenario scenario =
                    (SSStickHandleScenario)this.mScenario;
                SSTouchMark tm = ss.getTouchMarkMgr().getLastDownTouchMark();
                //convert tm to screen point.
                bool isLocalLightManipulation =
                    stickMgr.stickColliderChecker(tm.getLastPt()) == 3;
                if (isLocalLightManipulation) {
                    XCmdToChangeScene.execute(ss,
                        SSStickHandleScenario.
                        ChangeLocalLightPositionScene.getSingleton(), null);
                }
            }

            public override void handleTouchDrag() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToChangeLightDirectionByShadow.execute(ss);
            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSShadowStickMgr stickMgr = ss.getShadowStickMgr();
                SSStickHandleScenario scenario =
                    (SSStickHandleScenario)this.mScenario;
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    //remove currently manipulating stick from the manipulating
                    //stick list.
                    stickMgr.getManipulatingSticks().Clear();
                    scenario.getManipulatingTouchMarks().Remove(tm);
                    XCmdToChangeScene.execute(ss,
                        SSDefaultScenario.ReadyScene.getSingleton(), null);
                }
            }

            public override void wrapUp() {}

            //util function
        }
    }
}