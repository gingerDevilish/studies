#include "inclusions.h"

House::House(const string& HouseNum): tax(100), paid(0), StreetName(0), HouseNumber(HouseNum), depts(3), floors(5), onfloor(3)
{}

House::House(const string& HouseNum, int tax, int depts, int floors, int onfloor): paid(0), StreetName(0), HouseNumber(HouseNum)
{
	if (tax<0) throw "tax";
	if (depts<0) throw "porch number";
	if (floors<0) throw "floors number";
	if (onfloor<0) throw "same floor apartment number";
	this->tax = tax;
	this->depts = depts;
	this->floors = floors;
	this->onfloor = onfloor;
}

House::~House()
{}

string House::getNumber() const
{
	return HouseNumber;
}

string getStreet() const
{
	return StreetName;
}

int House::gettax() const
{
	return tax;
}

bool House::ispaid() const
{
	return paid;
}

int House::getapts() const
{
	return depts*floors*onfloor;
}

int House::monthsum() const
{
	return tax*paid*getapts();
}

void House::changetax (int newtax)
{
	if (newtax<0) throw 1;
	tax=newtax;
}

void House::payments(bool flag)
{
	paid=flag;
}

void House::printInfo() const
{
	cout << HouseNumber << "\t" << getapts() << " apts\t" << tax << " per month\t" << (paid?"tax paid":"tax unpaid") << endl;
}

const House& House::operator=(const House& a)
{
	HouseNumber=a.HouseNumber;
	tax=a.tax;
	depts=a.depts;
	floors=a.floors;
	onfloor=a.onfloor;
	paid=0;
	return *this;
}

bool House::operator == (const House& a) const
{
	return ( (HouseNumber==a.HouseNumber) && (tax==a.tax) && (depts==a.depts) && (floors==a.floors) && (onfloor==a.onfloor) );
}

void House:: SetStreet (const string& name)
{
	StreetName=name;
}