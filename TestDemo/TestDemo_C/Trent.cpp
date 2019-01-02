/*************************************************
Copyright:  Copyright Trent.
Author:		Trent
Date:		2015-03-09
Description:MTCNN.
**************************************************/


#include"Trent.h"

#include "mtcnn.h"
#include <stdint.h>
static char* model_path = "C:/Users/Administrator/Desktop/mtcnn/001_TestDemo/TestDemo/TestDemo_C/models";
MTCNN* mtcnn;


int FD_Initialize()
{
	mtcnn = new MTCNN(model_path);
	return 0;
};
int FD_Process(unsigned char* srcData, int width, int height, int stride, int faceInfos[15])
{
	unsigned char* data = (unsigned char*)malloc(sizeof(unsigned char) * height * width * 3);
	unsigned char* pSrc = srcData;
	unsigned char* pData = data;
	for (int j = 0; j < height; j++)
	{
		for (int i = 0; i < width; i++)
		{
			pData[0] = pSrc[0];
			pData[1] = pSrc[1];
			pData[2] = pSrc[2];
			pData += 3;
			pSrc += 4;
		}

	}
	ncnn::Mat ncnn_img = ncnn::Mat::from_pixels(data, ncnn::Mat::PIXEL_RGB, width, height);
	std::vector<Bbox> finalBbox;
	mtcnn->detect(ncnn_img, finalBbox);
	if(finalBbox.size() > 0)
	{
		faceInfos[0] = 1;
		faceInfos[1] = finalBbox[0].x1;
		faceInfos[2] = finalBbox[0].y1;
		faceInfos[3] = finalBbox[0].x2 - finalBbox[0].x1;
		faceInfos[4] = finalBbox[0].y2 - finalBbox[0].y1;
		faceInfos[5] = finalBbox[0].ppoint[0];
		faceInfos[6] = finalBbox[0].ppoint[5];
		faceInfos[7] = finalBbox[0].ppoint[1];
        faceInfos[8] = finalBbox[0].ppoint[6];
        faceInfos[9] = finalBbox[0].ppoint[2];
        faceInfos[10] = finalBbox[0].ppoint[7];
		faceInfos[11] = finalBbox[0].ppoint[3];
		faceInfos[12] = finalBbox[0].ppoint[8];
		faceInfos[13] = finalBbox[0].ppoint[4];
		faceInfos[14] = finalBbox[0].ppoint[9];
	}
	free(data);
	return 0;
};
void FD_Unitialize()
{
	if(model_path != NULL)
		free(model_path);
	delete(mtcnn);
}