#pragma once
#include <cstdlib>
#include <string>
#include <iostream>
#include "House.h"
using namespace std;

House::House(string HN): tax(100), paid(0), apt(45), StreetName(0), HouseNumber(HN)
{}

House::House(string HN, int tax, int apt): paid(0), StreetName(0), HouseNumber(HN)
{
	if (tax>0)
		this -> tax=tax;
	else
	{
		this -> tax=0;
		cout << endl << "Due to wrong tax input tax value is set to 0" << endl;
	}
	if (apt>0)
		this -> apt=apt;
	else
	{
		this -> apt=0;
		cout << endl << "Due to wrong apartment number input, it is set to 0" << endl;
	}
}

House::~House()
{}

string House::getNumber()
{
	return HouseNumber;
}

int House::gettax()
{
	return tax;
}

bool House::ispaid()
{
	return paid;
}

int House::getapts()
{
	return apt;
}

int House::monthsum()
{
	return tax*paid*apt;
}

void House::changetax (int newtax)
{
	if (newtax>0)
		tax=newtax;
	else
		cout << endl << "Failed to change tax." << endl;
}

void House::payments(bool flag)
{
	switch (flag)
	{
	case 0:
	case 1:
		paid=flag; break;
	default:
		cout << endl << "Invalid payment status." << endl;
	}
}

void House::info()
{
	cout << HouseNumber << "\t" << apt << " apts\t" << tax << " per month\t" << (paid?"tax paid":"tax unpaid") << endl;
}

House& House::operator=(House& a)
{
	HouseNumber=a.HouseNumber;
	tax=a.tax;
	apt=a.apt;
	paid=0;
	return *this;
}

bool House::operator == (const House& a) const
{
	return ( (HouseNumber==a.HouseNumber) && (tax==a.tax) && (apt==a.apt) && (StreetName==a.StreetName) );
}

void House:: SetStreet (string name)
{
	StreetName=name;
}