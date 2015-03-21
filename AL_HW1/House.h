#pragma once
#include <cstdlib>
#include <string>
#include <iostream>
#include "Street.h"
using namespace std;

class House
{
protected:
	string HouseNumber;	// Номер дома в символьном выражении
	int tax;			// Тариф на одну квартиру
	bool paid;			// Флаг оплачено/неоплачено
	int apt;			// Количество квартир в доме
	House();			// Конструктор без параметров запрещен, т.к. невозможно вычислить номер дома.
	string StreetName;	// Название улицы, на которой находится дом. Остается пустым, пока дом не добавлен на улицу.
public:
	House(string HN);			// Создается стандартный дом - хрущевка на 5 этажей, 3 подъезда, 3 квартиры на этаже, итого 45 квартир.
								// Тариф принимается равным 100 руб
	House(string HN, int tax, int apt);
	~House();				
	string getNumber();	// Возвращает номер дома
	int gettax();		// Возвращает значение тарифа
	bool ispaid();		// Возвращает состояние оплаты
	int getapts();		// Возвращает количество квартир
	int monthsum();		// Счиитает, сколько денег собрали в этом месяце за квартплату в этом доме 
	void changetax (int newtax);	// Позволяет менять тариф оплаты
	void payments(bool flag);		// Позволяет менять статус оплаты 
	void info();					// Выводит на экран полную информацию о доме
	House& operator =(House & a);	// Присваивает одному дома свойства другого. При этом счет дома обнуляется.
	void SetStreet(string name);
	bool operator == (const House& a) const;	// Сравнивает дома.
};