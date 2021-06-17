/*
 * Created by Ranorex
 * User: VROC
 * Time: 9:41 PM
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

namespace WL_Automation.Products
{
    /// <summary>
    /// Description of SortProducts.
    /// </summary>
    [TestModule("76425923-CB69-45BE-BF25-E2A7F9BAA395", ModuleType.UserCode, 1)]
    public class SortProducts : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public SortProducts()
        {
            // Do not delete - a parameterless constructor is required!
        }
		
        WL_AutomationRepository repo = WL_AutomationRepository.Instance;
        
        
        [TestVariable("5550d144-79cc-481e-a63f-e8d55b983115")]
        public string sortBy {get; set;}
                
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
            Ranorex.Report.Info("Sort products by " + sortBy);
            Validate.Exists(repo.Saucedemo.Products.Products_span);
            Delay.Seconds(2);
            repo.Saucedemo.Products.ProductSort_select.SelectOption2(sortBy);
            Ranorex.Report.Screenshot(repo.Saucedemo.Products.ProductSort_select);
            Delay.Seconds(2);
            
            string pathPrice="/dom[@domain='www.saucedemo.com']///div[@class='inventory_list']///div[@class='pricebar']/div[@class='inventory_item_price']";
            IList<Ranorex.DivTag> pricesAmt = Host.Local.Find <Ranorex.DivTag>(pathPrice);
            int numProducts=0;
            
            decimal[] priceCantd = new decimal[pricesAmt.Count];

            foreach(Ranorex.DivTag price in pricesAmt){
            	price.MoveTo();
            	//Ranorex.Report.Info(numProducts + " || " + price.InnerText);
            	string p = price.InnerText;
            	p = (p.Split('$')[1]).Trim();
            	priceCantd[numProducts] = Convert.ToDecimal(p);
            	numProducts++;
            	Delay.Seconds(2);
            	
            }//close for
            
            
            bool sorted = verifySort(priceCantd);
            
            if(sorted){
            	Ranorex.Report.Success("The products are sorted properly");
            }else{
            	Ranorex.Report.Failure("The products are not sorted properly");
            }
            
        }//close run
        
        /**
         * Verify if sort is correct
         * @param prices
         * 
         * */
        public bool verifySort(decimal[] prices){
        	bool working = true;
        	
        	for(int i=0; i<prices.Length; i++){
        		
        		for(int j=i+1; j<prices.Length; j++){
        			
        			decimal result = prices[i] - prices[j];
        			
        			if(result > 0){
        				//Ranorex.Report.Info("Data is not sorted");
        				working = false;
        				break;
        			}else{
        				//Ranorex.Report.Info("Data is sorted");
        			}        			
        			
        		}//close for 2
        		
        	}//close for 1
        	
        	return working;
        	
        }//close verifySort
        
    }//close class
    
}//close namespace
