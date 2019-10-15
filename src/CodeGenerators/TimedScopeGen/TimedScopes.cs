﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 
#pragma warning disable 1591
namespace Microsoft.Omex.CodeGenerators.TimedScopeGen {
    using global::System.Xml.Serialization;
    
    
    /// <remarks/>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [global::System.SerializableAttribute()]
    [global::System.Diagnostics.DebuggerStepThroughAttribute()]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/TimedScopes.xsd")]
    [global::System.Xml.Serialization.XmlRootAttribute("TimedScopes", Namespace="http://tempuri.org/TimedScopes.xsd", IsNullable=false)]
    public partial class TimedScopeCollection {
        
        private TimedScopeArea[] timedScopeAreaField;
        
        private string namespaceField;
        
        /// <remarks/>
        [global::System.Xml.Serialization.XmlElementAttribute("TimedScopeArea")]
        public TimedScopeArea[] TimedScopeArea {
            get {
                return this.timedScopeAreaField;
            }
            set {
                this.timedScopeAreaField = value;
            }
        }
        
        /// <remarks/>
        [global::System.Xml.Serialization.XmlAttributeAttribute()]
        public string @namespace {
            get {
                return this.namespaceField;
            }
            set {
                this.namespaceField = value;
            }
        }
    }
    
    /// <remarks/>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [global::System.SerializableAttribute()]
    [global::System.Diagnostics.DebuggerStepThroughAttribute()]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/TimedScopes.xsd")]
    public partial class TimedScopeArea {
        
        private TimedScope[] timedScopeField;
        
        private string nameField;
        
        private string friendlyNameField;
        
        /// <remarks/>
        [global::System.Xml.Serialization.XmlElementAttribute("TimedScope")]
        public TimedScope[] TimedScope {
            get {
                return this.timedScopeField;
            }
            set {
                this.timedScopeField = value;
            }
        }
        
        /// <remarks/>
        [global::System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [global::System.Xml.Serialization.XmlAttributeAttribute()]
        public string friendlyName {
            get {
                return this.friendlyNameField;
            }
            set {
                this.friendlyNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [global::System.SerializableAttribute()]
    [global::System.Diagnostics.DebuggerStepThroughAttribute()]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/TimedScopes.xsd")]
    public partial class TimedScope {
        
        private string descriptionField;
        
        private string oCEHandBookLinkField;
        
        private string[] textField;
        
        private string nameField;
        
        private bool onDemandField;
        
        private bool capturesUniqueUserHashesField;
        
        public TimedScope() {
            this.onDemandField = false;
            this.capturesUniqueUserHashesField = false;
        }
        
        /// <remarks/>
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        public string OCEHandBookLink {
            get {
                return this.oCEHandBookLinkField;
            }
            set {
                this.oCEHandBookLinkField = value;
            }
        }
        
        /// <remarks/>
        [global::System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
            }
        }
        
        /// <remarks/>
        [global::System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [global::System.Xml.Serialization.XmlAttributeAttribute()]
        [global::System.ComponentModel.DefaultValueAttribute(false)]
        public bool onDemand {
            get {
                return this.onDemandField;
            }
            set {
                this.onDemandField = value;
            }
        }
        
        /// <remarks/>
        [global::System.Xml.Serialization.XmlAttributeAttribute()]
        [global::System.ComponentModel.DefaultValueAttribute(false)]
        public bool capturesUniqueUserHashes {
            get {
                return this.capturesUniqueUserHashesField;
            }
            set {
                this.capturesUniqueUserHashesField = value;
            }
        }
    }
}
#pragma warning restore 1591