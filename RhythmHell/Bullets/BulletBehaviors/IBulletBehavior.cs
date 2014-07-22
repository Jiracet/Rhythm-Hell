using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RhythmHell
{
    interface IBulletBehavior
    {
        Vector2 Update(Vector2 centerLocation, float timeManipulationFactor);
    }
}
