# webapp_Metric-and-app-handbook
This is an app using C#, ASP.NET and SQL 

This is a COCACOLA property. The app can only use in the COKE network server.

The webapp is a handbook/dictionary that allow user to search certain metrics and application to find out the details. It retrieves
the data from backend SQL database, and use Microsoft Visual Studio, with C# and ASP to design the frontend. It also includes an
Admin Page allow administrator to update the database with frontend frame.


App and Metric Help Center Notes
•	Search Page:

1.	The Metrics filter does not affected by the pillar filter.
2.	Under the pillar filter, there are 5 options. However, in the database, all the application’s pillar are either PSS, customer care or Sales & Commercial, so when the user choose the other 2: ‘Enabling Functions’ or ‘commercial leadership’, there will be no application show in application results.
 
 
3.	Josh mentioned there’s a way to implement the search function by using ‘IN’ in the SQL select statement. Right now, it is only search by the name of the apps or metrics.
4.	For the tables, there’s no maximum height for each row. It will become a problem when the definition or other information is crazy long.
5.	When I resize the window, the ratio and position become weird.
 
6.	In the meeting, someone mentioned that only active apps should appear in the table.
7.	I think there will be a problem when the formula image is too large…
8.	The formula image is by File Name from the database, not the URL
9.	It is probably more convenient for user if a reset button added next to the refresh button.. 









•	Application View:

1.	Supporting Files section, the files are centered, it looks ok when there’s 2 or more files there. But it will look awkward when there is only 1 file.
2.	If the application title is too long…
 
3.	For the tables, there’s no maximum height for each row. It will become a problem when the definition or other information is crazy long.
4.	Excel Export doesn’t work
5.	The question logo link the user to the support center. However, since the pop up window doesn’t have a back button, so the only way to go back is press the Backspace on the keyboard.
6.	A new link should be created so that user can go to the actual app from the app help page.
7.	For the supporting file, if the file is ppt or excel file, the image logo will automatically show, however, anything else, the logo picture is blank file logo.
 
8.	The File goes from the URL, not the file name from the folder.
9.	When I resize the window…
 
10.	I think there will be a problem when the formula image is too large…
11.	The formula image is by File Name from the database, not the URL











•	Metric View:

1.	The Excel export doesn’t work
2.	The question logo above the table and the ‘support center’ hyperlink in the bottom of the table link the user to the support center. However, since the pop up window doesn’t have a back button, so the only way to go back is press the Backspace on the keyboard.
3.	I think there will be a problem when the formula image is too large…
4.	If the metric doesn’t link with another application or the business owner ID does not match any userID in coe.dboUser table, then the metric cannot be viewed in metric table.
5.	The time stamp in the bottom is not update time, it is the created time.
6.	The formula image is by File Name from the database, not the URL













•	Application Admin Page

1.	The left side tree view include all apps and metrics, it probably be the best if it only includes the one that are not in develop, or the one is currently actively using. 
2.	For all the drop down list in the page, since the drop down item value for ‘’select something..” and “undefined” are all ‘-1’, therefore, if in the database, some field are marked as undefined, the front end interface will show “select something…” instead of “undefined.”
3.	All the fields could leave a blank upon submission. For text box, it will become an empty string in the database, if leave a dropdown list unchoose, the field will be “undefined” in the database.
4.	Business Owner ID, Owner User ID and Support Owner ID may have a 6 characters maximum limitation. BTW, enter an ID instead of a name, or even change the filed to a drop down list of names may be easier for administrators. 
5.	For adding a new application, click submit button create a new row in tblApplication table, it automatically gives the new application a new App ID, so the system can user the appid achieve linking metrics, adding files and other task. 
6.	For existing application, click the submit button simply update the data in the textboxes and dropdown lists. 
7.	In Both case, the submit button does not update anything for related metrics and supporting files. 
8.	When adding a new application, it is possible for administrator to click submit button without filling anything (textbox, dropdowns..) in this page.
9.	It is possible best changing the code by using try..except..statement, so the website won’t crush…instead, it could simply show an error meg. 
10.	When adding a new application, or change the name of existing application, the left side tree view does not update in real time. The user must reclick the admin tab, or refresh the entire page (not just the frame), to be able to see the change.
11.	The hyperlink color in the tree view is traditional (blue and purple)…
12.	Remove Files for supporting files, remove the row from the database, but that does not actually delete the uploaded file from “DATA” folder from the solution folder.
•	Metric Admin Page

1.	For all the drop down list in the page, since the drop down item value for ‘’select something..” and “undefined” are all ‘-1’, therefore, if in the database, some field are marked as undefined, the front end interface will show “select something…” instead of “undefined.”
2.	All the fields could leave a blank upon submission. For text box, it will become an empty string in the database, if leave a dropdown list unchoose, the field will be “undefined” in the database.
3.	Business Owner ID and Metric Owner ID may have a 6 characters maximum limitation. BTW, enter an ID instead of a name, or even change the filed to a drop down list of names may be easier for administrators. 
4.	Example section, the HTML text editor could handle any font style, color. However, it has problem when uploaded an image. It may has something to do with web.config.
5.	For adding a new metric, click submit button create a new row in tblHelp_Metric table, it automatically gives the new metric a new metric ID, so the system can user the metricid achieve linking metrics, apps, adding image formula and other tasks. 
6.	For existing metrics, click the submit button simply update the data in the textboxes and dropdown lists. 
7.	In Both case, the submit button does not update anything for related metrics, related applications, image formula. But it update the formula help text.
8.	When adding a new metric, it is possible for administrator to click submit button without filling anything (textbox, dropdowns..) in this page.
9.	It is possible best changing the code by using try..except..statement, so the website won’t crush…instead, it could simply show an error meg. 
10.	When adding a new metric, change the name of existing metric, or remove an existing metric, the left side tree view does not update in real time. The user must reclick the admin tab, or refresh the entire page (not just the frame), to be able to see the changes.
11.	The hyperlink color in the tree view is traditional (blue and purple)…
12.	Remove formula image for formulas, remove the row from the database, but that does not actually delete the uploaded the image from “DATA” folder from the solution folder.
13.	It is potentially a problem if the image formula’s size is too big…
14.	For the algorithm side, I think there is a problem with the available Metric section, because when adding a new metric, available metric will show the name of the new metric, which is not logically right: a metric cannot relate to itself. However, after I relate this new metric to some other metrics, the itselfs name disappeared..
15.	For both application admin page and metric admin page, resize the window will look weird.
16.	For both application admin page and metric admin page, after clicking the submit button, it will go to a confirmation page, where it includes an arrow and a view page link. There is a purple box around the image bc that is a hyperlink..














•	Additional Notes

1.	Is it better to have more info in the app view page..(frequency, pillar…)
2.	When creating either a new application or metric from the admin page, the id are automatically assigned..
3.	Is it better that adding a new column in either app table or metric table, probably called ‘delete_by_admin_page’, so the metric won’t actually delete, just disappeared in the page.
4.	All the formula image are come from this website: http://www.codecogs.com/latex/eqneditor.php
5.	Missing the function of printing or exporting  PDF of everything (like every metric, every application)




