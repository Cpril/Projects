#include "App.h"

#include <iostream>

#include "Bridges.h"
#include "DataSource.h"
#include "Menu.h"
#include "data_src/MovieActorWikidata.h"

using namespace std;
using namespace bridges;

App::App(vector<MovieActorWikidata> &movies_vector) {
    cout << "Got " << movies_vector.size() << " movies...\n";

    // iterate through the vector and gather the actors into each movie record.
    // TODO: for loop here.
    for (int i = 0; i < movies_vector.size(); i++) {
        string movieName = movies_vector[i].getMovieName();
        string actorName = movies_vector[i].getActorName();

        movies_by_name[movieName].insert(movies_by_name[movieName].end(),
                                         actorName);
    }
}

void App::showAllMovies() {
    // TODO:
    //   loop through movies_by_name.
    for (db_iter it = movies_by_name.begin(); it != movies_by_name.end();
         it++) {
        // get the movie name and print it
        string movieName = it->first;
        cout << "Movie: " << movieName << endl;
        // call showActorsInaMovie()
        showActorsInAMovie(it);
    }
}

void App::showActorsInAMovie(const db_iter &movie_it) {
    // TODO:
    set<string> ActorNameSet =
        movie_it->second;  // get the set with actor names
    // loop through actor names and print them
    for (set<string>::iterator it = ActorNameSet.begin();
         it != ActorNameSet.end(); ++it) {
        cout << "      " << *it << endl;
    }
}

void App::getMovieNameAndShowActors() {
    string movie_name;
    cout << "Enter a movie name to see the list of actors: ";
    cin.ignore();
    getline(cin, movie_name);
    // use find() on movies_by_name to search for movie_name.
    // if found, print out the actors.
    if (movies_by_name.find(movie_name) != movies_by_name.end()) {
        set<string> actors = movies_by_name[movie_name];
        for (set<string>::iterator it = actors.begin(); it != actors.end();
             it++) {
            cout << *it << endl;
        }
    } else {
        cout << "Movie is not found" << endl;
    };
}

void App::showAllMoviesForAnActor() {
    string actor_name;
    cout << "Enter an actor name to show all movies for that actor: ";
    cin.ignore();
    getline(cin, actor_name);
    // create a variable, movies_by_actor, that is a set of strings.
    set<string> movies_by_actor;
    // loop through movies_by_name.
    for (db_iter it = movies_by_name.begin(); it != movies_by_name.end();
         it++) {
        set<string> setofactors = it->second;
        if (setofactors.find(actor_name) != setofactors.end()) {
            movies_by_actor.insert(movies_by_actor.end(), it->first);
        }
    }
    // loop through movies_by_actor and print out each item.
    for (set<string>::iterator it = movies_by_actor.begin();
         it != movies_by_actor.end(); ++it) {
        cout << *it << endl;
    }
}

void App::run() {
    while (true) {
        char choice = getMenuChoice();
        if (choice == Menu::QUIT) {
            break;
        }
        perform(choice);
    }
}

void App::perform(char choice) {
    switch (choice) {
        case 'a':
            showAllMovies();
            break;
        case 'b':
            getMovieNameAndShowActors();
            break;
        case 'c':
            showAllMoviesForAnActor();
            break;
        default:
            cerr << "\n*** choice " << choice << " is not supported\n" << endl;
    }
}

char App::getMenuChoice() const {
    Menu menu;
    char choice;
    while (true) {
        cout << '\n' << menu << flush;
        cin >> choice;
        if (menu.containsChoice(choice)) {
            break;
        }
        cout << "\n*** " << choice << " is not supported!\n" << endl;
    }
    return choice;
}