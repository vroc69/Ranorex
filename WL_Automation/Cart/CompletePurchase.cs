/*
 * Created by Ranorex
 * User: OOCV85091
 * Date: 6/16/2021
 * Time: 8:23 PM
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
    /// Description of CompletePurchase.
    /// </summary>
    [TestModule("D914C4F8-972D-4CEE-BB1F-D14F047F320F", ModuleType.UserCode, 1)]
    public class CompletePurchase : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public CompletePurchase()
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
            Validate.Exists(repo.Saucedemo.PurchaseDone.CheckoutComplete_span,"Purchase done screen is displayed !!");
            Delay.Seconds(2);
            repo.Saucedemo.PurchaseDone.ThanksOrder_title.MoveTo();
            Ranorex.Report.Screenshot(repo.Saucedemo.PurchaseDone.ThanksOrder_title);
            Validate.Exists(repo.Saucedemo.PurchaseDone.BackToProducts_btn, "The purchase is finished !!");
            Delay.Seconds(2);
            repo.Saucedemo.PurchaseDone.BackToProducts_btn.Click();
            
        }//close run
        
    }//close class
    
}//close namespace
