﻿#pragma checksum "..\..\..\..\..\View\Pages\RegistrationPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6886AB52F1B33C7E7F652017094FD425348790B7"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using RegistrationAndLogining.View.Pages;
using RegistrationAndLogining.ViewModels.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace RegistrationAndLogining.View.Pages {
    
    
    /// <summary>
    /// RegistrationPage
    /// </summary>
    public partial class RegistrationPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 60 "..\..\..\..\..\View\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelPass2;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\..\View\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelPass1;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\..\View\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailTextBlock;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\..\View\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox P1;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\..\View\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox P2;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\..\..\View\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CodeTextBox;
        
        #line default
        #line hidden
        
        
        #line 174 "..\..\..\..\..\View\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockYourEmail;
        
        #line default
        #line hidden
        
        
        #line 177 "..\..\..\..\..\View\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CheckCodeButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RegistrationAndLogining;component/view/pages/registrationpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\Pages\RegistrationPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LabelPass2 = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.LabelPass1 = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.EmailTextBlock = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.P1 = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 103 "..\..\..\..\..\View\Pages\RegistrationPage.xaml"
            this.P1.LostFocus += new System.Windows.RoutedEventHandler(this.P1_LostFocus_1);
            
            #line default
            #line hidden
            return;
            case 5:
            this.P2 = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 108 "..\..\..\..\..\View\Pages\RegistrationPage.xaml"
            this.P2.PasswordChanged += new System.Windows.RoutedEventHandler(this.P2_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CodeTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.TextBlockYourEmail = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.CheckCodeButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

