using System;
using System.Collections.Generic;

namespace ApplicationCentreFormation.Models
{
    public partial class Candidat
    {
        public Candidat()
        {
            SessionCandidat = new HashSet<SessionCandidat>();
        }

        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Cin { get; set; }
        public string Photo { get; set; }
        public string Cv { get; set; }
        public string MotPass { get; set; }

        public virtual ICollection<SessionCandidat> SessionCandidat { get; set; }
    }
}
