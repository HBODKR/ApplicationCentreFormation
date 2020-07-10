using System;
using System.Collections.Generic;

namespace ApplicationCentreFormation.Models
{
    public partial class Formation
    {
        public Formation()
        {
            Session = new HashSet<Session>();
        }

        public Guid Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string ChargeHoraire { get; set; }
        public string Programme { get; set; }
        public Guid NiveauId { get; set; }

        public virtual Niveau Niveau { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}
