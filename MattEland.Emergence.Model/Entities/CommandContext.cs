using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Model.Messages;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.Model.Entities
{
    public class CommandContext : ICommandContext
    {
        [NotNull, ItemNotNull]
        private readonly IEnumerable<WorldObject> _objects;

        [NotNull, ItemNotNull]
        private readonly IList<GameMessage> _messages = new List<GameMessage>();

        public CommandContext([NotNull] Actor actor, [NotNull] IEnumerable<WorldObject> objects)
        {
            _objects = objects ?? throw new ArgumentNullException(nameof(objects));
            Actor = actor ?? throw new ArgumentNullException(nameof(actor));
        }

        [NotNull] public Actor Actor { get; }
        
        public void MoveObject([NotNull] WorldObject obj, Position newPos)
        {            
            var oldPos = obj.Pos;
        
            obj.Pos = newPos;
        
            AddMessage(new MovedMessage(obj, oldPos, newPos));
        }

        private void AddMessage([NotNull] GameMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            
            _messages.Add(message);
        }

        public void MoveExecutingActor(Position newPos) => MoveObject(Actor, newPos);
        public void UpdateObject(WorldObject gameObject) => AddMessage(new ObjectUpdatedMessage(gameObject));

        public void UpdateCapturedCores()
        {
            // Check to see if all cores are captured
            var cores = _objects.OfType<Core>().ToList();
            int totalCores = cores.Count;
            int capturedCores = cores.Count(c => c.IsCaptured);
            var isOpen = capturedCores >= totalCores;

            // Display messages to the user
            DisplayText($"{capturedCores} of {totalCores} are now under your control.");
            if (isOpen)
            {
                DisplayText("You can now breach the firewall.");
            }

            // Firewalls may have opened / closed
            _objects.OfType<Firewall>().Each(firewall =>
            {
                firewall.IsOpen = isOpen;
                UpdateObject(firewall);
            });
        }

        public void DisplayText(string text) => AddMessage(new DisplayTextMessage(text));

        public IEnumerable<GameMessage> Messages => _messages;
    }
}