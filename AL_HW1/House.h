#include "inclusions.h"

class House
{
private:
	string HouseNumber;	// ����� ���� � ���������� ���������
	int tax;			// ����� �� ���� ��������
	bool paid;			// ���� ��������/����������
	int floors;			// ���������� ������
	int depts;			// ���������� ���������
	int onfloor;		// ���������� ������� �� ����� ���������� ��������
	House();			// ����������� ��� ���������� ��������, �.�. ���������� ��������� ����� ����.
	string StreetName;	// �������� �����, �� ������� ��������� ���. �������� ������, ���� ��� �� �������� �� �����.
public:
	House(const string& HouseNum);			// ��������� ����������� ��� - �������� �� 5 ������, 3 ��������, 3 �������� �� �����, ����� 45 �������.
											// ����� ����������� ������ 100 ���
	House(const string& HouseNum, int tax, int depts, int floors, int onfloor);
	~House();				
	string getNumber() const;		// ���������� ����� ����
	int gettax() const;				// ���������� �������� ������
	bool ispaid() const;			// ���������� ��������� ������
	int getapts() const;			// ���������� ���������� �������
	int monthsum() const;			// ��������, ������� ����� ������� � ���� ������ �� ���������� � ���� ���� 
	void changetax (int newtax);	// ��������� ������ ����� ������
	void payments(bool flag);		// ��������� ������ ������ ������ 
	void printInfo() const;			// ������� �� ����� ������ ���������� � ����
	const House& operator =(const House& a);	// ����������� ������ ���� �������� �������. ��� ���� ���� ���� ����������.
	void SetStreet(const string& name);			// ������������� �����
	bool operator == (const House& a) const;	// ���������� ����.
};