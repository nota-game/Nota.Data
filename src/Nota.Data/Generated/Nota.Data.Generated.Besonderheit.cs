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
namespace Nota.Data.Generated.Besonderheit
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("BedingugsAuswahl", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("Not", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
    public partial class BedingugsAuswahl
    {
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Or", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public BedingugsAuswahlen Or { get; set; }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("And", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public BedingugsAuswahlen And { get; set; }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Not", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public BedingugsAuswahl Not { get; set; }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Besonderheit", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public Nota.Data.Generated.Misc.NamedType Besonderheit { get; set; }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("BedingugsAuswahlen", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("Or", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(And))]
    public partial class BedingugsAuswahlen
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<BedingugsAuswahlen> _or;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Or", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public System.Collections.ObjectModel.Collection<BedingugsAuswahlen> Or
        {
            get
            {
                return this._or;
            }
            private set
            {
                this._or = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Or-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Or collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OrSpecified
        {
            get
            {
                return (this.Or.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="BedingugsAuswahlen" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="BedingugsAuswahlen" /> class.</para>
        /// </summary>
        public BedingugsAuswahlen()
        {
            this._or = new System.Collections.ObjectModel.Collection<BedingugsAuswahlen>();
            this._and = new System.Collections.ObjectModel.Collection<BedingugsAuswahlen>();
            this._not = new System.Collections.ObjectModel.Collection<BedingugsAuswahl>();
            this._besonderheit = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType>();
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<BedingugsAuswahlen> _and;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("And", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public System.Collections.ObjectModel.Collection<BedingugsAuswahlen> And
        {
            get
            {
                return this._and;
            }
            private set
            {
                this._and = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die And-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the And collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AndSpecified
        {
            get
            {
                return (this.And.Count != 0);
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<BedingugsAuswahl> _not;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Not", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public System.Collections.ObjectModel.Collection<BedingugsAuswahl> Not
        {
            get
            {
                return this._not;
            }
            private set
            {
                this._not = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Not-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Not collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NotSpecified
        {
            get
            {
                return (this.Not.Count != 0);
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType> _besonderheit;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Besonderheit", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType> Besonderheit
        {
            get
            {
                return this._besonderheit;
            }
            private set
            {
                this._besonderheit = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Besonderheit-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Besonderheit collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BesonderheitSpecified
        {
            get
            {
                return (this.Besonderheit.Count != 0);
            }
        }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("Besonderheit", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
    public partial class Besonderheit : Nota.Data.Generated.Misc.NamedType
    {
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("And", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
    public partial class And : BedingugsAuswahlen
    {
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("Besonderheiten", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("Besonderheiten", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
    public partial class Besonderheiten
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<BesonderheitenBesonderheit> _besonderheit;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Besonderheit", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public System.Collections.ObjectModel.Collection<BesonderheitenBesonderheit> Besonderheit
        {
            get
            {
                return this._besonderheit;
            }
            private set
            {
                this._besonderheit = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Besonderheit-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Besonderheit collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BesonderheitSpecified
        {
            get
            {
                return (this.Besonderheit.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="Besonderheiten" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="Besonderheiten" /> class.</para>
        /// </summary>
        public Besonderheiten()
        {
            this._besonderheit = new System.Collections.ObjectModel.Collection<BesonderheitenBesonderheit>();
        }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("BesonderheitenBesonderheit", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class BesonderheitenBesonderheit : Nota.Data.Generated.Misc.INamedElement, Nota.Data.Generated.Misc.IKostenElement
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType> _tags;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("Tags", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Tag", Namespace="http://nota-game.azurewebsites.net/schema/misc")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType> Tags
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
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Tags-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Tags collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TagsSpecified
        {
            get
            {
                return (this.Tags.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="BesonderheitenBesonderheit" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="BesonderheitenBesonderheit" /> class.</para>
        /// </summary>
        public BesonderheitenBesonderheit()
        {
            this._tags = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType>();
            this._name = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.LokalisierungenLokalisirung>();
            this._beschreibung = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.LokalisierungenLokalisirung>();
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.LokalisierungenLokalisirung> _name;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("Name", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Lokalisirung", Namespace="http://nota-game.azurewebsites.net/schema/misc")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.LokalisierungenLokalisirung> Name
        {
            get
            {
                return this._name;
            }
            private set
            {
                this._name = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.LokalisierungenLokalisirung> _beschreibung;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("Beschreibung", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Lokalisirung", Namespace="http://nota-game.azurewebsites.net/schema/misc")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.LokalisierungenLokalisirung> Beschreibung
        {
            get
            {
                return this._beschreibung;
            }
            private set
            {
                this._beschreibung = value;
            }
        }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Ersetzt", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public BesonderheitenBesonderheitErsetzt Ersetzt { get; set; }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Bedingung", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public Nota.Data.Generated.Besonderheit.BedingugsAuswahl Bedingung { get; set; }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("Id", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private int _kosten = 0;
        
        /// <summary>
        /// </summary>
        [System.ComponentModel.DefaultValueAttribute(0)]
        [System.Xml.Serialization.XmlAttributeAttribute("Kosten", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int Kosten
        {
            get
            {
                return this._kosten;
            }
            set
            {
                this._kosten = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private bool _gebunden = false;
        
        /// <summary>
        /// <para xml:lang="de">Dieses Element beschreibt Besonderheiten, welche nicht frei zur verfügung stehen, sondern nur in Kombination mit einem anderen Element wie Einer Art erhalten werden kann.</para>
        /// </summary>
        [System.ComponentModel.DefaultValueAttribute(false)]
        [System.ComponentModel.DescriptionAttribute("Dieses Element beschreibt Besonderheiten, welche nicht frei zur verfügung stehen," +
            " sondern nur in Kombination mit einem anderen Element wie Einer Art erhalten wer" +
            "den kann.")]
        [System.Xml.Serialization.XmlAttributeAttribute("Gebunden", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool Gebunden
        {
            get
            {
                return this._gebunden;
            }
            set
            {
                this._gebunden = value;
            }
        }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("BesonderheitenBesonderheitTags", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class BesonderheitenBesonderheitTags
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType> _tag;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Tag", Namespace="http://nota-game.azurewebsites.net/schema/misc")]
        public System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType> Tag
        {
            get
            {
                return this._tag;
            }
            private set
            {
                this._tag = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="BesonderheitenBesonderheitTags" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="BesonderheitenBesonderheitTags" /> class.</para>
        /// </summary>
        public BesonderheitenBesonderheitTags()
        {
            this._tag = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType>();
        }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("BesonderheitenBesonderheitErsetzt", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class BesonderheitenBesonderheitErsetzt
    {
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Besonderheit", Namespace="http://nota-game.azurewebsites.net/schema/besonderheit")]
        public Nota.Data.Generated.Misc.NamedType Besonderheit { get; set; }
    }
}
