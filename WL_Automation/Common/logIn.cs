/*
 * Created by Ranorex
 * User: VROC
 * Date: 6/12/2021
 * Time: 12:44 PM
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
    /// Description of logIn.
    /// </summary>
    [TestModule("ED93E3B1-BDC9-4040-A471-434EA8C53944", ModuleType.UserCode, 1)]
    public class logIn : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public logIn()
        {
            // Do not delete - a parameterless constructor is required!
        }
		
        WL_AutomationRepository repo = WL_AutomationRepository.Instance;
        
        
        [TestVariable("97f17ed6-1188-4568-838c-7a12057b29c0")]
        public string user {get; set;}
        
        [TestVariable("31a799e4-8e79-42fa-8587-ebc68c5bcc95")]
        public string password {get; set;}
        
        
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
            Validate.Exists(repo.Saucedemo.Login.LoginWrapperInner_div);
            repo.Saucedemo.Login.UserName_input.OverwriteText(user);
            repo.Saucedemo.Login.Password_input.OverwriteText(password);
            Ranorex.Report.Screenshot(repo.Saucedemo.Login.LoginWrapperInner_div);
            repo.Saucedemo.Login.Login_button.MoveTo();
			repo.Saucedemo.Login.Login_button.Click();
			
			if(repo.Saucedemo.Login.ErrorMsg_divInfo.Exists()){
				Delay.Seconds(2);
				Ranorex.Report.Info("User Invalid !!");
				Ranorex.Report.Screenshot(repo.Saucedemo.Login.ErrorMsg_div);
				Ranorex.Report.Success("Verifing this is an invalid user !!");
			}else{
				Validate.Exists(repo.Saucedemo.Products.Products_span,"Product screen is displayed !!");
				Ranorex.Report.Success("Success login !!");
				repo.Saucedemo.Products.ProductSort_select.Click();
				//Delay.Seconds(2);
				
			}//close if
            
        }//close run
        
    }//close class
    
}//close namespace
