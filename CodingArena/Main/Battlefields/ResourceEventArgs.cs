using CodingArena.Annotations;
using CodingArena.Player;
using System;

namespace CodingArena.Main.Battlefields
{
    public class ResourceEventArgs : EventArgs
    {
        public IResource Resource { get; }

        public ResourceEventArgs([NotNull] IResource resource)
        {
            Resource = resource ?? throw new ArgumentNullException(nameof(resource));
        }
    }
}