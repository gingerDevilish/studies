/** \file fastsort.cpp
* A simple C++ program that shows how the method of Quicksort works.
*/

#include <stdlib.h>
#include <stdio.h>
void quicksort(__int8 *mas, int first, int last);
int main();

/** \fn void quicksort(__int8 *mas, int first, int last) 
*	\brief A function for sorting an array with Quicksort method.
*	The function is found on the Internet. Tested on an 5-item array, works properly.
*	According to [Wikipedia](https://en.wikipedia.org/wiki/Quicksort#Algorithm), 
*		> Quicksort first divides a large array into two smaller sub-arrays: the low elements and the high elements. Quicksort can then recursively		> sort the sub-arrays.
*	\param mas a pointer to an __int8 array you want to sort.
*	\param first the left divide of function's usage. If you don't want to change first 7 elements, consider using 7 here, and so on.
*	\param last the right divide of function's usage. If you don't want to change last 3 elements, consider using n-4 there, n is the size of your array.
*/
void quicksort(__int8 *mas, int first, int last)
{
	/** \var __int8 mid
	*	A variable that will contain the pivot. The pivot will be the average between the first and the last elements.
	*/
	/** \var __int8 count
	*	A variable used to swap pairs of elements.
	*/
	__int8 mid, count;
	/** \var int f
	*	Serves like another shorter name for [first](@ref first).	
	*/
	/** \var int l
	*	Serves like another shorter name for [last](@ref last).
	*/
	int f=first, l=last;
	mid=mas[(f+l) / 2];
	do
	{
		while (mas[f]<mid) f++;
		while (mas[l]>mid) l--;
		if (f<=l) 
		{
			count=mas[f];
			mas[f]=mas[l];
			mas[l]=count;
			f++;
			l--;
		}
	} while (f<l);
	if (first<l) quicksort(mas, first, l);
	if (f<last) quicksort(mas, f, last);
}

/// A classical C++ `int main()`
/**
*	It's actually a little weird to write documentation for `main()`. In this sample, all it does is serving an example of usage of quicksort.
*	Usage is simple.
*	- First, after you run the program, enter the number of elements in your array. It can be big: think of a million once :). Press ENTER.
*	- Second, you are supposed to put down all the elements; there should be as much of them as you decided in the previous step, not more and not less. The range of numbers is [-128; 127]. And yet don't forget to press SPACE after each one. Press ENTER when you finish.
*	- Enjoy! THe program will sort your array, and after that it will be showed. Then press any key to close the program.
*/
int main()
{
	/** \var __int8 *arr
	* This pointer will be used for creating an array containing numerous __int8 elements.
	*/
	__int8 *arr;
	/**\var int n
	* A variable that will contain a number of elements in an array
	*/
	int n;
	printf("\nEnter the number of elements in array:\t");
	scanf("%d", &n);
	arr=new __int8 [n];
	printf("\nNow enter all the elements (range -128 to 127):\n");
	for (int i=0; i<n; i++)
		scanf("%d", &arr[i]);
	quicksort(arr, 0, n-1);
	printf("\nHere is your array sorted:\n");
	for (int i=0; i<n; i++)
		printf("%d ", arr[i]);
	system("pause");
	return 0;
}