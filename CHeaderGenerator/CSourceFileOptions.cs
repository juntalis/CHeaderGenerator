﻿using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using Microsoft.VisualStudio.Shell;
using NLog;
using System;
using System.Linq.Expressions;

namespace CHeaderGenerator
{
    public class CSourceFileOptions : DialogPage, INotifyPropertyChanged
    {
        private bool showIncludeGuard = true;
        private string headerComment = @"/*
    Header file generated by C Header Generator.
    Executed by {Name} on {Date}.
*/";
        private bool includeStaticFunctions = false;
        private bool includeExternFunctions = false;
        private string logLayout = "[${longdate}] ${level}: ${logger} - ${message}";
        private LogLevel logLevel = LogLevel.Info;
        private bool autoSaveFiles = true;

        public event PropertyChangedEventHandler PropertyChanged;

        [Description("Generate an include guard to surround the file")]
        [DisplayName("Show Include Guard")]
        [Category("General")]
        public bool ShowIncludeGuard
        {
            get { return this.showIncludeGuard; }
            set
            {
                if (this.showIncludeGuard != value)
                {
                    this.showIncludeGuard = value;
                    this.OnPropertyChanged(() => this.ShowIncludeGuard);
                }
            }
        }

        [Description("The format of the header comment to display at the beginning of the header file")]
        [DisplayName("Header Comment")]
        [Category("General")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string HeaderComment
        {
            get { return this.headerComment; }
            set
            {
                if (this.headerComment != value)
                {
                    this.headerComment = value;
                    this.OnPropertyChanged(() => this.HeaderComment);
                }
            }
        }

        [Description("Include static declarations in header file")]
        [DisplayName("Include Static Declarations")]
        [Category("General")]
        public bool IncludeStaticFunctions
        {
            get { return this.includeStaticFunctions; }
            set
            {
                if (this.includeStaticFunctions != value)
                {
                    this.includeStaticFunctions = value;
                    this.OnPropertyChanged(() => this.IncludeStaticFunctions);
                }
            }
        }

        [Description("Include extern declarations in header file")]
        [DisplayName("Include Extern Declarations")]
        [Category("General")]
        public bool IncludeExternFunctions
        {
            get { return this.includeExternFunctions; }
            set 
            {
                if (this.includeExternFunctions != value)
                {
                    this.includeExternFunctions = value;
                    this.OnPropertyChanged(() => this.IncludeExternFunctions);
                }
            }
        }

        [Description("Layout for log messages to the output window")]
        [DisplayName("Log Message Layout")]
        [Category("Logging")]
        public string LogLayout
        {
            get { return this.logLayout; }
            set
            {
                if (this.logLayout != value)
                {
                    this.logLayout = value;
                    this.OnPropertyChanged(() => this.LogLayout);
                }
            }
        }

        [Description("Minimum level for log messages")]
        [DisplayName("Log Level")]
        [Category("Logging")]
        public LogLevel LogLevel
        {
            get { return this.logLevel; }
            set
            {
                if (this.logLevel != value)
                {
                    this.logLevel = value;
                    this.OnPropertyChanged(() => this.LogLevel);
                }
            }
        }

        [Description("Automatically save files that have been modified when generating header files")]
        [DisplayName("Automatically Save Files")]
        [Category("General")]
        public bool AutoSaveFiles
        {
            get { return this.autoSaveFiles; }
            set
            {
                if (this.autoSaveFiles != value)
                {
                    this.autoSaveFiles = value;
                    this.OnPropertyChanged(() => this.AutoSaveFiles);
                }
            }
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propFunc)
        {
            var body = propFunc.Body as MemberExpression;
            if (body != null)
                this.OnPropertyChanged(body.Member.Name);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
