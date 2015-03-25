#include "inclusions.h"

Street::Street (string name): members(), length(0), unitax(0), StreetName(name)
{}

Street::Street (string name, const Street& a): StreetName(name), members(a.members), length(a.length), unitax(a.unitax)
{}

Street::~Street()
{
	members.clear();
}

string Street::getname() const
{
	return StreetName;
}

int Street::getlength() const
{
	return length;
}

int Street::getunitax() const
{
	return unitax;
}

void Street::addhouse (House* a)
{
	a->SetStreet(StreetName);
	if(unitax)
		a->changetax(unitax);
	members.push_back(*a);
	length++;
}

void Street::addtopos (House* a, int pos)
{
	if (pos<0)
	{
		cout<< endl << "Position error!" << endl;
		return;
	}
	a->SetStreet(StreetName);
	if(unitax)
		a->changetax(unitax);
	if (length)
	{
		vector<House>::iterator iter=members.begin();
		for (int i=0; i<pos; i++)
			iter++;
		members.insert(iter, *a);
		length++;
	}
	else
		addhouse(a);
}


void Street::delhouse(int pos)
{
	if (pos<0)
	{
		cout<< endl << "Position error!" << endl;
		return;
	}
	if(length)
	{
		members[pos].SetStreet(0);
		vector<House>::iterator iter=members.begin();
		for (int i=0; i<pos; i++)
			iter++;
		members.erase(iter);
		length--;
	}
	else
	{
		cout << endl << "The street is already empty!" << endl;
	}
}

void Street:: delhouse (const string& num)
{
	if (!length)
	{
		cout << endl << "The street is already empty!" << endl;
		return;
	}
	int i=0;
	while ((members[i].getNumber()!=num)&&(i<length))
		i++;
	if (i==length)
	{
		cout << endl << "No such element" << endl;
		return;
	}
	delhouse(i);
}

void Street::setname (const string& name)
{
	StreetName=name;
}

Street Street::split (int pos, const string& newname)
{
	Street a(newname);
	if (pos<0)
	{
		cout << endl << "Position error" << endl;
		return a;
	}
	for (int i=pos; i<length; i++)
		a.members.push_back(members[i]);
	a.length=length-pos;
	for (int i=0; i<a.length; i++)
		a.members[i].SetStreet(newname);
	for (int i=0; i<a.length; i++)
		members.pop_back();
	length=pos;
	return a;
}

void Street::setunitax(int newtax)
{
	if (unitax<0)
	{
		cout << endl << "Value error" << endl;
		return;
	}
	unitax=newtax;
	for (int i=0; i<length; i++)
		members[i].changetax(newtax);
}

int Street::counttax() const
{
	int sum=0;
	for (int i=0; i<length; i++)
	{
		sum+=members[i].gettax()*members[i].getapts();
	}
	return sum;
}

int Street::gettaken() const
{
	int sum=0;
	for (int i=0; i<length; i++)
		sum+=members[i].monthsum();
	return sum;
}

int Street::debt() const
{
	return counttax()-gettaken();
}

Street& Street::operator += (Street* a)
{
	for(int i=0; i<(a->length); i++)
		addhouse(&(a->members[i]));
	delete a;
	return *this;
}

bool Street::operator == (Street& a) const
{
	return (StreetName==a.StreetName)&&(members==a.members)&&(length==a.length)&&(unitax==a.unitax);
}

const Street& Street::operator = (const Street& a)
{
	members=a.members;
	length=a.length;
	unitax=a.unitax;
	return *this;
}

House& Street::operator [](int index)
{
	return members[index];
}

House& Street::operator [] (string num)
{
	int i=0;
	while ((members[i].getNumber()!=num)&&(i<length))
		i++;
	if (i=length)
	{
		cout<< endl << "No such member" << endl;
		return members[length-1];
	}
	return members[i];
}


void Street::printInfo()
{
	cout << "Street named\n" << StreetName << endl << length << " houses\t";
	unitax?(cout<<"has UNITAX of "<<unitax<<endl):(cout<<"no UNITAX\n");
	for (int i=0; i<length; i++)
		members[i].printInfo();
}