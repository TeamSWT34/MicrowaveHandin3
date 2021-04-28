﻿using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;

        public PowerTube(IOutput output)
        {
            myOutput = output;
        }

        public void TurnOn(int power)
        {
            /*
            if (power < 1 || 100 < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 100 (incl.)");
            }
            */
            // Fix code below - Boundary error for power:
            if (power < 50 || 700 < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 50 and 700 (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}