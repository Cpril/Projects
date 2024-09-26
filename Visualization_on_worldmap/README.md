# Agricultural Land Percentage Visualization

Welcome to visualization on Worldmap!
This project helps visualize changes over time for various countries in the world. The user is asked to enter a file path that link to their csv file and a year range they would like to visualize. The program then animate a worldmap and color each country with different colors based on the data in the csv file. Python packages including Pandas, Cartopy, Matplotlib are used. 

A sample csv file of Argricultural Land Percentage of World Countries provided in the folder. Please take note of the formatting of the sample csv file. The programs skips the first 2 rows and only uses the "Country Name" columns and selected year range columns. 
Data file downloaded from: https://data.worldbank.org/

Sample input: 
    
    - Please enter the path to your CSV file: C:\Users\prisc\OneDrive - Calvin University\projectme\WorldBank\API_AG.LND.AGRI.ZS_DS2_en_csv_v2_1058228.csv
    - data start year: 1960
    - data end year: 2003
    - Input the columns name of countries: Country Name
    - Please enter a title for your graph: Changes in Argricultural Land Percentage Worldwide
    animation generated. How would you like to view it? 1.display in real time. 2.save as GIF (please enter a number): 2
    - Where would you like to store the file (include the filename, e.g., 'output.gif'): C:\Users\prisc\OneDrive - Calvin University\projectme\WorldBank\output.gif
    please wait. This may take a while.
    - GIF successfully saved.

A sample GIF file created from the input above is included in the folder: output.gif

If you have any questions or feedback, feel free to reach out to me at [jc253@calvin.edu] or [priscillachen1123@gmail.com].
