using System;
using Tibia.Data;

namespace Tibia.Spawns
{
    public static class CreatureSpawnExtensions
    {
        /// <summary>
        ///     Determines whether this instance can see the specified creature spawn.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <returns>
        ///     <c>true</c> if this instance can see the specified creature spawn; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanSee(this ICreatureSpawn self, ICreatureSpawn creatureSpawn)
        {
            return !creatureSpawn.IsInvisible;
        }

        /// <summary>
        ///     Gets the step duration.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>The step duration.</returns>
        public static TimeSpan StepDuration(this ICreatureSpawn self)
        {
            ITile tile = self.Tile;

            int calculatedStepSpeed;
            ushort stepSpeed = self.Speed.WalkSpeed;

            // TODO: This value was originally hard-cored (SpeedB)
            if (stepSpeed <= -261.29)
                calculatedStepSpeed = 1;
            else
            {
                // TODO: These values were originally hard-coded (SpeedA, SpeedB, SpeedC)
                calculatedStepSpeed = (int) Math.Floor(857.36 * Math.Log((double) stepSpeed / 2 + 261.29) + -4795.01 + 0.5);
                if (calculatedStepSpeed <= 0)
                    calculatedStepSpeed = 1;
            }

            // TODO: groundSpeed should not be hard-coded
            ushort groundSpeed = 150;
            if (tile.Ground?.Item?.Speed != null)
                groundSpeed = tile.Ground.Item.Speed.Value;

            // TODO: 1000 should not be hard-coded
            int duration = (int) Math.Floor(1000 * groundSpeed / (double) calculatedStepSpeed);

            // TODO: 50 should not be hard-coded
            int stepDuration = (int) Math.Ceiling(duration / (double) 50) * 50;

            return TimeSpan.FromMilliseconds(stepDuration);
        }
    }
}