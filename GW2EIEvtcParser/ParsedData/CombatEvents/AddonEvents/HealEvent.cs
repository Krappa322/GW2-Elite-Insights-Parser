using System;

namespace GW2EIEvtcParser.ParsedData
{
    public class HealEvent : AbstractDamageEvent
    {
        public bool ConditionBased { get; protected set; }
        public int Healing { get; protected set; }

        internal HealEvent(bool conditionBased, int healing, CombatItem evtcItem, AgentData agentData, SkillData skillData) : base(evtcItem, agentData, skillData)
        {
            ConditionBased = conditionBased;
            Healing = healing;
        }

        public override bool ConditionDamageBased(ParsedEvtcLog log)
        {
            return ConditionBased;
        }
    }
}
