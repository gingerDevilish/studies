#include "inclusions.h"

class Street
{
	string StreetName;								// �������� �����.
	vector <House> members;							// ������, ���������� ���� �� ������ �����.
	int length;										// ���������� ����� �� �����.
	int unitax;										// ������ ��������� ������. ���� �� ���������� ����, �������� - 0.
	Street();										// ��������� ����������� �� ���������, �.�. ����� ������� ����� ��������.
public:
	Street(const string& name);						// ������� ������ ����� � �������� ���������.
	Street(const string& name, const Street & a);	// ������� �����, ���������� �� ������� ������������.
	~Street();										// ����������.
	string getname() const;							// ���������� �������� �����.
	int getlength() const;							// ���������� ���������� ����� �� �����.
	int getunitax() const;							// ���������� �������� ������ ��������� ������.
	void addhouse(const House& a);					// ��������� ��� � ����� �����.
	void addtopos(const House& a, int pos);			// ��������� ��� �� �������� �������.
	void delhouse(int pos);							// ������� ���, ������� �� ������������ �������.
	void delhouse(const string& HouseNum);			// ������� ��� � ������������ �������.
	void setname (const string& name);				// ��������������� �����.
	Street split (int pos, const string& newname);	// ��������� ����� �� ��� �� �������� �������, ���������� ����� ����� � �������� ������.
	void setunitax(int newtax);						// ������������� ���������� ����� ��� ���� �����.
	int counttax() const;							// ������������ ����� ����� ������, ������� ����� �������� � ���� �����.
	int debt() const;								// ���������� �����, ������� ������ ����� ���������.
	int gettaken() const;							// ���������� �����, ������� � ������ ������ ��� �������.
	void printInfo() const;							// ������� �� ����� ���������� �� ����� � ���� �� �����.
	Street& operator + (House a) const;				// ��������� ��� � ����� �����.
	Street& operator += (Street* a);				// ������������ ������ ����� � ������.
	bool operator == (Street& a) const;				// ���������� ����� � ������ ��������.
	bool has (const House& a) const;				// ��������� ������� �� ����� ��������� ����, � ������ ������.
	const Street& operator = (const Street& a);		// �������� �������� �� �����, � ����������� ��������.
	House& operator [](int index);					// ��������� ������ � ���� �� ��� �������.
	House& operator [](string num);					// ��������� ������ � ���� �� ��� ������.
};
