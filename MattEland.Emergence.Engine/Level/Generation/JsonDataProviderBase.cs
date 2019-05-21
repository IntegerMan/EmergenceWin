using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MattEland.Emergence.Engine.Level.Generation
{
    /// <summary>
    /// A base data provider
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class JsonDataProviderBase<T>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDataProviderBase{T}"/> class.
        /// </summary>
        protected JsonDataProviderBase()
        {
            _items = new Lazy<IEnumerable<T>>(LoadItems);
        }

        private readonly Lazy<IEnumerable<T>> _items;

        /// <summary>
        /// Loads the items into the collection.
        /// </summary>
        /// <returns>The items loaded</returns>
        private IEnumerable<T> LoadItems()
        {
            var json = SourceJson;

            if (string.IsNullOrWhiteSpace(json))
            {
                throw new InvalidOperationException($"SourceJson not specified on {GetType().FullName}");
            }

            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            }
            catch (JsonException ex)
            {
                throw new JsonException($"Could not read from JSON {json}", ex);
            }
        }

        /// <summary>
        /// Gets the source JSON data for feeding the <see cref="Items"/> collection.
        /// </summary>
        /// <value>The source JSON.</value>
        protected abstract string SourceJson {get;}

        /// <summary>
        /// Gets the collection items associated with the provider.
        /// </summary>
        /// <remarks>
        /// This functions by lazy loading the items from the <see cref="SourceJson"/> property.
        /// </remarks>
        /// <value>The items.</value>
        public IEnumerable<T> Items => _items.Value;

        /// <summary>
        /// Gets an item with the specified <paramref name="id"/> from the collection of items.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <returns>The item or <c>null</c> if no item could be found.</returns>
        public abstract T GetItem(string id);

    }
}