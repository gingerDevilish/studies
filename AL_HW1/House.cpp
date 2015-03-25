#include "inclusions.h"

House::House(const string& HouseNum): tax(100), paid(0), StreetName(0), HouseNumber(HouseNum), depts(3), floors(5), onfloor(3)
{}

House::House(const string& HouseNum, int tax, int depts, int floors, int onfloor): paid(0), StreetName(0), HouseNumber(HouseNum)
{
	if (tax>0)
		this->tax = tax;
	else
	{
		this->tax = 0;
		cout << endl << "Due to wrong tax input tax value is set to 0" << endl;
	}
	if (depts>0)
		this->depts = depts;
	else
	{
		this->depts = 0;
		cout << endl << "Due to wrong porch number input, it is set to 0" << endl;
	}
	if (floors>0)
		this->floors = floors;
	else
	{
		this->floors = 0;
		cout << endl << "Due to wrong floors number input, it is set to 0" << endl;
	}
	if (onfloor>0)
		this->onfloor = onfloor;
	else
	{
		this->onfloor = 0;
		cout << endl << "Due to wrong same floor apartment number input, it is set to 0" << endl;
	}
}

House::~House()
{}

string House::getNumber() const
{
	return HouseNumber;
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
	if (newtax>0)
		tax=newtax;
	else
		cout << endl << "Failed to change tax." << endl;
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
	return ( (HouseNumber==a.HouseNumber) && (tax==a.tax) && (depts==a.depts) && (floors==a.floors) && (onfloor==a.onfloor) && (StreetName==a.StreetName) );
}

void House:: SetStreet (const string& name)
{
	StreetName=name;
}