using System.Collections.Generic;
using UnityEngine;

namespace SS.AppObject {
    public abstract class SSAppObject {
        // fields
        protected string mID;
        public string getID() {
            return this.mID;
        }
        public void setID(string id) {
            this.mID = id;
        }
        protected GameObject mGameObject = null;
        public GameObject getGameObject() {
            return this.mGameObject;
        }
        protected List<SSAppObject> mChildren = null;
        public List<SSAppObject> getChildren() {
            return this.mChildren;
        }

        // constructor
        public SSAppObject(string name) {
            this.mID = name;
            this.mGameObject = new GameObject(name);
            this.mChildren = new List<SSAppObject>();
            this.addComponents();
        }

        // methods
        protected abstract void addComponents();

        public void addChild(SSAppObject child) {
            this.addChild(child, false);
        }
        public void removeChild(SSAppObject child) {
            this.removeChild(child, false);
        }

        // setting transform multiple times in one frame can cause error
        public void addChild(SSAppObject child, bool worldPosStays) {
            this.mChildren.Add(child);
            GameObject childGameObject = child.getGameObject();
            childGameObject.transform.SetParent(this.mGameObject.transform,
                worldPosStays);
        }
        public void removeChild(SSAppObject child, bool worldPosStays) {
            this.mChildren.Remove(child);
            GameObject childGameObject = child.getGameObject();
            childGameObject.transform.SetParent(null, worldPosStays);
        }

        public virtual void destroyGameObject() {
            GameObject.Destroy(this.mGameObject);
            foreach (SSAppObject child in this.mChildren) {
                child.destroyGameObject();
            }
        }

        public void setRenderQueue(int i) {
            this.mGameObject.GetComponent<Renderer>().material.renderQueue
                = i;
        }
    }
}