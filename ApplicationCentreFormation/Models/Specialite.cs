using System;
using System.Collections.Generic;

namespace ApplicationCentreFormation.Models
{
    public partial class Specialite
    {
        public Specialite()
        {
            FormateurSpecialite = new HashSet<FormateurSpecialite>();
           // SpecialiteFormateur = new HashSet<SpecialiteFormateur>();
        }

        public Guid Id { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<FormateurSpecialite> FormateurSpecialite { get; set; }
       // public virtual ICollection<SpecialiteFormateur> SpecialiteFormateur { get; set; }
    }
}
