using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lagginDragon.src
{
    public class Implication
    {
        public enum ImplicationConcretionSetValues
        {
            AntecedentAndConsequent = 0,
            AntecedentAndNotConsequent = 1,
            NotAntecedentAndConsequent = 2,
            NotAntecedentAndNotConsequent = 3
        }

        public ImplicationConcretionSetValues DeriveConcreteSetValueFromPremisePair(bool antecedent, bool consequence)
        {
            if (antecedent && consequence)
            {
                return ImplicationConcretionSetValues.AntecedentAndConsequent;
            }
            else if (antecedent && !consequence)
            {
                return ImplicationConcretionSetValues.AntecedentAndNotConsequent;
            }
            else if (!antecedent && consequence)
            {
                return ImplicationConcretionSetValues.NotAntecedentAndConsequent;
            }
            else
            {
                return ImplicationConcretionSetValues.NotAntecedentAndNotConsequent;
            }
        }

        public bool DeriveBooleanFromSetValue(ImplicationConcretionSetValues value)
        {
            if (value == ImplicationConcretionSetValues.AntecedentAndConsequent)
            {
                return true;
            }
            else if (value == ImplicationConcretionSetValues.AntecedentAndNotConsequent)
            {
                return false;
            }
            else if (value == ImplicationConcretionSetValues.NotAntecedentAndConsequent)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
        // for returning different bools based off of context as opposed to implication logic; will elaborate later but i have an idea here; default values are of standard formal implication
        public bool DeriveContextualBooleanValueFromSetValue(ImplicationConcretionSetValues value, bool antecedentAndConsequent = true, bool antecedentAndNotConsequent = false, bool notAntecedentAndConsequent = true, bool notAntecedentAndNotConsequent = true)
        {
            if (value == ImplicationConcretionSetValues.AntecedentAndConsequent)
            {
                return antecedentAndConsequent;
            }
            else if (value == ImplicationConcretionSetValues.AntecedentAndNotConsequent)
            {
                return antecedentAndNotConsequent;
            }
            else if (value == ImplicationConcretionSetValues.NotAntecedentAndConsequent)
            {
                return notAntecedentAndConsequent;
            }
            else
            {
                return notAntecedentAndNotConsequent;
            }
        }
    }
}
