#pragma once
#include <cstdlib>
#include <string>
#include <iostream>
#include "Street.h"
using namespace std;

class House
{
protected:
	string HouseNumber;	// ����� ���� � ���������� ���������
	int tax;			// ����� �� ���� ��������
	bool paid;			// ���� ��������/����������
	int apt;			// ���������� ������� � ����
	House();			// ����������� ��� ���������� ��������, �.�. ���������� ��������� ����� ����.
	string StreetName;	// �������� �����, �� ������� ��������� ���. �������� ������, ���� ��� �� �������� �� �����.
public:
	House(string HN);			// ��������� ����������� ��� - �������� �� 5 ������, 3 ��������, 3 �������� �� �����, ����� 45 �������.
								// ����� ����������� ������ 100 ���
	House(string HN, int tax, int apt);
	~House();				
	string getNumber();	// ���������� ����� ����
	int gettax();		// ���������� �������� ������
	bool ispaid();		// ���������� ��������� ������
	int getapts();		// ���������� ���������� �������
	int monthsum();		// ��������, ������� ����� ������� � ���� ������ �� ���������� � ���� ���� 
	void changetax (int newtax);	// ��������� ������ ����� ������
	void payments(bool flag);		// ��������� ������ ������ ������ 
	void info();					// ������� �� ����� ������ ���������� � ����
	House& operator =(House & a);	// ����������� ������ ���� �������� �������. ��� ���� ���� ���� ����������.
	void SetStreet(string name);
	bool operator == (const House& a) const;	// ���������� ����.
};