
/*************************************************************************
Copyright:   Trent.
Author:		 Trent
Date:		 2015-4-23
Mail:        dongtingyueh@qq.com
Description: Sticker for face
*************************************************************************/
#ifndef __TRENT_FD_MTCNN__
#define __TRENT_FD_MTCNN__

#include <stdlib.h>
#include <stdio.h>
#include <math.h>

#define MIN2(a, b) ((a) < (b) ? (a) : (b))
#define MAX2(a, b) ((a) > (b) ? (a) : (b))
#define CLIP3(x, a, b) MIN2(MAX2(a,x), b)

#ifdef _MSC_VER

#ifdef __cplusplus
#define EXPORT extern "C" _declspec(dllexport)
#else
#define EXPORT __declspec(dllexport)
#endif

EXPORT int FD_Initialize();
EXPORT int FD_Process(unsigned char* srcData, int width, int height, int stride, int faceInfos[15]);
EXPORT void FD_Unitialize();
#else

#ifdef __cplusplus
extern "C" {
#endif    
	int FD_Initialize(char* path);
	int FD_Process(unsigned char* srcData, int width, int height, int stride, int faceInfos[15]);
	void FD_Unitialize();
#endif
#endif
