/*
 * Created by Ranorex
 * User: OOCV85091
 * Date: 6/16/2021
 * Time: 8:10 PM
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

namespace WL_Automation.Cart
{
    /// <summary>
    /// Description of Overview.
    /// </summary>
    [TestModule("9F709BDD-423E-4B79-9DB6-91CFAF902AE2", ModuleType.UserCode, 1)]
    public class Overview : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Overview()
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
            
            //starting here --------------------------------------------------------------------------------------------------------------------
            Validate.Exists(repo.Saucedemo.Overview.CheckoutOverview_span,"Checkout:overview screen is displayed !!");
            Ranorex.Report.Screenshot(repo.Saucedemo.CartDetails.FullCart_div);
            repo.Saucedemo.Overview.shippingInfo_div.MoveTo();
            repo.Saucedemo.Overview.totalAmt_div.MoveTo();
            repo.Saucedemo.Overview.TaxDollar240_div.MoveTo();
            repo.Saucedemo.Overview.totalTotalAmt_div.MoveTo();
            Delay.Seconds(2);
            
            repo.Saucedemo.Overview.Finish_btn.Click();
            Delay.Seconds(2);
			            
        }//close run
        
    }//close class
    
}//close namespace
