﻿#pragma checksum "..\..\IRemittance.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8A5B251EDE5665444A1D53A29E186559F61331797427D3490F0F8C48412E4132"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Homework_13;
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


namespace Homework_13 {
    
    
    /// <summary>
    /// Remittance
    /// </summary>
    public partial class Remittance : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 58 "..\..\IRemittance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TxtBlockName;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\IRemittance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TxtBlockNumber;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\IRemittance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TxtBlockBank_account;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\IRemittance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView List;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\IRemittance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Name;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\IRemittance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Number;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\IRemittance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Txtbox_Sum;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\IRemittance.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Remittance;
        
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
            System.Uri resourceLocater = new System.Uri("/Homework_13;component/iremittance.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\IRemittance.xaml"
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
            this.TxtBlockName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.TxtBlockNumber = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.TxtBlockBank_account = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.List = ((System.Windows.Controls.ListView)(target));
            
            #line 94 "..\..\IRemittance.xaml"
            this.List.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.List_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Name = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 6:
            this.Number = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 7:
            
            #line 126 "..\..\IRemittance.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Txtbox_Sum = ((System.Windows.Controls.TextBox)(target));
            
            #line 133 "..\..\IRemittance.xaml"
            this.Txtbox_Sum.GotFocus += new System.Windows.RoutedEventHandler(this.Txtbox_Sum_GotFocus);
            
            #line default
            #line hidden
            
            #line 133 "..\..\IRemittance.xaml"
            this.Txtbox_Sum.LostFocus += new System.Windows.RoutedEventHandler(this.Txtbox_Sum_LostFocus);
            
            #line default
            #line hidden
            
            #line 134 "..\..\IRemittance.xaml"
            this.Txtbox_Sum.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Txtbox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btn_Remittance = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

