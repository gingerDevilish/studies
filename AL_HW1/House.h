#include "inclusions.h"

class House
{
private:
	string HouseNumber;	// Номер дома в символьном выражении
	int tax;			// Тариф на одну квартиру
	bool paid;			// Флаг оплачено/неоплачено
	int floors;			// Количество этажей
	int depts;			// Количество подъездов
	int onfloor;		// Количество квартир на одной лестничной площадке
	House();			// Конструктор без параметров запрещен, т.к. невозможно вычислить номер дома.
	string StreetName;	// Название улицы, на которой находится дом. Остается пустым, пока дом не добавлен на улицу.
public:
	House(const string& HouseNum);			// Создается стандартный дом - хрущевка на 5 этажей, 3 подъезда, 3 квартиры на этаже, итого 45 квартир.
											// Тариф принимается равным 100 руб
	House(const string& HouseNum, int tax, int depts, int floors, int onfloor);
	~House();				
	string getNumber() const;		// Возвращает номер дома
	int gettax() const;				// Возвращает значение тарифа
	bool ispaid() const;			// Возвращает состояние оплаты
	int getapts() const;			// Возвращает количество квартир
	int monthsum() const;			// Счиитает, сколько денег собрали в этом месяце за квартплату в этом доме 
	void changetax (int newtax);	// Позволяет менять тариф оплаты
	void payments(bool flag);		// Позволяет менять статус оплаты 
	void printInfo() const;			// Выводит на экран полную информацию о доме
	const House& operator =(const House& a);	// Присваивает одному дома свойства другого. При этом счет дома обнуляется.
	void SetStreet(const string& name);			// Устанавливает улицу
	bool operator == (const House& a) const;	// Сравнивает дома.
};