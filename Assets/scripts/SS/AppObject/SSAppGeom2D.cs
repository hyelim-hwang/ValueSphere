using SS.Geom;

namespace SS.AppObject
{
    public abstract class SSAppGeom2D : SSAppObject2D
    {
        //fields
        protected SSGeom2D mGeom = null;

        public SSGeom2D getGeom()
        {
            return this.mGeom;
        }

        public void setGeom(SSGeom2D geom)
        {
            this.mGeom = geom;
            this.refreshAtGeomChange();
        }

        //constructor
        public SSAppGeom2D(string name) : base(name)
        {

        }

        //methods
        public void refreshAtGeomChange()
        {
            this.refreshRenderer();
            this.refreshCollider();
        }

        protected abstract void refreshRenderer();
        protected abstract void refreshCollider();
    }
}

