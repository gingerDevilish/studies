#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>

int main ()
{
	unsigned char *arr, k=0;
	int n=0;
	printf("Enter the number of elements in the array:\n");
	fflush(stdin);
	scanf("%d", &n);
	arr = (unsigned char *) malloc(sizeof(unsigned char)*n);
	printf("\nNow please enter all the elements of your array.");
	printf("\nTheir range should be 0 to 255.\n");
	for (int i=0; i<n; i++)
	{
		printf("\nElement number %d:\t", i+1);
		scanf ("%d", &arr[i]);
	}
	printf("\nHere is your array:\n");
	for (int i=0; i<n; i++)
		printf("%d\t", arr[i]);
	for (int i=1; i<n; i++)
		for (int j=0; j<i; j++)
		{
			if (arr[j]>arr[i])
			{
				k=arr[j];
				arr[j]=arr[i];
				arr[i]=k;
			}
		}
	printf("\nSo there is your array sorted:\n");
	for (int i=0; i<n; i++)
		printf("%d\t", arr[i]);
	system("pause");
	return 0;
}