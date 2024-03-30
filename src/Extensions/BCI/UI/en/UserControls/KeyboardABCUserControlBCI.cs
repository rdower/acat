﻿////////////////////////////////////////////////////////////////////////////
//
// Copyright 2013-2019; 2023 Intel Corporation
// SPDX-License-Identifier: Apache-2.0
//
//
// KeyboardABCUserControlBCI.cs
//
// User control for the keyboard that is alphabetically arranged. 
//
////////////////////////////////////////////////////////////////////////////

using ACAT.Lib.Core.AnimationManagement;
using ACAT.Lib.Core.PanelManagement;
using ACAT.Lib.Core.UserControlManagement;
using ACAT.Lib.Core.Utility;
using ACAT.Lib.Core.WidgetManagement;
using System;
using System.Windows.Forms;

namespace ACAT.Extensions.BCI.UI.UserControls
{
    [DescriptorAttribute("79BF6DA3-0505-4356-BF68-FAE63A051C12",
        "KeyboardControl",
        "User Control keyboard BCI")]

    public partial class KeyboardABCUserControlBCI : UserControl, IUserControl
    {
        private static String _formConfigFilePath = "";
        private static UserControlConfigMapEntry _mapEntry;
        private UserControlKeyboardCommon _keyboardCommon;
        IScannerPanel _scanner;

        public KeyboardABCUserControlBCI()
        {
            InitializeComponent();
        }

        public event AnimationPlayerStateChanged EvtPlayerStateChanged;
        /// <summary>
        /// Gets the descriptor for this class
        /// </summary>
        public IDescriptor Descriptor
        {
            get { return DescriptorAttribute.GetDescriptor(GetType()); }
        }

        /// <summary>
        /// Gets the snchronization object
        /// </summary>
        public SyncLock SyncObj
        {
            get { return _keyboardCommon.SyncObj; }
        }

        public IUserControlCommon UserControlCommon
        {
            get
            {
                return _keyboardCommon;
            }
        }

        public static string getpathConfigFile()
        {
            try
            {
                if (_mapEntry != null)
                    _formConfigFilePath = _mapEntry.ConfigFileName;

            }
            catch (Exception)
            {

            }
            return _formConfigFilePath;
        }

        public bool Initialize(UserControlConfigMapEntry mapEntry, TextController textController, IScannerPanel scanner)
        {
            _mapEntry = mapEntry;

            _keyboardCommon = new UserControlKeyboardCommon(this, mapEntry, textController, scanner);


            _scanner = scanner;

            bool retVal = _keyboardCommon.Initialize();

           

            _keyboardCommon.AnimationManager.EvtPlayerStateChanged += AnimationManager_EvtPlayerStateChanged;
            //_keyboardCommon.RootWidget.Finder.FindAllChildren(typeof(WinControlWidget), _listButtonsWidgets);//TO BE UPDATED WHEN KEYBOARD CHANGE
            return retVal;
        }

        public void OnLoad()
        {
            _keyboardCommon.OnLoad();


            _keyboardCommon.AnimationManager.OnLoad(_keyboardCommon.RootWidget);
        }

        public void OnPause()
        {

        }

        public void OnResume()
        {

        }
        public void OnWidgetActuated(WidgetActuatedEventArgs e, ref bool handled)
        {
            //_wordPredictionCommon.OnWidgetActuated(e, ref handled);

        }

        private void AnimationManager_EvtPlayerStateChanged(object sender, PlayerStateChangedEventArgs e)
        {
            if (EvtPlayerStateChanged != null)
            {
                EvtPlayerStateChanged(this, e);
            }
        }
    }
}
