/// \file List.cpp A simple program that introduces a list data structure.
#include <cstdlib>
#include <iostream>
#include <string>
using namespace std;

/// \struct List
/// A piece of our future list.
struct List
{
	int info; ///< A variable that contains information.
	List* pNext; ///< A pointer to the next element of a list.
};

/// \fn bool add (int position, int value, List*& pF)
/// \brief A function that adds an element at the required position
/// \param position A position we want to see the element at.
/// \param value Defines which information the element will contain.
/// \param pF A pointer to the first element of a list.
bool add (int position, int value, List*& pF)
{
	List* pZad=pF, *pNew=new List;
	pNew -> info = value;
	if (position==1)
	{
		pNew -> pNext = pF;
		pF = pNew;
		return true;
	}
	for(int i=1; (i<position)&&(pZad!=0); i++)
	{
		pZad = pZad -> pNext;
	}
	pNew -> pNext = pZad;
	List* pPred=pF;
	while ((pPred -> pNext)!=pZad)
		pPred=pPred -> pNext;
	pPred -> pNext = pNew;
	return true;
}

/// \fn List* del (int position, List*& pF)
/// \brief Deletes the element placed at the mentioned position. Returns a pointer to the deleted element.
/// \param position Defines the position to delete the element from.
/// \param pF A pointer to the first element of a list.
List* del (int position, List*& pF)
{
	if (pF==0)
		return 0;
	List* pZad=pF;
	if (position==1)
	{
		pF=pF -> pNext;
		return pZad;
	}
	for (int i=1; (i<position)&&(pZad!=0); i++)
	{
		pZad = pZad -> pNext;
	}
	if (pZad==0)
		return 0;
	List* pPrev=pF;
	while ((pPrev -> pNext)!=pZad)
		pPrev = pPrev -> pNext;
	pPrev -> pNext = pPrev -> pNext -> pNext;
	return pZad;
};

/// \fn void print (List* pF)
/// \brief Prints out all the elements of the list.
/// \param pF A pointer to the first element of a list.
void print (List* pF)
{
	cout << endl;
	while (pF)
	{
		cout << pF -> info << " ";
		pF = pF -> pNext;
	}
	cout << endl;
}

/// \fn int action (string s)
/// \brief Chooses a function to be executed in the main loop.
/// \param s A string we get from the console.
int action (string s)
{
	if (s=="add")
		return 1;
	else if (s=="del")
		return 2;
	else if (s=="print")
		return 3;
	else if (s=="exit")
		return 4;
	else return 0;
}

/// \fn int main()
/// \brief A plain old simple main().
int main ()
{
	string s;
	int pos, val;
	List* pF=0;
	bool flag= true;
	while(flag)
	{
		cin >> s;
		switch (action(s))
		{
		case 0:
			cout << "\nInput error! Try again!\n";
			continue;
			break;
		case 1:
			cin >> pos >> val;
			add(pos, val, pF);
			break;
		case 2:
			cin >> pos;
			del(pos, pF);
			break;
		case 3:
			print(pF);
			break;
		case 4:
			flag=false;
			break;
		}
		cout<<endl;
	}
	return 0;
}