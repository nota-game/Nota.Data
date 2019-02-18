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
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("StandardDaten", Namespace="http://nota-game.azurewebsites.net/schema/nota")]
        public DatenStandardDaten StandardDaten { get; set; }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Organismen", Namespace="http://nota-game.azurewebsites.net/schema/lebewesen")]
        public Nota.Data.Generated.Lebewesen.Organismen Organismen { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Pfad.PfadGruppenPfade> _pfadGruppen;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("PfadGruppen", Namespace="http://nota-game.azurewebsites.net/schema/pfad")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Pfade", Namespace="http://nota-game.azurewebsites.net/schema/pfad")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Pfad.PfadGruppenPfade> PfadGruppen
        {
            get
            {
                return this._pfadGruppen;
            }
            private set
            {
                this._pfadGruppen = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="Daten" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="Daten" /> class.</para>
        /// </summary>
        public Daten()
        {
            this._pfadGruppen = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Pfad.PfadGruppenPfade>();
            this._talente = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Talent.TalenteTalent>();
            this._fertigkeiten = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Fertigkeit.FertigkeitenFertigkeit>();
            this._besonderheiten = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Besonderheit.BesonderheitenBesonderheit>();
            this._tags = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.Tag>();
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
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.Tag> _tags;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("Tags", Namespace="http://nota-game.azurewebsites.net/schema/misc")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Tag", Namespace="http://nota-game.azurewebsites.net/schema/misc")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.Tag> Tags
        {
            get
            {
                return this._tags;
            }
            private set
            {
                this._tags = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="en">Minimum inclusive value: 1.</para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("Version", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int Version { get; set; }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("DatenStandardDaten", Namespace="http://nota-game.azurewebsites.net/schema/nota", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DatenStandardDaten
    {
        
        /// <summary>
        /// <para xml:lang="en">Minimum inclusive value: 1.</para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("GenerierungsPunkte", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int GenerierungsPunkte { get; set; }
    }
}
