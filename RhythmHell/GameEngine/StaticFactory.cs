using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace RhythmHell
{
    static class StaticFactory
    {
        public static List<Key> Clone(List<Key> original)
        {
            List<Key> copy = new List<Key>();

            for (int i = 0; i < original.Count; i++)
            {
                copy.Add(original[i]);
            }

            return copy;
        }

        

    }
}
