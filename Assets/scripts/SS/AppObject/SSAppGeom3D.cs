using SS.Geom;
using UnityEngine;

namespace SS.AppObject
{
    public abstract class SSAppGeom3D : SSAppObject3D
    {
        protected SSGeom3D mGeom = null;

        public SSGeom3D getGeom()
        {
            return this.mGeom;
        }

        public void setGeom(SSGeom3D geom)
        {
            this.mGeom = geom;
            this.refreshAtGeomChange();
        }
        public Collider getCollider()
        {
            return this.mGameObject.GetComponent<Collider>();
        }

        public SSAppGeom3D(string name) : base(name)
        {

        }

        public void refreshAtGeomChange()
        {
            this.refreshRenderer();
            this.refreshCollider();
        }

        protected abstract void refreshRenderer();
        protected abstract void refreshCollider();
    }
}

