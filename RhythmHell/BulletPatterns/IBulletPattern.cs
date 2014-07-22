using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RhythmHell
{
    interface IBulletPattern
    {
        BulletPool BulletPool { get; set; } //the bullet pool the pattern belongs to
        bool IsReverse { get; set; }

        void Update();
    }
}
