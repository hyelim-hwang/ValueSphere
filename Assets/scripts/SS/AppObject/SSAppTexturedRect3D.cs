using SS.Geom;
using SS.AppObject;
using UnityEngine;

namespace SS.AppObject {
    public class SSAppTexturedRect3D : SSAppGeom3D {
        // fields
        private RenderTexture mTexture;
        public void setTexture(RenderTexture texture) {
            this.mTexture = texture;
            this.refreshRenderer();
        }

        private Vector2 mTextureOffset;
        public void setTextureOffset(float offset) {
            this.setTextureOffset(new Vector2(offset, offset));
            this.refreshRenderer();
        }

        private Vector2 mTextureCropRatio;

        private float mOpacity;
        protected void setOpacity(float opacity) {
            this.mOpacity = opacity;
            this.refreshRenderer();
        }

        // constructor
        public SSAppTexturedRect3D(
            string name, float width, float height
        ) : base($"{name}/ TexturedRect3D") {
            this.mGeom = new SSRect3D(
                width, height, Vector3.zero, Quaternion.Euler(0, 180, 0)
            );

            this.mTextureOffset = Vector2.zero;
            this.mTextureCropRatio = new Vector2(0.5f, 0.5f);

            this.refreshAtGeomChange();
        }

        // methods
        public void setSize(float width, float height) {
            SSRect3D rect = (SSRect3D)this.mGeom;
            this.mGeom = new SSRect3D(
                width, height, rect.getPos(), rect.getRot()
            );
            this.refreshAtGeomChange();
        }

        public Vector2 getTexturePixelSize() {
            return new Vector2(
                this.mTexture.width, this.mTexture.height
            );
        }

        public void setTextureOffset(Vector2 offset) {
            this.mTextureOffset = offset;
            this.refreshRenderer();
        }

        public void setTextureCropRatio(Vector2 dimension) {
            this.mTextureCropRatio = dimension;
            this.refreshRenderer();
        }

        protected override void addComponents() {
            this.mGameObject.AddComponent<MeshFilter>();
            this.mGameObject.AddComponent<MeshRenderer>();
            this.mGameObject.AddComponent<MeshCollider>();
        }

        protected override void refreshRenderer() {
            SSRect3D rect = (SSRect3D)this.mGeom;

            MeshFilter mf = this.mGameObject.GetComponent<MeshFilter>();
            mf.mesh = rect.calcMesh();

            float textureOffsetX = (this.mTextureOffset.x + 1f) / 2f;
            float textureOffsetY = (this.mTextureOffset.y + 1f) / 2f;

            Renderer r = this.mGameObject.GetComponent<Renderer>();
            r.material = new Material(Shader.Find("Unlit/Texture"));
            r.material.SetTexture("_MainTex", this.mTexture);
            r.material.SetFloat("_Opacity", this.mOpacity);
            r.material.SetFloat("_TextureOffsetX", textureOffsetX);
            r.material.SetFloat("_TextureOffsetY", textureOffsetY);
            r.material.SetFloat("_TextureScaleX", 1 / this.mTextureCropRatio.x);
            r.material.SetFloat("_TextureScaleY", 1 / this.mTextureCropRatio.y);
        }

        protected override void refreshCollider() {
            MeshCollider mc = this.mGameObject.GetComponent<MeshCollider>();
            mc.sharedMesh = this.mGameObject.GetComponent<MeshFilter>().mesh;
        }
    }
}
