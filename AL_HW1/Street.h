#pragma once
#include <cstdlib>
#include <string>
#include <iostream>
#include <vector>
#include "House.h"
using namespace std;

class Street
{
	string StreetName;							// �������� �����.
	vector <House, allocator <House>> members;	// ������, ���������� ���� �� ������ �����.
	int length;									// ���������� ����� �� �����.
	int unitax;									// ������ ��������� ������. ���� �� ���������� ����, �������� - 0.
	Street();									// ��������� ����������� �� ���������, �.�. ����� ������� ����� ��������.
public:
	Street(string name);						// ������� ������ ����� � �������� ���������.
	Street(string name, const Street & a);		// ������� �����, ���������� �� ������� ������������.
	~Street();									// ����������.
	string getname();							// ���������� �������� �����.
	int getlength();							// ���������� ���������� ����� �� �����.
	int getunitax();							// ���������� �������� ������ ��������� ������.
	void addhouse(House* a);					// ��������� ��� � ����� �����.
	void addtopos(House* a, int pos);			// ��������� ��� �� �������� �������.
	void delhouse(int pos);						// ������� ���, ������� �� ������������ �������.
	void delhouse(string HN);					// ������� ��� � ������������ �������.
	void setname (string name);					// ��������������� �����.
	Street split (int pos, string newname);		// ��������� ����� �� ��� �� �������� �������, ���������� ����� ����� � �������� ������.
	void setunitax(int newtax);					// ������������� ���������� ����� ��� ���� �����.
	int counttax();								// ������������ ����� ����� ������, ������� ����� �������� � ���� �����.
	int debt();									// ���������� �����, ������� ������ ����� ���������.
	int gettaken();								// ���������� �����, ������� � ������ ������ ��� �������.
	void info();								// ������� �� ����� ���������� �� ����� � ���� �� �����.
	Street & operator + (House a) const;		// ��������� ��� � ����� �����.
	Street & operator += (Street* a);			// ������������ ������ ����� � ������.
	bool operator == (Street& a) const;			// ���������� ����� � ������ ��������.
	const Street & operator = (const Street & a);	// �������� �������� �� �����, � ����������� ��������.
	House & operator [](int index);					// ��������� ������ � ���� �� ��� �������.
	House & operator [](string num);				// ��������� ������ � ���� �� ��� ������.
};
