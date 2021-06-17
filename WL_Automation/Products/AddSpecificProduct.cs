/*
 * Created by Ranorex
 * User: OOCV85091
 * Date: 6/15/2021
 * Time: 9:23 PM
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

namespace WL_Automation.Products
{
    /// <summary>
    /// Description of AddSpecificProduct.
    /// </summary>
    [TestModule("CE1D6C26-F6E8-4F69-BF11-3195F8A57CEB", ModuleType.UserCode, 1)]
    public class AddSpecificProduct : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddSpecificProduct()
        {
            // Do not delete - a parameterless constructor is required!
        }
		
        WL_AutomationRepository repo = WL_AutomationRepository.Instance;
        
        
        [TestVariable("071a5319-7dae-4461-8cf8-7260537ef869")]
        public string findProdBy {get; set;}
                
        [TestVariable("a3cb2206-4a65-42b4-a1dd-c567497b53a2")]
        public string valueProdParam {get; set;}
                
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
            Ranorex.Report.Info("Add product by: " + findProdBy);
            Ranorex.Report.Info("Value to find: " + valueProdParam);
            repo.productName =  valueProdParam;
            repo.Saucedemo.Products.ProductName_div.MoveTo();
            Delay.Seconds(2);
            repo.Saucedemo.Products.AddProduct_btn.Click();
            Delay.Seconds(2);
            repo.Saucedemo.Products.numItemsCart_span.Click();
            Delay.Seconds(2);
            
            Ranorex.Report.Info("Verifing the correct product was added...");
            Validate.Exists(repo.Saucedemo.CartDetails.YourCart_span);
            Validate.Exists(repo.Saucedemo.CartDetails.itemName_div,"The correct product was added !!");
            Ranorex.Report.Screenshot(repo.Saucedemo.CartDetails.CartItem_div);
            Ranorex.Report.Screenshot(repo.Saucedemo.CartDetails.itemName_div);
            
            //remove items
            string pathRem="/dom[@domain='www.saucedemo.com']///button[@class='btn btn_secondary btn_small cart_button']";
            IList<Ranorex.ButtonTag> btnsRem = Host.Local.Find <Ranorex.ButtonTag>(pathRem);
            int numProds=0;
            Ranorex.Report.Info(btnsRem.Count + " products to remove");
            
            foreach(Ranorex.ButtonTag rem in btnsRem){
            	rem.Click();
            	numProds++;           	
            	Delay.Seconds(2);
            	Ranorex.Report.Info("Removing item in the cart...." + numProds);
            	            	            	
            }//close for
            
        }//close run
        
    }//close class
    
}//close namespace
