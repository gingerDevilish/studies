/// \file stack.cpp A simple program that introduces a stack data structure. Realisation uses lists.
#include <cstdlib>
#include <iostream>
#include <string>
using namespace std;

/// \struct Stack
/// A piece of our future stack.
struct Stack
{
	int info; ///< A variable that contains information.
	Stack* pNext; ///< A pointer to the next element of a list.
};

/// \fn void push(Stack*& pF, int val)
/// \brief Adds an element to the top of te stack.
/// \param pF A pointer to the top of the stack.
/// \param val The information this element will contain.
void push(Stack*& pF, int val)
{
	Stack* pNew=new Stack;
	pNew -> info = val;
	pNew -> pNext = pF;
	pF=pNew;
}

/// \fn Stack* pop (Stack*& pF)
/// \brief Deletes the element placed on the top of the stack. Returns a pointer to the popped element.
/// \param pF A pointer to the top of the stack.
Stack* pop (Stack*& pF)
{
	Stack* buf=pF;
	pF = pF -> pNext;
	return buf;
}

/// \fn void print (Stack*& pF)
/// \brief Prints out all the stack. In the end, the stack is empty.
/// \param pF A pointer to the top of the stack.
void print (Stack*& pF)
{
	Stack* pCur=pF;
	cout << endl;
	while (pCur -> pNext)
	{
		pCur=pop(pF);
		cout << pCur -> info << " ";
	}
	cout << endl;
}

/// \fn int action (string s)
/// \brief Chooses a function to be executed in the main loop.
/// \param s A string we get from the console.
int action (string s)
{
	if (s=="push")
		return 1;
	else if (s=="pop")
		return 2;
	else if (s=="print")
		return 3;
	else if (s=="exit")
		return 4;
	else return 0;
}

/// \fn int main()
/// \brief A plain old simple main().
int main()
{
	Stack* pF=0;
	int val;
	string s;
	bool flag=true;
	while (flag)
	{
		cin >> s;
		switch(action(s))
		{
		case 0:
			cout << endl << "Input error! Try again." << endl;
			continue;
			break;
		case 1:
			cin >> val;
			push(pF, val);
			break;
		case 2:
			pop(pF);
			break;
		case 3:
			print(pF);
			break;
		case 4:
			flag=false;
			break;
		}
	}
	return 0;
}