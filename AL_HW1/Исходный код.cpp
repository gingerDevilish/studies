#include "inclusions.h"

int main()
{
	srand(time(0));
	House* h1[9];
	for (int i=0; i<9; i++)
	{
		try
		{
			h1[i]=new House(to_string(i+1), rand()%10, rand()%10, rand()%10, rand()%10);
		}
		catch(string& s)
		{
			cout << endl << "Due to wrong " << s << " input, it is set to standard." << endl;
			delete h1[i];
			h1[i]=new House(to_string(i+1));
		}
	}
	House* h2[9];
	for (int i=0; i<9; i++)
	{
		try
		{
			h2[i]=new House(to_string(i+1), rand()%10, rand()%10, rand()%10, rand()%10);
		}
		catch(string& s)
		{
			cout << endl << "Due to wrong " << s << " input, it is set to standard." << endl;
			delete h2[i];
			h2[i]=new House(to_string(i+1));
		}
	}
	House* h3[9];
	for (int i=0; i<9; i++)
	{
		try
		{
			h3[i]=new House(to_string(i+1), rand()%10, rand()%10, rand()%10, rand()%10);
		}
		catch(string& s)
		{
			cout << endl << "Due to wrong " << s << " input, it is set to standard." << endl;
			delete h3[i];
			h3[i]=new House(to_string(i+1));
		}
	}

	Street s1("1"), s2("2"), s3("3");
	for (int i=0; i<9; i++)
	{
		try
		{
		s1.addhouse(*h1[i]);
		s2.addhouse(*h2[i]);
		s3.addhouse(*h3[i]);
		}
		catch(string& s)
		{
			cout << endl << "You have this house on a street already: " << s << endl;
		}
	}
	s1.printInfo();
	s2.printInfo();
	s3.printInfo();

	s1[0].payments(1);
	s1[2].payments(1);
	s1[3].payments(1);
	s1[6].payments(1);
	s1[7].payments(1);
	s2[0].payments(1);
	s2[1].payments(1);
	s2[2].payments(1);
	s2[5].payments(1);
	s2[8].payments(1);
	s3[0].payments(1);
	s3[2].payments(1);
	s3[3].payments(1);
	s3[4].payments(1);
	s3[6].payments(1);
	s3[8].payments(1);
	
	try
	{
		s1.setunitax(14);
	s1.addtopos(*(new House("4a", 6, 6, 6, 6)), 4);
	s1.delhouse("8");
	}
	catch (int)
	{
		cout << endl << "Out of range numbers input" << endl;
	}
	catch (string& s)
	{
		cout << endl << "You have this house on a street already: " << s << endl;
	}
	Street s4("4");
	try
	{
		s4.addhouse(*h1[7]);
	}
	catch (string& s)
	{
		cout << endl << "You have this house on a street already: " << s << endl;
	}
	s2.setname("Vtoraya");
	Street s5("Sedmaya ulitsa");
	s5=s2.split(4, "Sedmaya ulitsa");
	s4+=&s2;

	s1.printInfo();
	s3.printInfo();
	s4.printInfo();
	s5.printInfo();

	system("pause");
	return 0;
}