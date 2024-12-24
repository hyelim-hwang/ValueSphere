using UnityEngine;
using X;
using SS.Cmd;
using UnityEngine.InputSystem;
using SS.AppObject;

namespace SS.Scenario {
    public partial class SSDefaultScenario : XScenario {
        public class ReadyScene : SSScene {
            //singleton pattern
            private static ReadyScene mSingleton = null;
            public static ReadyScene getSingleton() {
                Debug.Assert(ReadyScene.mSingleton != null);
                return ReadyScene.mSingleton;
            }
            public static ReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(ReadyScene.mSingleton == null);
                ReadyScene.mSingleton = new ReadyScene(scenario);
                return ReadyScene.mSingleton;
            }

            private ReadyScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToAutoSave.execute(ss);
                //construct valueSphere here and initialize the position if there
                //exists sphere already.
                if (ss.getValueSphereMgr().getValueSphere() == null) {
                    ss.getValueSphereMgr().setValueSphere(
                    new SSValueSphere("valueSphere", 0f, Color.gray));
                    SSValueSphere vs = ss.getValueSphereMgr().getValueSphere();
                    Vector3 leftBottomPtInWorld =
                        ss.getPerspCameraPerson().getCamera().
                        ScreenToWorldPoint(new Vector2(0f, 0f));
                    vs.setRadius(0.7f);
                    vs.setPos(new Vector3(leftBottomPtInWorld.x / 2,
                        leftBottomPtInWorld.y / 2, 2));
                }
            }

            public override void handleKeyDown(Key kc) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                switch(kc) {
                    case Key.UpArrow:
                        SSCmdToIncreaseStrokeWidth.execute(ss);
                        break;
                    case Key.DownArrow:
                        SSCmdToDecreaseStrokeWidth.execute(ss);
                        break;
                    case Key.E:
                        XCmdToChangeScene.execute(ss,
                            SSEraseScenario.EraseScene.getSingleton(), this);
                        break;
                }
            }

            public override void handleKeyUp(Key kc) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                switch(kc) {
                    case Key.S:
                        SSCmdToSaveFile.execute(ss);
                        break;
                    case Key.O:
                        SSCmdToOpenFile.execute(ss);
                        SSCmdToAutoSave.execute(ss);
                        break;
                    case Key.H:
                        SSCmdToCallHairdryer.execute(ss);
                        break;
                    case Key.C:
                        SSCmdToclearAllValueStroke.execute(ss);
                        break;
                    case Key.R:
                        SSCmdToCallRobotArm.execute(ss);
                        break;
                    case Key.Z:
                        SSCmdToUndo.execute(ss);
                        break;
                    case Key.Y:
                        SSCmdToRedo.execute(ss);
                        break;

                }
            }

            public override void handlePenDown(Vector2 pt) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSValueSphere vs = ss.getValueSphereMgr().getValueSphere();
                //if pen touches the inner sphere area
                Vector3 penDownInWorldPt = ss.getPerspCameraPerson().getCamera().
                    ScreenToWorldPoint(new Vector3(pt.x, pt.y, 2.0f));
                penDownInWorldPt.z = 2f;

                if (vs != null && (penDownInWorldPt -
                    vs.getGameObject().transform.position).magnitude <
                    vs.getRadius() / 2) {
                    foreach (
                        SSCursor2D tc in ss.getCursorMgr().getTouchCursors()) {
                        tc.getGameObject().SetActive(false);
                    }
                    SSCmdToPickColor.execute(ss);
                    XCmdToChangeScene.execute(ss,
                        SSDrawScenario.DrawScene.getSingleton(), this);
                }
            }

            public override void handlePenDrag(Vector2 pt) {}

            public override void handlePenUp(Vector2 pt) {}

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
                SSValueSphere vs = ss.getValueSphereMgr().getValueSphere();
                if (ss.getTouchMarkMgr().wasTouchDownJustNow()) {
                    SSTouchMark tm =
                    ss.getTouchMarkMgr().getLastDownTouchMark();
                    Vector3 tmInWorld = ss.getPerspCameraPerson().getCamera().
                    ScreenToWorldPoint(tm.getFirstPt());
                    Vector3 tmInWorldAligned =
                        new Vector3(tmInWorld.x, tmInWorld.y, 2f);
                    //if the touch is in the sphere area,
                    //take the sphere out from
                    //the canvas corner.
                    if (Vector3.Distance(tmInWorldAligned,
                        vs.getSphere().transform.position) < vs.getRadius()) {
                        XCmdToChangeScene.execute(ss,
                        SSSphereHandleScenario.MoveSphereScene.getSingleton(),
                        this);
                    } else {
                        //navigate the canvas.
                        //XCmdToChangeScene.execute(ss,
                        //SSNavigateScenario.RotateReadyScene.getSingleton(),
                        //this);
                    }
                }
            }

            public override void handleTouchDrag() {
                //throw new System.NotImplementedException();
            }

            public override void handleTouchUp() {
                //throw new System.NotImplementedException();
            }

            public override void wrapUp() {}
        }
    }
}