﻿#pragma checksum "..\..\FenMenuDesCdr.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9FB631C9FF12563123A92E2AA63038CB336B0FDE8CDAC6CE1B1C63814B35F6B7"
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
    /// FenMenuDesCdr
    /// </summary>
    public partial class FenMenuDesCdr : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\FenMenuDesCdr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAjoutRecette;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\FenMenuDesCdr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSoldeCook;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\FenMenuDesCdr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMesRecettes;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\FenMenuDesCdr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRetour;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfBdd;component/fenmenudescdr.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FenMenuDesCdr.xaml"
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
            this.btnAjoutRecette = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\FenMenuDesCdr.xaml"
            this.btnAjoutRecette.Click += new System.Windows.RoutedEventHandler(this.btnAjoutRecette_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnSoldeCook = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\FenMenuDesCdr.xaml"
            this.btnSoldeCook.Click += new System.Windows.RoutedEventHandler(this.btnSoldeCook_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnMesRecettes = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\FenMenuDesCdr.xaml"
            this.btnMesRecettes.Click += new System.Windows.RoutedEventHandler(this.btnMesRecettes_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnRetour = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\FenMenuDesCdr.xaml"
            this.btnRetour.Click += new System.Windows.RoutedEventHandler(this.btnRetour_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

