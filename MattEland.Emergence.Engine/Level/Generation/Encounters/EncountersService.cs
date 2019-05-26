using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Randomizations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Level.Generation.Encounters
{
    /// <summary>
    /// A service for dealing with scripted encounters and applying encounters to existing rooms.
    /// </summary>
    public sealed class EncountersService
    {
        private readonly EncounterDataProvider _encounterDataProvider;
        private readonly EncounterSetDataProvider _encounterSetsDataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncountersService"/> class.
        /// </summary>
        public EncountersService()
        {
            // TODO: These should probably be injected
            _encounterDataProvider = new EncounterDataProvider();
            _encounterSetsDataProvider = new EncounterSetDataProvider();
        }

        /// <summary>
        /// Gets the available encounters.
        /// </summary>
        /// <returns>All available encounters.</returns>
        public IEnumerable<EncounterData> GetEncounters() => _encounterDataProvider.Items.ToList().AsReadOnly();

        /// <summary>
        /// Gets the encounter with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The encounter identifier.</param>
        /// <returns>The encounter or <c>null</c>.</returns>
        public EncounterData GetEncounter(string id) => _encounterDataProvider.GetItem(id);

        /// <summary>
        /// Gets a random encounter from the available encounter collection.
        /// </summary>
        /// <returns>A random encounter element.</returns>
        public EncounterData GetRandomEncounter(IRandomization randomization) => GetEncounters().GetRandomElement(randomization);

        /// <summary>
        /// Generates encounter elements for a specified encounter.
        /// </summary>
        /// <param name="encounter">The encounter.</param>
        /// <returns>IEnumerable&lt;EncounterElement&gt;.</returns>
        public IEnumerable<EncounterElement> GenerateElementsForEncounter(EncounterData encounter, IRandomization randomization)
        {
            var elements = new List<EncounterElement>();

            foreach (var contentElement in encounter.Entities)
            {
                decimal chance = contentElement.Chance;

                if (chance < 1.0m && chance < (decimal) randomization.GetDouble()) continue;

                // Some encounters can have varying quantities of actors in them.
                var quantity = GetEntityQuantity(randomization, contentElement);

                // Generate the required number of entries
                for (int i = 0; i < quantity; i++)
                {
                    elements.Add(new EncounterElement { ObjectType = GameObjectType.Actor, ObjectId = contentElement.Id });
                }
            }

            return elements;
        }

        private static int GetEntityQuantity(IRandomization randomization, EncounterEntityData contentElement)
        {
            int quantity = Math.Max(0, contentElement.MinQuantity);
            if (contentElement.MaxQuantity > contentElement.MinQuantity)
            {
                quantity = randomization.GetInt(contentElement.MinQuantity, contentElement.MaxQuantity);
            }

            return quantity;
        }

        /// <summary>
        /// Generates an encounter from the given encounter elements and adds game objects to vacant cells as needed.
        /// </summary>
        /// <param name="cells">The cells. These can be vacant or unvacant.</param>
        /// <param name="encounter">The encounter to generate objects from.</param>
        /// <returns>A collection of generated elements</returns>
        public IEnumerable<GameObjectBase> GenerateEncounter(IEnumerable<GameCell> cells, EncounterData encounter, IRandomization randomization)
        {
            var encounterElements = GenerateElementsForEncounter(encounter, randomization);

            return AddEncounterElementsToCells(encounterElements, cells, randomization);
        }

        /// <summary>
        /// Generates an encounter from the given encounter elements and adds game objects to vacant cells as needed.
        /// </summary>
        /// <param name="encounterElements">The encounter elements.</param>
        /// <param name="cells">The cells. These can be vacant or unvacant.</param>
        /// <returns>A collection of generated elements</returns>
        public static IEnumerable<GameObjectBase> AddEncounterElementsToCells(IEnumerable<EncounterElement> encounterElements, IEnumerable<GameCell> cells, IRandomization randomization)
        {
            IList<GameCell> remainingCells = cells.Where(c => !c.HasObstacle).ToList();

            var entities = new List<GameObjectBase>();
            foreach (EncounterElement element in encounterElements)
            {

                GameCell matchedCell = remainingCells.GetRandomElement(randomization);
                if (matchedCell == null)
                {
                    break;
                }

                remainingCells.Remove(matchedCell);

                var gameObject = CreationService.CreateObject(element.ObjectId, element.ObjectType, matchedCell.Pos);
                matchedCell.AddObject(gameObject);

                entities.Add(gameObject);
            }

            return entities;
        }

        /// <summary>
        /// Gets all available encounter sets.
        /// </summary>
        /// <returns>All available encounter sets.</returns>
        public IEnumerable<EncounterSet> GetEncounterSets()
        {
            return _encounterSetsDataProvider.Items;
        }

        /// <summary>
        /// Gets the encounter set with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The encounter set or null.</returns>
        public EncounterSet GetEncounterSet(string id)
        {
            if (id == null)
            {
                id = "default";
            }

            var encounterSet = _encounterSetsDataProvider.GetItem(id);

            if (encounterSet != null && encounterSet.EncounterIds == null)
            {
                encounterSet.EncounterIds = new List<string>();
            }

            return encounterSet;
        }

        /// <summary>
        /// Generates the encounter from the specified encounter set.
        /// </summary>
        /// <param name="cells">The cells.</param>
        /// <param name="encounterSetId">The encounter set identifier.</param>
        public void GenerateEncounterFromEncounterSet(ICollection<GameCell> cells, string encounterSetId, IRandomization randomization)
        {
            if (string.IsNullOrWhiteSpace(encounterSetId))
            {
                encounterSetId = "Default";
            }

            var encounterSet = GetEncounterSet(encounterSetId);
            var encounterId = encounterSet.EncounterIds.GetRandomElement(randomization);
            var encounter = GetEncounter(encounterId);

            GenerateEncounter(cells, encounter, randomization);
        }
    }
}