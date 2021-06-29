using System;
using System.Collections.Generic;
using System.Linq;
using GW2EIEvtcParser.ParsedData;

namespace GW2EIEvtcParser.EIData
{
    public class FinalHealStats
    {
        public int SquadHealing { get; internal set; }
        public int GroupHealing { get; internal set; }
        public int SelfHealing { get; internal set; }
        public int AllHealingExclMinions { get; internal set; }
        public int AllHealingInclMinions { get; internal set; }

        internal FinalHealStats(ParsedEvtcLog log, long start, long end, AbstractSingleActor actor, AbstractSingleActor target)
        {
            (SquadHealing, GroupHealing, SelfHealing, AllHealingExclMinions, AllHealingInclMinions) = ComputeHealingFrom(log, actor, actor.GetHealEvents(target, log, start, end));
        }

        private static (int, int, int, int, int) ComputeHealingFrom(ParsedEvtcLog log, AbstractSingleActor actor, IReadOnlyList<HealEvent> healEvents)
        {
            int squadHealing = 0;
            int groupHealing = 0;
            int selfHealing = 0;
            int allHealingExclMinions = 0;
            int allHealingInclMinions = 0;

            foreach (HealEvent healEvent in healEvents)
            {
                allHealingInclMinions += healEvent.Healing;
                if (healEvent.To.Master == null)
                {
                    allHealingExclMinions += healEvent.Healing;
                }
                if (healEvent.To.Type == AgentItem.AgentType.Player)
                {
                    squadHealing += healEvent.Healing;
                }
                if (healEvent.To.UniqueID == healEvent.From.UniqueID)
                {
                    selfHealing += healEvent.Healing;
                }
                // TODO: groupHealing - AgentItem doesn't carry subgroup?
            }
            return (squadHealing, groupHealing, selfHealing, allHealingExclMinions, allHealingInclMinions);
        }
    }
}
