/*
 * Created by Ranorex
 * User: VROC
 * Date: 6/12/2021
 * Time: 9:36 AM
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
    /// Description of OpenBrowser.
    /// </summary>
    [TestModule("B05357C9-F71C-4AE5-8ABB-77C272A07284", ModuleType.UserCode, 1)]
    public class OpenBrowser : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public OpenBrowser()
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
            
            //starting -------------------------------------------------------------------------------------------
            Ranorex.Report.Info("Opening web site: " + url + " using " + browserName);
            Host.Local.OpenBrowser(url, browserName, false, false);
            
        }//close run
        
    }//clos class
    
}//close namespace
