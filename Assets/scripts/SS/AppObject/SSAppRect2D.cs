using System.Collections.Generic;
using UnityEngine;

namespace SS.AppObject {
    public class SSAppRect2D : SSAppGeom2D {
        // fields
        private Color mColor = Color.red; // easily noticable color
        public Color getColor() { return this.mColor; }
        public void setColor(Color c) {
            this.mColor = c;
            this.refreshRenderer();
        }
        public void setSize(float width, float height) {
            SSRect2D rect = (SSRect2D) this.mGeom;
            this.mGeom = new SSRect2D(width, height, rect.getPos(), rect.getRot());
            this.refreshAtGeomChange();
        }
        public Vector2 getSize() {
            SSRect2D rect = (SSRect2D) this.mGeom;
            return new Vector2(rect.getWidth(), rect.getHeight());
        }
        public void setPosition(Vector2 pos) {
            this.getGameObject().transform.position = pos;
        }
        public Vector2 getPosition() {
            return this.getGameObject().transform.position;
        }
        private SSAppPolyline2D mEdge = null;
        public SSAppPolyline2D getEdge() {
            return this.mEdge;
        }

        // constructor
        public SSAppRect2D(string name, float width, float height, Color color) :
            base($"{ name }/Rect2D") {

            this.mGeom = new SSRect2D(width, height, Vector3.zero,
                Quaternion.identity);
            this.mColor = color;
            this.refreshAtGeomChange();
        }

        protected override void addComponents() {
            this.mGameObject.AddComponent<MeshFilter>();
            this.mGameObject.AddComponent<MeshRenderer>();
            this.mGameObject.AddComponent<Rigidbody2D>();
            this.mGameObject.AddComponent<BoxCollider2D>();

            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            Material mat = new Material(Shader.Find("UI/Unlit/Transparent"));
            mr.material = mat;

            Rigidbody2D rb =this.mGameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.useFullKinematicContacts = true;
        }

        protected override void refreshRenderer() {
            SSRect2D rect = (SSRect2D) this.mGeom;
            MeshFilter mf = this.mGameObject.GetComponent<MeshFilter>();
            mf.mesh = rect.calcMesh();
            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            mr.material.color = this.mColor;
        }

        protected override void refreshCollider() {
            SSRect2D rect = (SSRect2D) this.mGeom;
            BoxCollider2D bc = this.mGameObject.GetComponent<BoxCollider2D>();
            bc.size = new Vector2(rect.getWidth(), rect.getHeight());
        }
    }
}