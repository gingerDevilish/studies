/// \file queue.cpp A simple program that introduces a queue data structure. Realisation uses cyclic arrays.
#include <cstdio>
#include <iostream>
#include <string>
using namespace std;
/// \def index(n)
/// \brief A macro that halps to join together [0] and [1023] cells.
#define index(n) ((n)%1024+1024)%1024

/// \var int a[1024]
/// \brief The array used for the queue.
int a[1024]={0};
/// \var int head
/// \brief The index of the first element in a queue.
int head=0;
/// \var int tail
/// \brief The index of the element that comes after the last one in the queue.
int tail=1;
/// \var int quantity
/// \brief The current number of elements in the queue.
int quantity=0;

/// \fn void push(int val)
/// \brief Adds an element in the head of the queue.
/// \param val The information contained in the element.
void push(int val)
{
	a[head]=val;
	head=index(head-1);
	quantity++;
}

/// \fn int pop()
/// \brief Deletes an element in the tail of the queue.
int pop()
{
	if (!quantity)
		return ~0;
	int k=a[index(tail-1)];
	tail=index(tail-1);
	quantity--;
	return k;
}

/// \fn void print()
/// \brief Prints out all the queue from the tail (oldest) to the head (newest) element. In the end all the queue is cleared.
void print()
{
	cout << endl;
	int k;
	while (quantity)
	{
		k=pop();
		cout << k << " ";
	}
	cout << endl;
}

/// \fn int action (string s)
/// \brief Chooses a function to be executed in the main loop.
/// \param s A string we get from the console.
int action(string s)
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
	string s;
	int val;
	bool flag=true;
	while(flag)
	{
		cin >> s;
		switch (action(s))
		{
		case 0:
			cout << endl << "Input error! Try again" << endl;
			continue;
			break;
		case 1:
			cin >> val;
			push(val);
			break;
		case 2:
			pop();
			break;
		case 3:
			print();
			break;
		case 4:
			flag=false;
			break;
		}
	}
	return 0;
}