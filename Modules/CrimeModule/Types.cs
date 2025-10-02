using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeModule.Types
{
    public class Types
    {
        public enum StatementEvidenceType
        {
            Witness = 0,
            CharacterReference = 1
        }
        public enum PhysicalEvidenceType
        {
            Weapon = 0,
            Object = 1,
            Location = 2,
            Event = 3,
            Misc = 4
        }

        public enum BiologicalEvidenceType
        {
            Blood = 0,
            Saliva = 1,
            SemenOrVaginalFluid = 2,
            Urine = 3,
            Faeces = 4,
            Epithelial = 5
        }

        public enum DigitalEvidenceType
        {
            Video = 0,
            Audio = 1,
            Image = 2
        }

        public enum Gender
        {
            male = 0,
            female = 1
        }
    }
}
