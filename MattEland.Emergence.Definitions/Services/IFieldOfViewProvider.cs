using System.Collections.Generic;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Services
{
    public interface IFieldOfViewProvider
    {
        /// <summary>
        /// Check if the Cell is in the currently computed field-of-view
        /// Field-of-view must first be calculated by calling ComputeFov and/or AppendFov
        /// </summary>
        /// <remarks>
        /// Field-of-view (FOV) is basically a calculation of what is observable in the Map from a given Cell with a given light radius
        /// </remarks>
        /// <example>
        /// Field-of-view can be used to simulate a character holding a light source and exploring a Map representing a dark cavern
        /// Any Cells within the FOV would be what the character could see from their current location and lighting conditions
        /// </example>
        /// <param name="pos">The location of the Cell to check</param>
        /// <returns>True if the Cell is in the currently computed field-of-view, false otherwise</returns>
        bool IsInFov(Pos2D pos);

        /// <summary>
        /// Gets a collection of positions marked as currently visible by the algorithm.
        /// </summary>
        ISet<Pos2D> VisiblePositions { get; }
    }
}