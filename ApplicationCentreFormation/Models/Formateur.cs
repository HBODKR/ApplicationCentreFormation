using System;
using System.Collections.Generic;

namespace ApplicationCentreFormation.Models
{
    public partial class Formateur
    {
        public Formateur()
        {
            FormateurSpecialite = new HashSet<FormateurSpecialite>();
            SessionFormateur = new HashSet<SessionFormateur>();
           // SpecialiteFormateur = new HashSet<SpecialiteFormateur>();
        }

        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Cin { get; set; }
        public string TarifHoraire { get; set; }
        public string Photo { get; set; }
        public string Cv { get; set; }
        public string MotPass { get; set; }

        public virtual ICollection<FormateurSpecialite> FormateurSpecialite { get; set; }
        public virtual ICollection<SessionFormateur> SessionFormateur { get; set; }
        //public virtual ICollection<SpecialiteFormateur> SpecialiteFormateur { get; set; }
    }
}
