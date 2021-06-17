/*
 * Created by Ranorex
 * User: VROC
 * Time: 9:17 PM
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace WL_Automation.Common
{
    /// <summary>
    /// Description of CloseBrowser.
    /// </summary>
    [TestModule("D8A26917-51C9-4214-AC91-3EA7BFE5F7AE", ModuleType.UserCode, 1)]
    public class CloseBrowser : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public CloseBrowser()
        {
            // Do not delete - a parameterless constructor is required!
        }
		
        WL_AutomationRepository repo = WL_AutomationRepository.Instance;
        
		[TestVariable("1b1384b2-070d-4746-a153-e580f0ed81fe")]
		public string url {get; set;}
		
		[TestVariable("38ccb52c-3714-4a63-b3d5-266e4078b020")]
		public string browserName {get; set;}
		
        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
            //starting here ----------------------------------------------------------------------------------
            Ranorex.Report.Info("Closing: " + browserName + " browser ");
            Host.Local.KillBrowser(browserName);            
            
        }//close run
        
    }//close class
    
}//close namespace
