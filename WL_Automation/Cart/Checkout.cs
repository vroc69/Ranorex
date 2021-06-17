/*
 * Created by Ranorex
 * User: OOCV85091
 * Date: 6/16/2021
 * Time: 7:55 PM
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

using WL_Automation.Common;

namespace WL_Automation.Cart
{
    /// <summary>
    /// Description of Checkout.
    /// </summary>
    [TestModule("0E4B8F0C-1C3D-424E-AC6A-7A802C3E21B4", ModuleType.UserCode, 1)]
    public class Checkout : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Checkout()
        {
            // Do not delete - a parameterless constructor is required!
        }
		
        WL_AutomationRepository repo = WL_AutomationRepository.Instance;
        
		[TestVariable("f6e5315d-2666-404a-ab02-307dea59a088")]
		public string fisrtName {get; set;}
		      
		[TestVariable("d02a8d90-62af-4012-89ea-68766cc18630")]
		public string lastName {get; set;}
		      
		[TestVariable("a15c24d0-cef4-4e6e-b7ed-4955a5aeec14")]
		public string postalCode {get; set;}
		        
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
            
            //starting here ---------------------------------------------------------------------------------------------------------------------
            Validate.Exists(repo.Saucedemo.Checkout.CheckoutYourInformation_span,"Checkout screen is displayed !!");
            Delay.Seconds(2);
            repo.Saucedemo.Checkout.FirstName_input.OverwriteText(fisrtName);
            repo.Saucedemo.Checkout.LastName_input.OverwriteText(lastName);
            repo.Saucedemo.Checkout.PostalCode_input.OverwriteText(postalCode);
            Delay.Seconds(2);
            Ranorex.Report.Screenshot(repo.Saucedemo.Checkout.CheckoutInformation_div);
            
            repo.Saucedemo.Checkout.Continue_btn.Click();
            Delay.Seconds(2);
            
        }//close run
        
    }//close class
    
}//close namespace
