#include <map>
#include <set>
#include <string>
#include <vector>

#include "Bridges.h"
#include "DataSource.h"
#include "data_src/MovieActorWikidata.h"

#include "App.h"

using namespace std;
using namespace bridges;


int main(int argc, char** argv) {
    if (argc != 3) {
        cerr << "Usage: ./movies <BridgesUserName> <BridgesId>\n";
        exit(-1);
    }
    // create Bridges object
    Bridges bridges(73, argv[1], argv[2]);
    DataSource ds(&bridges);

    int start_year, end_year;
    cout << "Enter a starting year between 1955 and 2022: ";
    cin >> start_year;
    cout << "Enter an ending year: ";
    cin >> end_year;

    if (end_year < start_year) {
        cout << "End year must be same as or after start year. Good-bye.\n";
        exit(-1);
    }

    vector<MovieActorWikidata> movies_vector = ds.getWikidataActorMovie(start_year, end_year);

    App app(movies_vector);

    app.run();

    return 0;
}
