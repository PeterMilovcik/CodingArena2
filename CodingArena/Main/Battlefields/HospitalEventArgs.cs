using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Hospitals;
using System;

namespace CodingArena.Main.Battlefields
{
    public class HospitalEventArgs : EventArgs
    {
        public HospitalEventArgs([NotNull] Hospital hospital)
        {
            Hospital = hospital ?? throw new ArgumentNullException(nameof(hospital));
        }

        public Hospital Hospital { get; }
    }
}