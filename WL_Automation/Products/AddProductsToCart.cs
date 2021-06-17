/*
 * Created by Ranorex
 * User: VROC
 * Time: 8:00 PM
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
    /// Description of AddProductsToCart.
    /// </summary>
    [TestModule("7E7FDF3C-6C17-4A17-8FF3-0CAB7B03F6C0", ModuleType.UserCode, 1)]
    public class AddProductsToCart : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddProductsToCart()
        {
            // Do not delete - a parameterless constructor is required!
        }
		
        WL_AutomationRepository repo = WL_AutomationRepository.Instance;
        
        
        [TestVariable("18b522a3-eae0-496a-b336-f64ac11978d8")]
        public int numProducts {get; set;}
        
        [TestVariable("f8df4e8d-42bd-4ff2-abea-bf6ba9003702")]
        public bool removeProds {get; set;}
        
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
            
            //start here -------------------------------------------------------------------------------------
            Ranorex.Report.Info(numProducts + " products to add to the cart !!");
            addProduct(numProducts);
            Delay.Seconds(2);
            repo.Saucedemo.Products.numItemsCart_span.Click();
            Delay.Seconds(2);
            bool correctAdded = verifyCart(numProducts);
            
            if(correctAdded){
            	Ranorex.Report.Screenshot(repo.Saucedemo.CartDetails.FullCart_div);
            	Ranorex.Report.Success("The products in the cart are correct !!");
            }else{
            	Ranorex.Report.Screenshot(repo.Saucedemo.CartDetails.FullCart_div);
            	Ranorex.Report.Failure("The products in the cart are not correct !!");
            }
            
            if(removeProds){
            	Ranorex.Report.Info("It is necessary to remove the producs");
            	
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
            	
            }else{
            	Ranorex.Report.Info("It is not necessary to remove the producs");
            	Ranorex.Report.Info("Continuing with the process....");
            	Delay.Seconds(2);
            	repo.Saucedemo.CartDetails.Checkout_btn.Click();
            }
            
        }//close run
        
        /**
         * To add products to Cart
         * @param num products
         * 
         * */
        public void addProduct(int numP)
        {
        	string pathBtnAdd="/dom[@domain='www.saucedemo.com']///button[@class='btn btn_primary btn_small btn_inventory']";
            IList<Ranorex.ButtonTag> btnAdds = Host.Local.Find <Ranorex.ButtonTag>(pathBtnAdd);
            int numProducts=1;
            Ranorex.Report.Info(btnAdds.Count + " buttons found");
            
            foreach(Ranorex.ButtonTag btn in btnAdds){
            	
            	if(numProducts <= numP){
            		btn.Click();
            		Ranorex.Report.Info("Adding product to cart...." + numProducts);
            		numProducts++;           	
            		Delay.Seconds(2);
            	}
            	            	
            }//close for
            
            //Ranorex.Report.Info("numProducts = " + numProducts);
            //Ranorex.Report.Info("numP = " + numP);
            
            if((numProducts-1) == numP){
            	Ranorex.Report.Success("All products added !!");
            	Ranorex.Report.Screenshot(repo.Saucedemo.Products.numItemsCart_span);
            	
            }else{
            	Ranorex.Report.Failure("Not all products added !!");
            	Ranorex.Report.Screenshot(repo.Saucedemo.HeaderSection.ShoppingCart_a);
            }
            
        }//close add product
        
        /*
         * Verify the products in the cart
         * 
         */
        public bool verifyCart(int numP){
        	string pathItem="/dom[@domain='www.saucedemo.com']///div[#'cart_contents_container']///div[@class='cart_item']";
            IList<Ranorex.DivTag> items = Host.Local.Find <Ranorex.DivTag>(pathItem);
            int numProducts=0;
            Ranorex.Report.Info(items.Count + " products found");
            
            foreach(Ranorex.DivTag item in items){
            	item.MoveTo();
            	numProducts++;           	
            	Delay.Seconds(2);
            	Ranorex.Report.Info("Item in the cart...." + numProducts);
            	            	            	
            }//close for
			
            if(numP == numProducts){
            	return true;
            }else{
            	return false;
            }
            
        }//close verigyCart
        
    }//close class
    
}//close namespace
