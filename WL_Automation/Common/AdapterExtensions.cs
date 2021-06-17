/*
 * Created by Ranorex
 * User: OOCV85091
 * Date: 6/12/2021
 * Time: 12:49 PM
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
	/// InputTag adds Extension Methods to Ranorex adapter classes
	/// </summary>
	public static class AdapterExtensions
	{
		/// <summary>
		/// Runs the ITestModule instance with TestModuleRunner.Run()
		/// </summary>
		/// <param name="testModule"></param>
		public static void Start(this ITestModule testModule)
		{
			TestModuleRunner.Run(testModule);
		}
		
		/// <summary>
		/// Tests if string has a value (is not null or empty)
		/// </summary>
		/// <returns>true if string is not null or empty</returns>
		public static bool HasValue(this String stringObj)
		{
			return !string.IsNullOrEmpty(stringObj);
		}
		
		/// <summary>
		/// Tests if string is a numeric value
		/// </summary>
		/// <returns>true if string can be parsed as the given number style</returns>
		public static bool IsNumeric(this String stringObj, System.Globalization.NumberStyles numberStyle)
		{
			double throwaway;
			return double.TryParse(stringObj, numberStyle,
			               System.Globalization.CultureInfo.CurrentCulture, out throwaway);
		}
		
		/// <summary>
		/// Tests if string is a numeric value
		/// </summary>
		/// <returns>true if string can be parsed as a double</returns>
		public static bool IsNumeric(this String stringObj)
		{
			double throwaway;
			return double.TryParse(stringObj, out throwaway);
		}
		
		/// <summary>
		/// Tests if string is a currency value
		/// </summary>
		/// <returns>true if string can be parsed as a currency</returns>
		public static bool IsCurrency(this String stringObj)
		{
			return IsNumeric(stringObj, System.Globalization.NumberStyles.Currency);
		}
		
		/// <summary>
		/// Replace the contents of a textbox with the submitted string
		/// </summary>
		/// <param name="text">text to insert into input box</param>
		public static void OverwriteText(this WebElement textbox, String text)
		{
			Report.Info("Overwriting "+textbox+" with text '"+text+"'");
			
			// Clear the textbox
			textbox.PerformClick();
			Delay.Milliseconds(500);
			textbox.ClearText();
			
			// type the provided string
			textbox.PressKeys(text);
			Delay.Milliseconds(500);
		}
		
		/// <summary>
		/// Delete all text currently in textbox
		/// </summary>
		public static void ClearText(this WebElement textbox)
		{
			int lengthBeforeDelete;
        	
        	// first, try to select and delete all text
        	if ( !String.IsNullOrEmpty(textbox.TagValue) ) {
        		textbox.PressKeys("{CONTROL DOWN}{END}{CONTROL UP}"+
        		                  "{SHIFT DOWN}{CONTROL DOWN}{HOME}{SHIFT UP}{CONTROL UP}{DELETE}");
        	}
			
        	// while textbox value isn't null or empty, attempt to remove text
        	while ( !String.IsNullOrEmpty(textbox.TagValue) ) {
        		// prepare for next check
        		lengthBeforeDelete = textbox.TagValue.Length;
        		
        		// Press the backspace and delete key
        		textbox.PressKeys("{Back}{Delete}");
        		
        		// make sure that length is not constant; it should be decreasing.
        		if ( !String.IsNullOrEmpty(textbox.TagValue) &&
        		    lengthBeforeDelete == textbox.TagValue.Length) {
        			// length is not decreasing, error
        			throw new RanorexException("textbox contents are not being properly deleted");
        		}
        		
        	} // end while
		}
		
		/// <summary>
		/// Selects an OptionTag by InnerText within a SelectTag 
		/// (ignores case)
		/// </summary>
		/// <param name="listItem">name of item to select</param>
		public static void SelectOption(this SelectTag selectBox, string itemName)
		{
			Report.Info("Selecting item '"+itemName+"' in "+selectBox);
			
			Regex itemRegex = new Regex( "(?i)^" + Regex.Escape(itemName) + "$" );
			SelectOptionCore(selectBox, itemRegex);
		}
		
		/// <summary>
		/// Selects an OptionTag by InnerText within a SelectTag 
		/// without "$"
		/// </summary>
		/// <param name="listItem">name of item to select</param>
		public static void SelectOption2(this SelectTag selectBox, string itemName)
		{
			Report.Info("Selecting item '"+itemName+"' in "+selectBox);
			
			Regex itemRegex = new Regex( "(?i)^" + Regex.Escape(itemName));
			SelectOptionCore(selectBox, itemRegex);
		}
		
		/// <summary>
		/// Selects an OptionTag by InnerText 
		/// that matches a given regex string
		/// </summary>
		/// <param name="itemRegex">regular expression matching the item to be selected</param>
		public static void SelectOption(this SelectTag selectBox, Regex itemRegex)
		{
			Report.Info("Selecting item '"+itemRegex+"' (regex) in "+selectBox);
			
			SelectOptionCore(selectBox, itemRegex);
		}
		
		private static void SelectOptionCore(this SelectTag selectBox, Regex itemRegex)
		{
			selectBox.EnsureVisible();
			selectBox.Click();
			
			OptionTag option = selectBox.FindSingle<OptionTag>("option[@InnerText~'"+itemRegex+"']");
			option.Selected = true;
			
			// No longer needed, doesn't work for listbox with size > 1
//			ListItem item = "/container[@caption='selectbox']/list/listitem[@text~'" + itemRegex + "']";
//			item.Click();
		}
		
		/// <summary>
		/// Return the selected OptionTag for the this SelectTag
		/// </summary>
		/// <returns></returns>
		public static OptionTag GetSelectedOption(this SelectTag selectBox)
		{
			string tagValue = selectBox.TagValue;
			OptionTag selectedOption = selectBox.FindSingle<OptionTag>("optiontag[@TagValue='"+tagValue+"']");
			
			return selectedOption;
		}
		
		/// <summary>
		/// Return the selected string for this SelectTag
		/// </summary>
		/// <returns></returns>
		public static string GetSelectedOptionText(this SelectTag selectBox)
		{
			OptionTag selectedOption = GetSelectedOption(selectBox);
			
			return selectedOption.InnerText;
		}
		
		/// <summary>
		/// Toggle a checkbox to 'ON'
		/// </summary>
		public static void ToggleOn(this InputTag checkbox)
		{
			Report.Info("Toggling on "+checkbox);
			
			// Only click checkbox if it is unchecked
			if (checkbox.Checked.ToUpper() == "FALSE") {
				checkbox.Click();
			}
		}
		
		/// <summary>
		/// Toggle a checkbox to 'OFF'
		/// </summary>
		public static void ToggleOff(this InputTag checkbox)
		{
			Report.Info("Toggling off "+checkbox);
			
			// Only click checkbox if it is checked
			if (checkbox.Checked.ToUpper() == "TRUE") {
				checkbox.Click();
			}
		}
		
	}//close class
    
}// close namespace
