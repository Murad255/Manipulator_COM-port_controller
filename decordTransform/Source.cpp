#include <iostream>
#include <math.h>
#include "conio.h"
#define PI 3.14
#define L1	105
#define L2	147

using namespace std;

class Coord
{
	int decX, decY, decZ, spR;
	float spQ, spF;
	float servQ, servA, servB;
public:
	Coord(int decX, int decY, int decZ);
	void write(int decX, int decY, int decZ);
	float givA() { return servA; }
	float givB() { return servB; }
	float givQ() { return servQ; }
};


int main() {
	setlocale(0,"");
	int x, y, z;
	cout << "¬ведите координаты. \nX=";
	cin >> x;
	cout<< "\nY = ";
	cin >> y;
	cout << "\nZ = ";
	cin >> z;

	Coord dec(x,y,z);
	cout << '\n' << "угол A=" << dec.givA() << '\n' << "угол B=" << dec.givB() << '\n' << "угол Q=" << dec.givQ();

	_getch();
	return 0;
}
void Coord::write(int decX, int decY, int decZ) {
	this->decX = decX;
	this->decY = decY;
	this->decZ = decZ;

	spR = (int)sqrt(decX*decX + decY * decY + decZ * decZ);
	spQ = (float)((atan(sqrt(decX*decX + decY * decY) / decZ)) * 180.0 / PI);
	spF = (float)(atan(decY / decX) *180.0 / PI);

	servQ = spQ;
	servB = (float)(180 - (acos((L1*L1 + L2 * L2 - spR * spR) / (2 * L1*L2))) * 180.0 / PI);
	servA = (float)(spF - (acos((-L1 * L1 + L2 * L2 - spR * spR) / (2 * L1*spR))) * 180 / PI);

}

Coord::Coord(int decX = 0, int decY = 0, int decZ = 0) 
{
	write(decX, decY, decZ);
}