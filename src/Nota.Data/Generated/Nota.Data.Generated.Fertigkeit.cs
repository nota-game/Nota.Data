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
namespace Nota.Data.Generated.Fertigkeit
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("Fertigkeiten", Namespace="http://nota-game.azurewebsites.net/schema/fertigkeit", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("Fertigkeiten", Namespace="http://nota-game.azurewebsites.net/schema/fertigkeit")]
    public partial class Fertigkeiten
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<FertigkeitenFertigkeit> _fertigkeit;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Fertigkeit", Namespace="http://nota-game.azurewebsites.net/schema/fertigkeit")]
        public System.Collections.ObjectModel.Collection<FertigkeitenFertigkeit> Fertigkeit
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
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="Fertigkeiten" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="Fertigkeiten" /> class.</para>
        /// </summary>
        public Fertigkeiten()
        {
            this._fertigkeit = new System.Collections.ObjectModel.Collection<FertigkeitenFertigkeit>();
        }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("FertigkeitenFertigkeit", Namespace="http://nota-game.azurewebsites.net/schema/fertigkeit", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FertigkeitenFertigkeit
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType> _tags;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("Tags", Namespace="http://nota-game.azurewebsites.net/schema/fertigkeit")]
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
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="FertigkeitenFertigkeit" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="FertigkeitenFertigkeit" /> class.</para>
        /// </summary>
        public FertigkeitenFertigkeit()
        {
            this._tags = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType>();
        }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Voraussetzung", Namespace="http://nota-game.azurewebsites.net/schema/fertigkeit")]
        public Nota.Data.Generated.Misc.BedingungsAuswahl Voraussetzung { get; set; }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Beschreibung", Namespace="http://nota-game.azurewebsites.net/schema/fertigkeit")]
        public string Beschreibung { get; set; }
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("Name", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Name { get; set; }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("FertigkeitenFertigkeitTags", Namespace="http://nota-game.azurewebsites.net/schema/fertigkeit", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FertigkeitenFertigkeitTags
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
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="FertigkeitenFertigkeitTags" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="FertigkeitenFertigkeitTags" /> class.</para>
        /// </summary>
        public FertigkeitenFertigkeitTags()
        {
            this._tag = new System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.NamedType>();
        }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.206.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("Fertigkeit", Namespace="http://nota-game.azurewebsites.net/schema/fertigkeit")]
    public partial class Fertigkeit : Nota.Data.Generated.Misc.NamedType
    {
    }
}
