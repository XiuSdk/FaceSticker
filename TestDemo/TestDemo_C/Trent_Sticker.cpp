#include"Trent_Sticker.h"
#include"Trent.h"
void GetTexTransMatrix(float x1, float y1, float x2, float y2, float x3, float y3,float tx1, float ty1, float tx2, float ty2, float tx3, float ty3, float*texMatrix)
{
	float detA;
	detA = tx1*ty2 + tx2*ty3 + tx3*ty1 - tx3*ty2 - tx1*ty3 - tx2*ty1;
	float A11, A12, A13, A21, A22, A23, A31, A32, A33;
	A11 = ty2 - ty3;
	A21 = -(ty1 - ty3);
	A31 = ty1 - ty2;
	A12 = -(tx2 - tx3);
	A22 = tx1 - tx3;
	A32 = -(tx1 - tx2);
	A13 = tx2*ty3 - tx3*ty2;
	A23 = -(tx1*ty3 - tx3*ty1);
	A33 = tx1*ty2 - tx2*ty1;  
	texMatrix[0] = (x1*A11 + x2*A21 + x3*A31) / detA;  
	texMatrix[1] = (x1*A12 + x2*A22 + x3*A32) / detA;  
	texMatrix[2] = (x1*A13 + x2*A23 + x3*A33) / detA;  
	texMatrix[3] = (y1*A11 + y2*A21 + y3*A31) / detA; 
	texMatrix[4] = (y1*A12 + y2*A22 + y3*A32) / detA;       
	texMatrix[5] = (y1*A13 + y2*A23 + y3*A33) / detA;  
}
int Trent_Sticker(unsigned char* srcData, int width, int height, int stride, unsigned char* mask, int maskWidth, int maskHeight, int maskStride, int srcFacePoints[6], int maskFacePoints[6], int ratio)
{
	int ret = 0;
	float H[6];
	GetTexTransMatrix(maskFacePoints[0], maskFacePoints[1], maskFacePoints[2], maskFacePoints[3], maskFacePoints[4], maskFacePoints[5], srcFacePoints[0], srcFacePoints[1], srcFacePoints[2], srcFacePoints[3], srcFacePoints[4], srcFacePoints[5], H);
	for (int j = 0; j < height; j++)
	{
		for (int i = 0; i < width; i++)
		{
			float x = (float)i;
			float y = (float)j;
			float tx = 0;
			float ty = 0;
			tx = (int)((H[0] * (x)+H[1] * (y)+H[2]) + 0.5);
			ty = (int)((H[3] * (x)+H[4] * (y)+H[5]) + 0.5);
			tx = CLIP3(tx, 0, maskWidth - 1);
			ty = CLIP3(ty, 0, maskHeight - 1);			
			int mb = mask[(int)tx * 4 + (int)ty * maskStride];
			int mg = mask[(int)tx * 4 + (int)ty * maskStride + 1];
			int mr = mask[(int)tx * 4 + (int)ty * maskStride + 2];
			int alpha = mask[(int)tx * 4 + (int)ty * maskStride + 3];
			int b = srcData[i * 4 + j * stride];
			int g = srcData[i * 4 + j * stride + 1];
			int r = srcData[i * 4 + j * stride + 2];			
			srcData[(int)i * 4 + (int)j * stride] = CLIP3((b * (255 - alpha) + mb * alpha) / 255, 0, 255);
			srcData[(int)i * 4 + (int)j * stride + 1] = CLIP3((g * (255 - alpha) + mg * alpha) / 255, 0, 255);
			srcData[(int)i * 4 + (int)j * stride + 2] = CLIP3((r * (255 - alpha) + mr * alpha) / 255, 0, 255);
		}
	}
	return ret;
};