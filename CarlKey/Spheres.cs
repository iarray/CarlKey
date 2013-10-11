using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarlKey
{
    [Serializable]
    public class Spheres
    {
        public Keys[] _sps;

        public Keys this[int index]
        {
            get
            {
                return _sps[index];
            }
            set
            {
                _sps[index] = value;
            }
        }

        public Spheres(Keys s1, Keys s2, Keys s3)
        {
            _sps = new Keys[3] { s1, s2, s3 };
        }

        public Spheres(Keys s1)
        {
            _sps = new Keys[3] { s1, s1, s1 };
        }

        public Spheres()
        {
            _sps = new Keys[3] { Keys.E, Keys.E, Keys.E };
        }
    }
}
