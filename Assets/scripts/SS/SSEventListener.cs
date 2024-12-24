using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SS {
    public class SSEventListener {
        //fields
        private SSApp mSS = null;

        //constructor
        public SSEventListener(SSApp ss) {
            this.mSS = ss;
        }

        //methods
        public void keyPressed(Key kc) {
            //Debug.Log("keyPressed" + kc);
            //delegate event to the scenes
            SSScene curScene = (SSScene)this.mSS.getScenarioMgr().getCurScene();
            curScene.handleKeyDown(kc);
        }

        public void keyReleased(Key kc) {
            //Debug.Log("keyReleased" + kc);
            SSScene curScene = (SSScene)this.mSS.getScenarioMgr().getCurScene();
            curScene.handleKeyUp(kc);
        }

        public void mouseMoved(Vector2 pt) {
            //update the cursor
            SSCursorMgr cursorMgr = this.mSS.getCursorMgr();
            cursorMgr.getPenCursor().getGameObject().transform.position = pt;
        }

        public void mouseLeftPressed(Vector2 pt) {
            //Debug.Log("mouseLeftPressed" + pt);
            //update the cursor
            SSCursorMgr cursorMgr = this.mSS.getCursorMgr();
            cursorMgr.getPenCursor().getGameObject().transform.position = pt;
            if (this.mSS.getPenMarkMgr().handlePenDown(pt)) {
                SSScene curScene =
                    (SSScene)this.mSS.getScenarioMgr().getCurScene();
                curScene.handlePenDown(pt);
            }
        }

        public void mouseLeftDragged(Vector2 pt) {
            //Debug.Log("mouseLeftDragged" + pt);
            //update the cursor
            SSCursorMgr cursorMgr = this.mSS.getCursorMgr();
            cursorMgr.getPenCursor().getGameObject().transform.position = pt;
            if (this.mSS.getPenMarkMgr().handlePenDrag(pt)) {
                SSScene curScene =
                    (SSScene)this.mSS.getScenarioMgr().getCurScene();
                curScene.handlePenDrag(pt);
            }
        }

        public void mouseLeftReleased(Vector2 pt) {
            //Debug.Log("mouseLeftReleased" + pt);
            //update the cursor
            SSCursorMgr cursorMgr = this.mSS.getCursorMgr();
            cursorMgr.getPenCursor().getGameObject().transform.position = pt;
            if (this.mSS.getPenMarkMgr().handlePenUp(pt)) {
                SSScene curScene =
                    (SSScene)this.mSS.getScenarioMgr().getCurScene();
                curScene.handlePenUp(pt);
            }
        }

        public void mouseRightPressed(Vector2 pt) {
            //Debug.Log("mouseRightPressed" + pt);
        }

        public void mouseRightDragged(Vector2 pt) {
            //Debug.Log("mouseRightDragged" + pt);
        }

        public void mouseRightReleased(Vector2 pt) {
            //Debug.Log("mouseRightReleased" + pt);
        }

        // pen
        public void penDown(Vector2 pt) {
            // TODO
            if (this.mSS.getPenMarkMgr().handlePenDown(pt)) {
                // activate pen cursor
                SSCursor2D penCursor = this.mSS.getCursorMgr().getPenCursor();
                penCursor.getGameObject().transform.position = pt;
                penCursor.getGameObject().SetActive(true);

                // call current scene
                SSScene curscene = (SSScene)this.mSS.getScenarioMgr().
                    getCurScene();
                curscene.handlePenDown(pt);
            }
        }

        public void penDragged(Vector2 pt) {
            // TODO
            if (this.mSS.getPenMarkMgr().handlePenDrag(pt)) {
                // update pen cursor
                SSCursor2D penCursor = this.mSS.getCursorMgr().getPenCursor();
                penCursor.getGameObject().transform.position = pt;

                // update collider physics
                Physics2D.Simulate(Time.fixedDeltaTime);

                // call current scene
                SSScene curscene = (SSScene)this.mSS.getScenarioMgr().
                   getCurScene();
                curscene.handlePenDrag(pt);
            }
        }

        public void penUp(Vector2 pt) {
            if (this.mSS.getPenMarkMgr().handlePenUp(pt)) {
                // update pen cursor
                SSCursor2D penCursor = this.mSS.getCursorMgr().getPenCursor();
                penCursor.getGameObject().transform.position = pt;

                // update collider physics
                Physics2D.Simulate(Time.fixedDeltaTime);

                // call current scene
                SSScene curscene = (SSScene)this.mSS.getScenarioMgr().
                    getCurScene();
                curscene.handlePenUp(pt);

                // deactivate pen cursor
                penCursor.getGameObject().SetActive(false);
            }
        }

        public void eraserDown(Vector2 pt) {
            if (this.mSS.getPenMarkMgr().handleEraserDown(pt)) {
                // activate pen cursor
                SSCursor2D eraserCursor = this.mSS.getCursorMgr().getPenCursor();
                eraserCursor.getGameObject().transform.position = pt;
                eraserCursor.getGameObject().SetActive(true);

                // update collider physics
                Physics2D.Simulate(Time.fixedDeltaTime);

                // call current scene
                SSScene curscene = (SSScene)this.mSS.getScenarioMgr().
                    getCurScene();
                curscene.handleEraserDown(pt);
            }
        }

        public void eraserDragged(Vector2 pt) {
            if (this.mSS.getPenMarkMgr().handleEraserDrag(pt)) {
                // update pen cursor
                SSCursor2D eraserCursor =
                    this.mSS.getCursorMgr().getPenCursor();
                eraserCursor.getGameObject().transform.position = pt;

                // update collider physics
                Physics2D.Simulate(Time.fixedDeltaTime);

                // call current scene
                SSScene curscene = (SSScene)this.mSS.getScenarioMgr().
                   getCurScene();
                curscene.handleEraserDrag(pt);
            }
        }

        public void eraserUp(Vector2 pt) {
            // TODO
            if (this.mSS.getPenMarkMgr().handleEraserUp(pt)) {
                // update pen cursor
                SSCursor2D eraserCursor =
                    this.mSS.getCursorMgr().getPenCursor();
                eraserCursor.getGameObject().transform.position = pt;

                // update collider physics
                Physics2D.Simulate(Time.fixedDeltaTime);

                // call current scene
                SSScene curscene = (SSScene)this.mSS.getScenarioMgr().
                    getCurScene();
                curscene.handleEraserUp(pt);

                // deactivate pen cursor
                eraserCursor.getGameObject().SetActive(false);
            }
        }

        public void touchDown(SSTouchPacket tp) {
            if (this.mSS.getTouchMarkMgr().touchDown(tp)) {
                //activate touch cursor
                SSCursor2D tc = new SSCursor2D(this.mSS, tp.getId(),
                    "TouchCursor", SSCursorMgr.TOUCH_RADIUS, tp.getPt());
                this.mSS.getCursorMgr().getTouchCursors().Add(tc);

                // update collider physics
                Physics2D.Simulate(Time.fixedDeltaTime);

                // set current touch state
                this.mSS.getTouchMarkMgr().setTouchDownWasJustNow(true);

                // call current scene
                SSScene curscene = (SSScene)this.mSS.getScenarioMgr().
                    getCurScene();
                curscene.handleTouchDown();

                // reset current touch state
                this.mSS.getTouchMarkMgr().setTouchDownWasJustNow(false);
            }
        }

        public void touchDragged(List<SSTouchPacket> tps) {
            if (this.mSS.getTouchMarkMgr().touchDrag(tps)) {
                //update touch cursor
                foreach (SSTouchMark tm in this.mSS.getTouchMarkMgr().
                    getDraggedTouchMarks()) {
                    SSCursor2D tc = this.mSS.getCursorMgr().findTouchCursor(tm);
                    if (tc != null) {
                        tc.getGameObject().transform.position = tm.getLastPt();
                    }
                }

                // update collider physics
                Physics2D.Simulate(Time.fixedDeltaTime);

                // set current touch state
                this.mSS.getTouchMarkMgr().setTouchDragWasJustNow(true);

                // call current scene
                SSScene curscene = (SSScene)this.mSS.getScenarioMgr().
                    getCurScene();
                curscene.handleTouchDrag();

                // reset current touch state
                this.mSS.getTouchMarkMgr().setTouchDragWasJustNow(false);
            }
        }

        public void touchUp(SSTouchPacket tp) {
            if (this.mSS.getTouchMarkMgr().touchUp(tp)) {
                // update touch cursor
                SSCursor2D tc = this.mSS.getCursorMgr().findTouchCursor(tp);
                if (tc != null) {
                    tc.getGameObject().transform.position = tp.getPt();
                }

                // update collider physics
                Physics2D.Simulate(Time.fixedDeltaTime);

                // set current touch state
                this.mSS.getTouchMarkMgr().setTouchUpWasJustNow(true);

                // call current scene
                SSScene curscene = (SSScene)this.mSS.getScenarioMgr().
                    getCurScene();
                curscene.handleTouchUp();

                // reset current touch state
                this.mSS.getTouchMarkMgr().setTouchUpWasJustNow(false);

                // deactivate touch cursor
                if (tc != null) {
                    this.mSS.getCursorMgr().getTouchCursors().Remove(tc);
                    tc.destroyGameObject();
                }
            }
        }
    }
}
