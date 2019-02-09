//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// This code was generated by XmlSchemaClassGenerator version 2.0.206.0.
namespace Nota.Data.Generated.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("Daten", Namespace="http://nota-game.azurewebsites.net/schema/nota", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("Daten", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
    public partial class Daten
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<DatenPfadePfadAuswahl> _pfade;
        
        /// <summary>
        /// <para xml:lang="de">Neben Gattung Kultur und Profession, können noch weitere Entscheidungen bei der Charakterschaffung beschritten werden. Welche dies sind wird an dieser Stelle definiert.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("Neben Gattung Kultur und Profession, können noch weitere Entscheidungen bei der C" +
            "harakterschaffung beschritten werden. Welche dies sind wird an dieser Stelle def" +
            "iniert.")]
        [System.Xml.Serialization.XmlArrayAttribute("Pfade", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
        [System.Xml.Serialization.XmlArrayItemAttribute("PfadAuswahl", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
        public System.Collections.ObjectModel.Collection<DatenPfadePfadAuswahl> Pfade
        {
            get
            {
                return this._pfade;
            }
            private set
            {
                this._pfade = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Pfade-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Pfade collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PfadeSpecified
        {
            get
            {
                return (this.Pfade.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="Daten" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="Daten" /> class.</para>
        /// </summary>
        public Daten()
        {
            this._pfade = new System.Collections.ObjectModel.Collection<DatenPfadePfadAuswahl>();
            this._lebewesene = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Lebewesen.LebeweseneLebewesen>();
            this._kulturen = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Kultur.KulturenKultur>();
            this._professionen = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Profession.ProfessionenProfession>();
            this._talente = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Talent.TalenteTalent>();
            this._fertigkeiten = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Fertigkeit.FertigkeitenFertigkeit>();
            this._besonderheiten = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Besonderheit.BesonderheitenBesonderheit>();
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Lebewesen.LebeweseneLebewesen> _lebewesene;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("Lebewesene", Namespace="http://nota-game.azurewebsites.net/schema/lebewesen")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Lebewesen", Namespace="http://nota-game.azurewebsites.net/schema/lebewesen")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Lebewesen.LebeweseneLebewesen> Lebewesene
        {
            get
            {
                return this._lebewesene;
            }
            private set
            {
                this._lebewesene = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Kultur.KulturenKultur> _kulturen;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("Kulturen", Namespace="http://nota-game.azurewebsites.net/schema/kultur")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Kultur", Namespace="http://nota-game.azurewebsites.net/schema/kultur")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Kultur.KulturenKultur> Kulturen
        {
            get
            {
                return this._kulturen;
            }
            private set
            {
                this._kulturen = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Profession.ProfessionenProfession> _professionen;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("Professionen", Namespace="http://nota-game.azurewebsites.net/schema/profession")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Profession", Namespace="http://nota-game.azurewebsites.net/schema/profession")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Profession.ProfessionenProfession> Professionen
        {
            get
            {
                return this._professionen;
            }
            private set
            {
                this._professionen = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Talent.TalenteTalent> _talente;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("Talente", Namespace="http://nota-game.azurewebsites.net/schema/talent")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Talent", Namespace="http://nota-game.azurewebsites.net/schema/talent")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Talent.TalenteTalent> Talente
        {
            get
            {
                return this._talente;
            }
            private set
            {
                this._talente = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Fertigkeit.FertigkeitenFertigkeit> _fertigkeiten;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("Fertigkeiten", Namespace="http://nota-game.azurewebsites.net/schema/fertigkeit")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Fertigkeit", Namespace="http://nota-game.azurewebsites.net/schema/fertigkeit")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Fertigkeit.FertigkeitenFertigkeit> Fertigkeiten
        {
            get
            {
                return this._fertigkeiten;
            }
            private set
            {
                this._fertigkeiten = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Besonderheit.BesonderheitenBesonderheit> _besonderheiten;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("Besonderheiten", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Besonderheit", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Besonderheit.BesonderheitenBesonderheit> Besonderheiten
        {
            get
            {
                return this._besonderheiten;
            }
            private set
            {
                this._besonderheiten = value;
            }
        }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Taktiken", Namespace="http://nota-game.azurewebsites.net/schema/kampf/aktionen")]
        public Nota.Data.Generated.Kampf.Aktionen.Taktiken Taktiken { get; set; }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Ausstattung", Namespace="http://nota-game.azurewebsites.net/schema/kampf/ausstattung")]
        public Nota.Data.Generated.Kampf.Ausstattung.Ausstattung Ausstattung { get; set; }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("Version", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Version { get; set; }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("DatenPfade", Namespace="http://nota-game.azurewebsites.net/schema/nota", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DatenPfade
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<DatenPfadePfadAuswahl> _pfadAuswahl;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("PfadAuswahl", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
        public System.Collections.ObjectModel.Collection<DatenPfadePfadAuswahl> PfadAuswahl
        {
            get
            {
                return this._pfadAuswahl;
            }
            private set
            {
                this._pfadAuswahl = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="DatenPfade" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="DatenPfade" /> class.</para>
        /// </summary>
        public DatenPfade()
        {
            this._pfadAuswahl = new System.Collections.ObjectModel.Collection<DatenPfadePfadAuswahl>();
        }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("DatenPfadePfadAuswahl", Namespace="http://nota-game.azurewebsites.net/schema/nota", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DatenPfadePfadAuswahl : Nota.Data.Generated.Misc.INamedElement
    {
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Beschreibung", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
        public string Beschreibung { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<DatenPfadePfadAuswahlPfad> _pfad;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Pfad", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
        public System.Collections.ObjectModel.Collection<DatenPfadePfadAuswahlPfad> Pfad
        {
            get
            {
                return this._pfad;
            }
            private set
            {
                this._pfad = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="DatenPfadePfadAuswahl" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="DatenPfadePfadAuswahl" /> class.</para>
        /// </summary>
        public DatenPfadePfadAuswahl()
        {
            this._pfad = new System.Collections.ObjectModel.Collection<DatenPfadePfadAuswahlPfad>();
        }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("Name", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Name { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private string _minimumAuswahl = "1";
        
        /// <summary>
        /// <para xml:lang="de">Der Nutzer muss mindestens die angegebene Anzahl an auswahlen treffen. (Default ist 1)</para>
        /// </summary>
        [System.ComponentModel.DefaultValueAttribute("1")]
        [System.ComponentModel.DescriptionAttribute("Der Nutzer muss mindestens die angegebene Anzahl an auswahlen treffen. (Default i" +
            "st 1)")]
        [System.Xml.Serialization.XmlAttributeAttribute("MinimumAuswahl", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MinimumAuswahl
        {
            get
            {
                return this._minimumAuswahl;
            }
            set
            {
                this._minimumAuswahl = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private string _maximumAuswahl = "1";
        
        /// <summary>
        /// <para xml:lang="de">Der Nutzer muss mindestens die angegebene Anzahl an auswahlen treffen. (Default ist 1)</para>
        /// </summary>
        [System.ComponentModel.DefaultValueAttribute("1")]
        [System.ComponentModel.DescriptionAttribute("Der Nutzer muss mindestens die angegebene Anzahl an auswahlen treffen. (Default i" +
            "st 1)")]
        [System.Xml.Serialization.XmlAttributeAttribute("MaximumAuswahl", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MaximumAuswahl
        {
            get
            {
                return this._maximumAuswahl;
            }
            set
            {
                this._maximumAuswahl = value;
            }
        }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("DatenPfadePfadAuswahlPfad", Namespace="http://nota-game.azurewebsites.net/schema/nota", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DatenPfadePfadAuswahlPfad : Nota.Data.Generated.Misc.INamedElement
    {
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Beschreibung", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
        public string Beschreibung { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<DatenPfadePfadAuswahlPfadBedingung> _bedingung;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Bedingung", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
        public System.Collections.ObjectModel.Collection<DatenPfadePfadAuswahlPfadBedingung> Bedingung
        {
            get
            {
                return this._bedingung;
            }
            private set
            {
                this._bedingung = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Bedingung-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Bedingung collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BedingungSpecified
        {
            get
            {
                return (this.Bedingung.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="DatenPfadePfadAuswahlPfad" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="DatenPfadePfadAuswahlPfad" /> class.</para>
        /// </summary>
        public DatenPfadePfadAuswahlPfad()
        {
            this._bedingung = new System.Collections.ObjectModel.Collection<DatenPfadePfadAuswahlPfadBedingung>();
            this._fertigkeit = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType>();
            this._talent = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Talent.Talent>();
        }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Modifikationen", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
        public Nota.Data.Generated.Lebewesen.Mods Modifikationen { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType> _fertigkeit;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Fertigkeit", Namespace="http://nota-game.azurewebsites.net/schema/fertigkeit")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType> Fertigkeit
        {
            get
            {
                return this._fertigkeit;
            }
            private set
            {
                this._fertigkeit = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Fertigkeit-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Fertigkeit collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FertigkeitSpecified
        {
            get
            {
                return (this.Fertigkeit.Count != 0);
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Talent.Talent> _talent;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Talent", Namespace="http://nota-game.azurewebsites.net/schema/talent")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Talent.Talent> Talent
        {
            get
            {
                return this._talent;
            }
            private set
            {
                this._talent = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Talent-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Talent collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TalentSpecified
        {
            get
            {
                return (this.Talent.Count != 0);
            }
        }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("Name", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Name { get; set; }
    }
    
    /// <summary>
    /// <para xml:lang="de">Falls es mindestens eine Bedingung Existiert, muss mindestens eine Bedingung Wahr sein damit die Auswahl gültig ist. Eine Bedingung ist wahr falls alle seine Kunder Wahr sind.</para>
    /// </summary>
    [System.ComponentModel.DescriptionAttribute("Falls es mindestens eine Bedingung Existiert, muss mindestens eine Bedingung Wahr" +
        " sein damit die Auswahl gültig ist. Eine Bedingung ist wahr falls alle seine Kun" +
        "der Wahr sind.")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("DatenPfadePfadAuswahlPfadBedingung", Namespace="http://nota-game.azurewebsites.net/schema/nota", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DatenPfadePfadAuswahlPfadBedingung
    {
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("ProfessionsBedingung", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
        public Nota.Data.Generated.Profession.ProfessionAuswahlen ProfessionsBedingung { get; set; }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("KulturBedingung", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
        public Nota.Data.Generated.Kultur.KulturAuswahlen KulturBedingung { get; set; }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("LebewesenBedingung", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
        public Nota.Data.Generated.Lebewesen.LebewesenAuswahlen LebewesenBedingung { get; set; }
    }
}
