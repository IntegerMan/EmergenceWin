﻿using System.Linq;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.AI.Brains
{
    public class LogicBombBrain : HunterBrain
    {
        private const string StatePrimed = "Primed";

        public LogicBombBrain(params Alignment[] targets) : base(targets)
        {
        }

        public override void UpdateActorState(ICommandContext context, IActor actor, IGameCell choice)
        {
            if (choice?.Actor != null && ArtificialIntelligenceService.IsOpposingTeam(actor.Team, choice.Actor.Team))
            {
                Prime(context, actor);
            }
        }

        private static void Prime(ICommandContext context, IGameObject actor)
        {
            if (actor.IsPlayer || context.CanPlayerSee(actor))
            {
                context.AddMessage($"{actor.Name} primes itself for detonation", ClientMessageType.Generic);
            }

            actor.State = StatePrimed;
        }

        public override bool HandleSpecialCommand(ICommandContext context, IActor actor)
        {
            if (actor.State == StatePrimed)
            {
                Detonate(context, actor);

                return true;
            }

            // Only prime based off of adjacent tiles
            var neighboringCells = actor.VisibleCells.Where(c => IsNeighboringCell(actor.Position, c));

            // Check each adjacent tile to see if it contains actors this actor considers prey in it, then prime.
            foreach (var cellPos in neighboringCells)
            {
                var cell = context.Level.GetCell(cellPos);

                if (cell?.Actor == null || !ArtificialIntelligenceService.IsOpposingTeam(actor.Team, cell.Actor.Team))
                {
                    continue;
                }

                Prime(context, actor);
                return true;
            }

            return base.HandleSpecialCommand(context, actor);
        }

        private static bool IsNeighboringCell(Pos2D origin, Pos2D target)
        {
            return (target.Y == origin.Y || target.X == origin.X) && 
                   target.CalculateDistanceFrom(origin) <= 1;
        }

        private static void Detonate(ICommandContext context, IActor actor)
        {
            if (actor.IsPlayer || context.CanPlayerSee(actor))
            {
                context.AddMessage($"{actor.Name} detonates!", ClientMessageType.Generic);
            }

            context.Level.RemoveObject(actor);
            context.CombatManager.HandleExplosion(context, actor, actor.Position, actor.Strength, 3, DamageType.Combination);
        }
    }
}