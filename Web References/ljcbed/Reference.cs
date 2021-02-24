﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace LoadTruck.ljcbed {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="Load_Mgt_Binding", Namespace="urn:microsoft-dynamics-schemas/codeunit/Load_Mgt")]
    public partial class Load_Mgt : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback CreateLoadOperationCompleted;
        
        private System.Threading.SendOrPostCallback VerifyTrailerOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Load_Mgt() {
            this.Url = global::LoadTruck.Properties.Settings.Default.LoadTruck_ljcbed_Load_Mgt;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event CreateLoadCompletedEventHandler CreateLoadCompleted;
        
        /// <remarks/>
        public event VerifyTrailerCompletedEventHandler VerifyTrailerCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/Load_Mgt:CreateLoad", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/Load_Mgt", ResponseElementName="CreateLoad_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/Load_Mgt", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return_value")]
        public string CreateLoad(string routeCode, string trailer, [System.Xml.Serialization.XmlElementAttribute(DataType="date")] System.DateTime shipDate) {
            object[] results = this.Invoke("CreateLoad", new object[] {
                        routeCode,
                        trailer,
                        shipDate});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CreateLoadAsync(string routeCode, string trailer, System.DateTime shipDate) {
            this.CreateLoadAsync(routeCode, trailer, shipDate, null);
        }
        
        /// <remarks/>
        public void CreateLoadAsync(string routeCode, string trailer, System.DateTime shipDate, object userState) {
            if ((this.CreateLoadOperationCompleted == null)) {
                this.CreateLoadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateLoadOperationCompleted);
            }
            this.InvokeAsync("CreateLoad", new object[] {
                        routeCode,
                        trailer,
                        shipDate}, this.CreateLoadOperationCompleted, userState);
        }
        
        private void OnCreateLoadOperationCompleted(object arg) {
            if ((this.CreateLoadCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateLoadCompleted(this, new CreateLoadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/Load_Mgt:VerifyTrailer", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/Load_Mgt", ResponseElementName="VerifyTrailer_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/Load_Mgt", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return_value")]
        public string VerifyTrailer(string trailerID) {
            object[] results = this.Invoke("VerifyTrailer", new object[] {
                        trailerID});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void VerifyTrailerAsync(string trailerID) {
            this.VerifyTrailerAsync(trailerID, null);
        }
        
        /// <remarks/>
        public void VerifyTrailerAsync(string trailerID, object userState) {
            if ((this.VerifyTrailerOperationCompleted == null)) {
                this.VerifyTrailerOperationCompleted = new System.Threading.SendOrPostCallback(this.OnVerifyTrailerOperationCompleted);
            }
            this.InvokeAsync("VerifyTrailer", new object[] {
                        trailerID}, this.VerifyTrailerOperationCompleted, userState);
        }
        
        private void OnVerifyTrailerOperationCompleted(object arg) {
            if ((this.VerifyTrailerCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.VerifyTrailerCompleted(this, new VerifyTrailerCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void CreateLoadCompletedEventHandler(object sender, CreateLoadCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateLoadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateLoadCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void VerifyTrailerCompletedEventHandler(object sender, VerifyTrailerCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class VerifyTrailerCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal VerifyTrailerCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591