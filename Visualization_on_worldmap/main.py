import pandas as pd
from cartopy.io import shapereader
from cartopy.crs import PlateCarree
import matplotlib.pyplot as plt
from matplotlib import animation
from matplotlib.animation import PillowWriter
import os




def load_data(file_path,year1,year2):
    """loads a data file from user input"""
    try:
        column_name = input("Input the columns name of countries: ") # try Country Name
        columns_to_keep = [column_name] + [str(year) for year in range(year1, year2)]
        data = pd.read_csv(file_path, header=1, skiprows=range(2), encoding='utf-8', usecols=columns_to_keep)
        return data
    except Exception as e:
        print(f"Error loading data: {e}")
        return None

def load_geo_data():
    """loads world geology data using cartopy"""
    try:
        filename = shapereader.natural_earth(resolution='110m', category='cultural', name='admin_0_countries')
        reader = shapereader.Reader(filename)
        countries = list(reader.records())
        return countries
    except Exception as e:
        print(f"Error loading geography data: {e}")
        return None

def create_animation(data, countries, year1, year2):
    """Create animation from geology data and imported data file"""
    fig, ax = plt.subplots(figsize=(10, 8), subplot_kw={"projection": PlateCarree()})
    title = input("Please enter a title for your graph: ")
    fig.suptitle(title, fontsize=14)
    plt.subplots_adjust(left=0.1, right=0.85, top=0.9, bottom=0.1)
    
    animate = []
    for i in range(year1, year2):
        yearmap = []
        for country in countries:
            country_name = country.attributes['NAME_EN']
            matching_countries = data[data['Country Name'].str.contains(country_name, na=False)]
            if not matching_countries.empty:
                cdata = matching_countries.iloc[0, i - year1 +1] 
                if pd.isnull(cdata):
                    color = 'white'
                elif cdata < 10:
                    color = 'lightyellow'
                elif cdata < 20:
                    color = 'palegreen'
                elif cdata < 30:
                    color = 'greenyellow'
                elif cdata < 40:
                    color = 'chartreuse'
                elif cdata < 50:
                    color = 'limegreen'
                elif cdata < 60:
                    color = 'seagreen'
                elif cdata < 70:
                    color = 'forestgreen'
                elif cdata < 80:
                    color = 'green'
                else:
                    color = 'darkgreen'
                
                geom = ax.add_geometries(country.geometry, PlateCarree(), facecolor=color, edgecolor='k')
                yearmap.append(geom)
        
        year_text = ax.text(0.5, 0.2, str(i), transform=ax.transAxes, fontsize=12, ha='center', va='bottom')
        yearmap.append(year_text)
        animate.append(yearmap)
    
    anim = animation.ArtistAnimation(fig, animate, interval=1, blit=True)
    
    colors = ['white', 'lightyellow', 'palegreen', 'greenyellow', 'chartreuse', 'limegreen', 'seagreen', 'forestgreen', 'green', 'darkgreen']
    labels = ['No Data', '<10%', '10-20%', '20-30%', '30-40%', '40-50%', '50-60%', '60-70%', '70-80%', '>80%']
    handles = [plt.Line2D([0], [0], color=color, lw=4) for color in colors]
    plt.legend(handles, labels, title='Agricultural Land (%)', bbox_to_anchor=(0.99, 0.5), loc='center left')
    # ask for user commend:
    answer = int(input("animation generated. How would you like to view it? 1.display in real time. 2.save as GIF (please enter a number): "))
    if answer == 1:
        plt.show()
    elif answer == 2:
        f = input("Where would you like to store the file (include the filename, e.g., 'output.gif'): ")
        directory = os.path.dirname(f)
        if not os.path.exists(directory):
            print("Directory does not exist. Please check the path and try again.")
            return
        print("please wait. This may take a while.")
        writervideo = PillowWriter(fps=60) 
        anim.save(f, writer=writervideo)
        print("GIF successfully saved.")
    else:
        print("this is not an option.")
        

def main():
    """Runs the program"""
    file_path = input("Please enter the path to your CSV file: ")
    year1 = int(input("data start year: ")) # try 1960
    year2 = int(input("data end year: ")) # try 2023
    if year1 > year2:
        print("Data Range Error. End year must be the same or later than the start year.")
        return
    if not os.path.exists(file_path):
        print("File not found. Please check the path and try again.")
        return
    
    data = load_data(file_path,year1,year2)
    if data is None:
        return
    
    countries = load_geo_data()
    if countries is None:
        return
    
    create_animation(data, countries,year1,year2)

if __name__ == "__main__":
    main()
