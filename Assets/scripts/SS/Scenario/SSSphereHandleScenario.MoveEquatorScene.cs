using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;

namespace SS.Scenario {
    public partial class SSSphereHandleScenario : XScenario {
        public class MoveEquatorScene : SSScene {
            //singleton pattern
            private static MoveEquatorScene mSingleton = null;
            public static MoveEquatorScene getSingleton() {
                Debug.Assert(MoveEquatorScene.mSingleton != null);
                return MoveEquatorScene.mSingleton;
            }
            public static MoveEquatorScene createSingleton(XScenario scenario) {
                Debug.Assert(MoveEquatorScene.mSingleton == null);
                MoveEquatorScene.mSingleton = new MoveEquatorScene(scenario);
                return MoveEquatorScene.mSingleton;
            }

            private MoveEquatorScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSphereHandleScenario scenario =
                    (SSSphereHandleScenario)this.mScenario;
                SSValueSphereMgr valueSphereMgr = ss.getValueSphereMgr();
                //hide sphere.
                valueSphereMgr.makeSphereTransparent();
            }

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {}

            public override void handlePenDown(Vector2 pt) {}

            public override void handlePenDrag(Vector2 pt) {
                //modify equator here.
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToMoveEquator.execute(ss);
            }

            public override void handlePenUp(Vector2 pt) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(ss,
                    SSSphereHandleScenario.SphereMoveNScaleScene.getSingleton(),
                    null);
            }

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
                //throw new System.NotImplementedException();
            }

            public override void handleTouchDrag() {
                //throw new System.NotImplementedException();

            }

            public override void handleTouchUp() {
                //throw new System.NotImplementedException();s
            }

            public override void wrapUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSValueSphereMgr valueSphereMgr = ss.getValueSphereMgr();
                //show sphere.
                valueSphereMgr.makeSphereShow();
            }
        }
    }
}