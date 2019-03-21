using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Resources;
using System;

namespace CodingArena.Main.Battlefields
{
    public class ResourceEventArgs : EventArgs
    {
        public Resource Resource { get; }

        public ResourceEventArgs([NotNull] Resource resource)
        {
            Resource = resource ?? throw new ArgumentNullException(nameof(resource));
        }
    }
}