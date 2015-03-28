#include "inclusions.h"

class Street
{
	string StreetName;								// Название улицы.
	vector <House> members;							// Вектор, содержащий дома на данной улице.
	int length;										// Количество домов на улице.
	int unitax;										// Единая налоговая ставка. Если не установена явно, значение - 0.
	Street();										// Запрещаем конструктор по умолчанию, т.к. улица обязана иметь название.
public:
	Street(const string& name);						// Создает пустую улицу с заданным названием.
	Street(const string& name, const Street & a);	// Создает улицу, устроенную по образцу существующей.
	~Street();										// Деструктор.
	string getname() const;							// Возвращает название улицы.
	int getlength() const;							// Возвращает количество домов на улице.
	int getunitax() const;							// Возвращает значение единой налоговой ставки.
	void addhouse(const House& a);					// Добавляет дом в конец улицы.
	void addtopos(const House& a, int pos);			// Добавляет дом на заданную позицию.
	void delhouse(int pos);							// Удаляет дом, стоящей на определенной позиции.
	void delhouse(const string& HouseNum);			// Удаляет дом с определенным номером.
	void setname (const string& name);				// Переименовывает улицу.
	Street split (int pos, const string& newname);	// Разделяет улицу на две по заданной границе, возвращает новую улицу с заданным именем.
	void setunitax(int newtax);						// Устанавливает одинаковый налог для всех домов.
	int counttax() const;							// Рассчитывает общую сумму сборов, которые можно получить с этой улицы.
	int debt() const;								// Возвращает сумму, которую жители улицы задолжали.
	int gettaken() const;							// Возвращает сумму, которую в данном месяце уже собрали.
	void printInfo() const;							// Выводит на экран информацию об улице и всех ее домах.
	Street& operator + (House a) const;				// Добавляет дом в конец улицы.
	Street& operator += (Street* a);				// Присоединяет вторую улицу к первой.
	bool operator == (Street& a) const;				// Сравнивает улицы с учетом названий.
	bool has (const House& a) const;				// Проверяет наличие на улице заданного дома, с учетом номера.
	const Street& operator = (const Street& a);		// Копирует сведения об улице, с сохранением названия.
	House& operator [](int index);					// Позволяет доступ к дому по его индексу.
	House& operator [](string num);					// Позволяет доступ к дому по его номеру.
};
