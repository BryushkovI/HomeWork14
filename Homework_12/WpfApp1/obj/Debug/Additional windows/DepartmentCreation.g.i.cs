﻿#pragma checksum "..\..\..\Additional windows\DepartmentCreation.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BE21C88B92EC334C3CC31E46E2A1F422B6264329EB3F646D156BD93D3858C1C8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using WpfApp1;


namespace WpfApp1 {
    
    
    /// <summary>
    /// DepartmentCreation
    /// </summary>
    public partial class DepartmentCreation : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\..\Additional windows\DepartmentCreation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Nameing_box;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Additional windows\DepartmentCreation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Manager_box;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Additional windows\DepartmentCreation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Percent_box;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Additional windows\DepartmentCreation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreateinside;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\Additional windows\DepartmentCreation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreateOutside;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/additional%20windows/departmentcreation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Additional windows\DepartmentCreation.xaml"
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
            
            #line 38 "..\..\..\Additional windows\DepartmentCreation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Nameing_box = ((System.Windows.Controls.TextBox)(target));
            
            #line 51 "..\..\..\Additional windows\DepartmentCreation.xaml"
            this.Nameing_box.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Nameing_box_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Manager_box = ((System.Windows.Controls.TextBox)(target));
            
            #line 54 "..\..\..\Additional windows\DepartmentCreation.xaml"
            this.Manager_box.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Nameing_box_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Percent_box = ((System.Windows.Controls.TextBox)(target));
            
            #line 63 "..\..\..\Additional windows\DepartmentCreation.xaml"
            this.Percent_box.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Nameing_box_TextChanged);
            
            #line default
            #line hidden
            
            #line 66 "..\..\..\Additional windows\DepartmentCreation.xaml"
            this.Percent_box.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Percent_box_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnCreateinside = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\..\Additional windows\DepartmentCreation.xaml"
            this.btnCreateinside.Click += new System.Windows.RoutedEventHandler(this.AddInside);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnCreateOutside = ((System.Windows.Controls.Button)(target));
            
            #line 83 "..\..\..\Additional windows\DepartmentCreation.xaml"
            this.btnCreateOutside.Click += new System.Windows.RoutedEventHandler(this.AddOutside);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

