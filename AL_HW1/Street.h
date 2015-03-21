#pragma once
#include <cstdlib>
#include <string>
#include <iostream>
#include <vector>
#include "House.h"
using namespace std;

class Street
{
	string StreetName;							// Название улицы.
	vector <House, allocator <House>> members;	// Вектор, содержащий дома на данной улице.
	int length;									// Количество домов на улице.
	int unitax;									// Единая налоговая ставка. Если не установена явно, значение - 0.
	Street();									// Запрещаем конструктор по умолчанию, т.к. улица обязана иметь название.
public:
	Street(string name);						// Создает пустую улицу с заданным названием.
	Street(string name, const Street & a);		// Создает улицу, устроенную по образцу существующей.
	~Street();									// Деструктор.
	string getname();							// Возвращает название улицы.
	int getlength();							// Возвращает количество домов на улице.
	int getunitax();							// Возвращает значение единой налоговой ставки.
	void addhouse(House* a);					// Добавляет дом в конец улицы.
	void addtopos(House* a, int pos);			// Добавляет дом на заданную позицию.
	void delhouse(int pos);						// Удаляет дом, стоящей на определенной позиции.
	void delhouse(string HN);					// Удаляет дом с определенным номером.
	void setname (string name);					// Переименовывает улицу.
	Street split (int pos, string newname);		// Разделяет улицу на две по заданной границе, возвращает новую улицу с заданным именем.
	void setunitax(int newtax);					// Устанавливает одинаковый налог для всех домов.
	int counttax();								// Рассчитывает общую сумму сборов, которые можно получить с этой улицы.
	int debt();									// Возвращает сумму, которую жители улицы задолжали.
	int gettaken();								// Возвращает сумму, которую в данном месяце уже собрали.
	void info();								// Выводит на экран информацию об улице и всех ее домах.
	Street & operator + (House a) const;		// Добавляет дом в конец улицы.
	Street & operator += (Street* a);			// Присоединяет вторую улицу к первой.
	bool operator == (Street& a) const;			// Сравнивает улицы с учетом названий.
	const Street & operator = (const Street & a);	// Копирует сведения об улице, с сохранением названия.
	House & operator [](int index);					// Позволяет доступ к дому по его индексу.
	House & operator [](string num);				// Позволяет доступ к дому по его номеру.
};
