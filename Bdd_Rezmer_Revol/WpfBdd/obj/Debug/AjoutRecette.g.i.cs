﻿#pragma checksum "..\..\AjoutRecette.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "66A1EC76873A5C5E9D49BB59E9F894AC7CAE1F41993820C65315F0234E4D5A13"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfBdd;


namespace WpfBdd {
    
    
    /// <summary>
    /// AjoutRecette
    /// </summary>
    public partial class AjoutRecette : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\AjoutRecette.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Valider;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\AjoutRecette.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBoxNomRecette;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\AjoutRecette.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboType;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\AjoutRecette.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBoxPrix;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\AjoutRecette.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBoxDescriptif;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\AjoutRecette.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Produits;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfBdd;component/ajoutrecette.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AjoutRecette.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btn_Valider = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\AjoutRecette.xaml"
            this.btn_Valider.Click += new System.Windows.RoutedEventHandler(this.btn_Valider_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtBoxNomRecette = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.comboType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.txtBoxPrix = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtBoxDescriptif = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btn_Produits = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\AjoutRecette.xaml"
            this.btn_Produits.Click += new System.Windows.RoutedEventHandler(this.btn_Produits_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

