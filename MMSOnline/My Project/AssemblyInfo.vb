Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("MMS Online")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("Shipwreck Marine")> 
<Assembly: AssemblyProduct("MMS Online")> 
<Assembly: AssemblyCopyright("")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("8caaca58-31a5-4c7a-b3f6-c0e39674422e")> 

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' <Assembly: AssemblyVersion("1.0.*")> 

'3.0.1.0 - Vs. 3.01
'           - Loads into a new program folder C:\Program Files\MMServices instead of MMSOnline
'           - Has updated the quotes to be numbered starting at 500000
'
'3.0.2.0 - Vs. 3.02
'           - Has a Year dropdown in Commision form. It does not filter the Year to Date since that is a single field for each employee.
'           - Changes the tab and top label on excel output files to match the Year dropdown.
'
'3.0.3.0 - Vs. 3.03
'           - Added a new IAP Pdf and changed the code to populate it. First draft.
'
'3.0.4.0 - Vs. 3.04
'           - Switched the IAP Pdf back and replaced the SAL Pdf.
'           - Fixed the Total for SAL and the Term.
'
'3.0.5.0 - Vs. 3.05
'           - Copied IAP code from original version to set it back.
'
'3.0.6.0 - Vs. 3.06
'           - Fixed the SAL Report numbers
'
'3.0.8.0 - Vs. 3.08
'           - Added New BOS form
'           - Fixed Lien value location and calculations
'           - Fixed spelling error on BOS form
'
'3.0.9.0 - Vs. 3.09
'           - Changed Pro Teck to Protection on the BOS input and printout.
'           - Added Term and ammort values.
'
'3.1.0.0 - Vs. 3.10
'           - Added Winterize Emails
'           - Changed tab order on BOS form
'           - Added WO numbering system (requires auto increment to be taken out)
'           - Change BOS numbering to read from database for easy change next time.
'
'3.1.1.0 - Vs. 3.11
'           - Added Managers access to unlock BOS
'           - Added new Void button for WO and Managers have access
'           - Added Boat Inspection sheet to the 'Print Checkin' button and filled in the known values.
'
'3.1.2.0 - Vs. 3.12
'           - Fixed the setup file to update
'               -sequence is to change this files version number and change the setup project version number.
'
'3.1.3.0 - Vs. 3.13
'           - A test version to make sure it upgrades the program.
'
'3.1.4.0 - Vs. 3.14
'           - Refresh the Work Order data grid when returning from a work order
'           - Fixed BOS finance admin fee.
'           - Added start of Parts Order subsystem.
'
'3.1.5.0 - Vs. 3.15
'           - Added Parts order subsystem.
'           - Changed the Call Log to separate between WO and PO.
'
'3.1.6.0 - Vs. 3.16
'           - Changed the Parts Order form to remove estimate info and calculate GST & Total
'           - Added ability to void parts orders and set complete
'           - Moved parts orders to the Service Page
'           - Added payment information for non WO orders
'
'3.1.7.0 - Vs. 3.17
'           - Modified the Warranty tabs to include pictures.
'           - Added a warranty search form.
'           - Changed warranty approval status values.
'
'3.1.8.0 - Vs. 3.18
'           - Updated permisions for warranty fields.
'           - Changed warranty search results to include customers name.
'
'3.1.9.0 - Vs. 3.19
'           - Added a schedule for warranty.
'           - Fixed Kellowna Atlantis store work order printing error.
'
'3.2.0.0 - Vs. 3.20
'           - Requested changes from the service group.
'
'3.2.1.0 - Vs. 3.21
'           - Changed rights to service and BOS for userlevel 1 to allow full rights.
'
'3.2.2.0 - Vs. 3.22
'           - Fixed Delivery List error.
'           - Fixed Atlatis BC BOS printing problem.
'
'3.2.3.0 - Vs. 3.23
'           - Added a Delivery Schedule for the Sales department.
'
'3.2.4.0 - Vs. 3.24
'           - Added Approval to work order.
'
'3.2.5.0 - Vs. 3.25
'           - Added drop down list of suppliers in the add parts order form.
'           - Added call log to BOS
'           - Added deposit entry on BOS form.
'           - Added Motor serial number to the warranty search
'           - Added PST and Dealer number to BOS in BC.
'
'3.2.6.0 - Vs. 3.26
'           - Changed BOS status from quote to pending when deposit value is entered.
'
'3.2.7.0 - Vs. 3.27
'           - Checked all connections and made sure they were closed and added dispose to them as well.
'
'3.2.8.0 - Vs. 3.28
'           - Added new suppliers to the parts order entery.
'
'3.2.9.0 - Vs. 3.29
'           - Fixed the view for Techs in other offices to be the same as Edmonton.
'           - Fixed the schedule to reload after the office is changed.
'
'3.3.0.0 - Vs. 3.30
'           - Added more bos status' to the delivery list.
'
'3.3.1.0 - Vs. 3.31
'           - Fixed BOS load error on invalid dates in the Boat_Reg_Date field (from the invrestricted table)
'           - Change ASAP to Same Day in the drop down for Priority in WO
'           - Change color of Same Day and Rush lines to stand out.
'           - Auto load the Delivery List without having to click refresh
'
'3.3.2.0 - Vs. 3.32
'           - Changed row color for values of 'ASAP', 'Same Day', and 'Rush' priorities in Priority form and TechForm 
'           - Changed row color for values of 'To Be Processed' in the warrantyClaimsMain form.
'           - Allow changeing Status of Work Orders from Void to Closed.
'           - Filtered out WO with a status of void from the techscrn and priority form lists.
'
'3.3.3.0 - Vs. 3.33
'           - Fixed the tech assignment list to show all techs and not just the one the wo store has.
'
'3.3.4.0 - Vs.3.34
'           - Changed the delivery list query to be only Rig Requested and Rig Approved work orders.
'
'3.3.5.0 - Vs.3.35
'           - Found the math error in the printed balance owing value.
'
'3.3.6.0 - Vs.3.36
'           - Auto assign store if not user is not admin.
'           - Added Shipwreck to the warranty type.
'           - Changed the phone number for Lighthouse.
'           - Replaced HST with PST and GST for BC work orders.
'
'3.3.7.0 - Vs.3.37
'           - Removed ATL and Atlantis Alberta from the system.
'
'3.3.8.0 - Vs.3.38
'           - Release boat only available for Management.
'           - Added Waiting for Pickup to Issue status dropdown.
'           - Added Atlantis AB to the BOS search list to view old records.
'
'3.3.9.0 - Vs.3.39
'           - Added Admin to the release boat ability.
'           - Increased shop supplies from 7 to 8 percent.
'
'3.4.0.0 - Vs.3.40
'           - Fixed calc error on work orders.
'           - Added global variables for shopSuplies, GST, And PST rates.
'           - Added warnings box for customers that show on the work order.
'3.4.1.0 - Vs.3.41
'           - Changed disclaimer on the bottom of work orders.
'3.4.2.0 - Vs.3.42
'           - Added button for Courtesy Clean
'           - Fixed address for Kelowna
'           - Added new fields for Service Quote
'           - Created a printout for Service Quote
'3.4.3.0 - Vs.3.43
'           - Changed BOS to show PST & GST instead of HST
'
'3.4.4.0 - Vs.3.44
'           - Fixed PST GST check box selection.
'
'3.4.5.0 - Vs.3.45
'           - Added requested items to WO and Parts Order.
'
'3.4.6.0 - Vs.3.46
'           - Fixed the stats query to include status 15,16,17
'3.4.7.0 - Vs.3.47
'           - Fixed Kelowna 'Print SAL' report.
'3.4.8.0 - Vs.3.48
'           - added Waiting for Parts to the status list for searching wo's
'           - added Sent back to Factory to stats of issues
'           - added the Timer for the tech time.
'3.4.9.0 - Vs.3.49
'           - added two reports to the 'View Reports' on the main menu for viewing Tech time.
'3.5.0.0 - Vs.3.50
'           - move the 'Waiting for Parts' and 'Send back to Factory' to Aproval Status dropdown
'3.5.1.0 - Vs.3.51
'           - Fixed missing items in aproval status
'           - Added an Easy Reset button for Dank if the MMSData user gets reset
'3.5.2.0 - Vs.3.52
'           - Removed users rights creation when editing or modifying users. MMSData is the only one that requires rights.
'3.5.3.0 - Vs.3.53
'           - Added back ordered to the parts search
'3.5.4.0 - Vs.3.54
'           - Added all missing Approval statuses to the search Warranty form.
'3.5.5.0 - Vs.3.55
'           - Allow Dan and Rob to change the BOS sold date.
'3.5.6.0 - Vs.3.56
'           - Modified Approval status for warranty.
'3.5.7.0 - Vs.3.57
'           - Changed the billable hours to 0 if warranty is selected and set the bill value back to 0.
'3.5.8.0 - Vs.3.58
'           - Replaced \ and / in BOS pdf files created in the MMS directory.
'3.5.9.0 - Vs.3.59
'           - Fixed errors on BOS when the total is blank
'3.6.0.0 - Vs.3.60
'           - Fixed Status search button for pickup
'           - Fixed irregular numbers showing in work order billing
'3.6.1.0 - Vs.3.61
'           - Fixed button selections for Work Order search
'           - Added red color to the Warranty "In Progress" status
'3.6.2.0 - Vs.3.62
'           - Fixed flickering on the Warranty form
'           - Changed all background colors in grids to be Khaki and LightCoral. Easier to look at.
'3.6.3.0 - Vs.3.63
'           - Remove columns from parts order search screen
'           - Added part number column and search by part number to the parts order search
'           - Fixed the call log to filter calls by PartOrderID if they don't have a work order ID (created outside of a work order)
'           - Add an order date to the Parts Order.
'           - Corrected spelling of User Management
'           - Hide the 'Shown sold by' button on the BOS
'           - Added a from/to date filter option for the warranty search
'           - Create an 'Export to Excel' button for the warranty search
'3.6.4.0 - Vs.3.64
'           - Allow everyone to change the Payment Type in an Service Issue.
'           - Added an order status of Quote for Parts orders.
'           - Added warranty type to the warranty search
'           - Added Claim Number to the warranty search
'3.6.5.0 - Vs.3.65
'           - Added All to the list of Suppliers for warranty search.
'3.6.6.0 - Vs.3.66
'           - Added Quote Approved to the payment type of the issue.
'           - Don't show the quote tab if status is complete
'           - Updated the database to have all Quote Requested payments to be changed to Quote Approved if status is complete.
'3.6.7.0 - Vs.3.67
'           - Added a new Standard Service type of V - Drive with three sub categories.
'3.6.8.0 - Vs.3.68
'           - Added new warranty suppliers
'           - Fixed new issue from closing the form when adding
'           - Changed the default charge per hour to be $128 for service.
'           - Waiting for amount for warranty in spreadsheet
'3.6.9.0 - Vs.3.69
'           - Added new fields for the warranty spreadsheet
'           - Changed all issues to have a charge of 128 per hr (even warranties)
'3.7.0.0 - Vs.3.70
'           - Added Storage to the Issue type list
'           - Fixed scheduler to ignore blank titles (empty schedules)
'           - Changed Charge customer to NO for Rig - Sterndrive and Rig - Outboard 
'           - Added Issue Type to the Tech hours report
'3.7.1.0 - Vs.3.71
'           - Added code to show service for the store that the user is working at.
'           - Modified the service checkin contract item number 6 to have pickup within one day.
'           - Added storage to the list of statuses for a work order.
'           - Fixed the quote alignment and added shop supplies to the quote.
'3.7.2.0 - Vs.3.72
'           - Fixed the quote conditions page size.
'3.7.3.0 - Vs.3.73
'           - Fixed reports and added a report for Tech billable
'3.7.5.0 - Vs.3.75
'           - Added security to remove the Void WO option if not an admin
'3.7.6.0 - Vs.3.76
'           - Fixed filter for work orders
'           - Added PaymentType to the Parts Order
'           - Only admin can remove Issues from a work order.
'           - Added a report for the techs to print their own hours
'           - Fixed the report for all techs to combine the shop and billable hours.
'3.7.7.0 - Vs.3.77
'           - Fixed the tech report to clear up billable time being multiplied.
'           - Fixed call logs from getting mixed up between stores on the same wo number.
'           - Moved the Payment Type for the parts from the whole order to the individual part.
'           - Changed the check for user management right to be the userID and not the userLogin
'           - Added checks for the BOS to have requried fields be entered before it can save.
'           - Don't allow customers to be added without an address and email.
'3.7.8.0 - Vs.3.78
'           - Fixed remove invoice button for only admin.
'           - Remove work orders from tech screen when issue is complete
'3.8.0.0 - Vs.3.80
'           - Fixed tech query 
'           - Added new items to Parts Order Status
'3.8.1.0 - Vs.3.81
'           - Allow techs to change the approval status
'           - Fixed addition error on issues that have warranty with billed to client = yes.
'3.8.2.0 - Vs.3.82
'           - Changed checkin conditions to be dynamically created and changed the store on number four.
'3.8.3.0 - Vs3.83
'           - Added notice line to bottom of printed parts order.
'3.8.4.0 - Vs3.84
'           - Do not allow changes to billable hours if Standard Service is selected for a work order issue.
'           - Change the Ski Pack # wording on the BOS to Sports Pkg #
'3.8.5.0 - Vs3.85
'           - Changed the work order printout for a sublet to include both the billable hours and the sublet amount.
'3.8.6.0 - Vs3.86
'           - Added a new Checklist to the PDI and filled in available information.
'3.8.8.0 - Vs3.88
'           - Fixed errors with reading the location table and resulting in lists of manufacturer being empty.
'3.8.9.0 - Vs3.89
'           - Fixed ?ctrlnum error in the inventorydata.vb code.
'3.9.0.0 - Vs3.90
'           - Fixed values for bill of sale not printing.
'3.9.1.0 - Vs3.91
'           - Added two new supliers to the parts order list.
'           - Add HIN (serial) number on InServiceChecklist2013 form
'3.9.2.0 - Vs3.92
'           - Added suppliers to warrenty search.
'           - Checked that the void button on bos is disabled if not admin.
'           - Added two new types of issues for work orders.
'           - Fixed issue with saving pictures back to disk.
'3.9.3.0 - Vs3.93
'           - Added warranty type options
'3.9.4.0 - Vs3.94
'           - Added all of Freddies logins to allow user management.
'3.9.5.0 - VS3.95
'           - Added a remove button for admin to the parts order form.
'3.9.6.0 - Vs3.96
'           - Added phone number to Atlantis address on BOS
'           - Fixed GST for Lighthouse
'   
'3.9.8.0 - Vs3.98
'           - Added internet auto reconnect when the internet connection is lost
'           - removed brands ()
'           
'    
'3.9.9.0 - Vs3.99
'           - Added internet auto reconnect when the internet connection is lost
'           - removed brands ()
'           
' 
'4.0.0.0 - Vs4.00
'           - Added internet auto reconnect when the internet connection is lost
'           - removed brands ()
'           
' 
'4.0.2.0 - Vs4.02
'           - Add work order condition #10
'           
' 4.0.3.0 - Vs4.03
' -add  Scarab and fix delivery list
'
'
'
<Assembly: AssemblyVersion("4.0.3.0")> 
<Assembly: AssemblyFileVersion("4.0.3.0")> 

