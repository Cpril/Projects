
#ifndef _MENU_H_
#define _MENU_H_

#include <iostream>
#include <string>
#include <set>
using namespace std;

class Menu {
public:
	static const char QUIT = 'q';

	Menu();
	string asString() const;
	void print(ostream& out) const;
	bool containsChoice(char choice) const;
private:
	set<char> myValidChoices;  // the valid menu choices
	string    myValue;         // the menu that is displayed
};

inline ostream& operator<<(ostream& out, const Menu& m) {
	m.print(out);
	return out;
}

#endif
