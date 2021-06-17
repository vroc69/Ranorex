/*
 * Created by Ranorex
 * User: VROC
 * Time: 9:07 PM
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
    /// Description of LogOut.
    /// </summary>
    [TestModule("161C8148-2CDA-4FD6-B5D0-1D0A09805F8E", ModuleType.UserCode, 1)]
    public class LogOut : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public LogOut()
        {
            // Do not delete - a parameterless constructor is required!
        }
		
        WL_AutomationRepository repo = WL_AutomationRepository.Instance;
        
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
            
            //starting ---------------------------------------------------------------------------------------
            repo.Saucedemo.HeaderSection.Menu_btn.Click();
            Delay.Seconds(2);
            repo.Saucedemo.Menu.Logout_menu_link.MoveTo();
            repo.Saucedemo.Menu.Logout_menu_link.Click();
            repo.Saucedemo.Self.WaitForDocumentLoaded();
            Delay.Seconds(2);
            
            Validate.Exists(repo.Saucedemo.Login.LoginWrapperInner_div);
            Ranorex.Report.Screenshot(repo.Saucedemo.Login.LoginWrapperInner_div);
            Ranorex.Report.Success("Success logout !!");
            
        }//close run
        
    }//close class
    
}//close namespace
