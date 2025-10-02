using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lagginDragon;
using SimpleInjector;

namespace CrimeModule
{
    public class Types : IModule
    {
        public string Name => "CrimeModule.Types";

        // // // // // / / / / / / /
        public void Deregister(Container container)
        {
            throw new NotImplementedException();
        }

        public void Register(Container container)
        {
            throw new NotImplementedException();
        }
        // // // / / // / / // / / // 

        public enum StatementEvidenceType
        {
            Witness = 0,
            CharacterReference = 1,
            CriminalStatement = 2
        }
        public enum PhysicalEvidenceType
        {
            Weapon = 0,
            Object = 1,
            Location = 2,
            Event = 3,
            FingerPrint = 4,
            Misc = 5
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
            Image = 2,
            FacialRecognition = 3
        }

        public enum Gender
        {
            male = 0,
            female = 1
        }
    }
}
