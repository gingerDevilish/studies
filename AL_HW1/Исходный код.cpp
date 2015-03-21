#pragma once
#include "House.h"
#include "Street.h"

int main()
{
	House* h1[9];
	h1[0]=new House("1", 6, 4);
	h1[1]=new House("2", 7, 5);
	h1[2]=new House("3", 6, 6);
	h1[3]=new House("4", 8, 7);
	h1[4]=new House("5", 7, 4);
	h1[5]=new House("6", 6, 9);
	h1[6]=new House("7", 7, 10);
	h1[7]=new House("8", 3, 1);
	h1[8]=new House("9", 4, 12);
	House* h2[9];
	h2[0]=new House("1", 4, 4);
	h2[1]=new House("2", 3, 7);
	h2[2]=new House("3", 4, 8);
	h2[3]=new House("4", 5, 9);
	h2[4]=new House("5", 3, 4);
	h2[5]=new House("6", 3, 3);
	h2[6]=new House("7", 6, 2);
	h2[7]=new House("8", 10, 1);
	h2[8]=new House("9", 4, 6);
	House* h3[9];
	h3[0]=new House("1", 5, 2);
	h3[1]=new House("2", 4, 3);
	h3[2]=new House("3", 2, 2);
	h3[3]=new House("4", 3, 2);
	h3[4]=new House("5", 4, 2);
	h3[5]=new House("6", 5, 2);
	h3[6]=new House("7", 6, 4);
	h3[7]=new House("8", 7, 2);
	h3[8]=new House("9", 3, 6);

	Street s1("1"), s2("2"), s3("3");
	for (int i=0; i<9; i++)
	{
		s1.addhouse(h1[i]);
		s2.addhouse(h2[i]);
		s3.addhouse(h3[i]);
	}
	s1.info();
	s2.info();
	s3.info();

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
	
	s1.setunitax(14);
	s1.addtopos(new House("4a", 6, 6), 4);
	s1.delhouse("8");
	Street s4("4");
	s4.addhouse(h1[7]);
	s2.setname("Vtoraya");
	Street s5("Sedmaya ulitsa");
	s5=s2.split(4, "Sedmaya ulitsa");
	s4+=&s2;

	s1.info();
	s3.info();
	s4.info();
	s5.info();

	system("pause");
	return 0;
}