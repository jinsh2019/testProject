// ConsoleApplication1.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>


extern "C" {
	_declspec(dllexport) int calc_size(int size);
}

int calc_size(int size) {
	int* buffer = new int[size];
	return 2 * size;
}