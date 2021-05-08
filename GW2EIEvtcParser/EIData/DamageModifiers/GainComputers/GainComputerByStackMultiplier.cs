﻿namespace GW2EIEvtcParser.EIData
{
    internal class GainComputerByStackMultiplier : GainComputer
    {
        public GainComputerByStackMultiplier()
        {
            Multiplier = true;
        }

        public override double ComputeGain(double gainPerStack, int stack)
        {
            return gainPerStack * stack / (100 + stack * gainPerStack);
        }
    }
}
