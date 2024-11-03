
#ifndef _APP_H_
#define _APP_H_

#include <map>
#include <set>
#include <string>
#include <vector>

#include "Bridges.h"
#include "DataSource.h"
#include "data_src/MovieActorWikidata.h"

using namespace std;
using namespace bridges;

typedef map<string, set<string>> db_type;
typedef map<string, set<string>>::iterator db_iter;

class App {
public:
    App(vector<MovieActorWikidata> &movies_vector);
    void showAllMovies();
    void showActorsInAMovie(const db_iter &movie_it);
    void showAllMoviesForAnActor();
    void getMovieNameAndShowActors();
    void perform(char choice);
    void run();
    char getMenuChoice() const;

private:
    db_type movies_by_name;
};

#endif